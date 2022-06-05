using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/* 
    Configuration File For Everything Related To Cards 
*/
namespace Config
{
    public enum CardGrade
    {
        Normal,
        Rare,
        Epic,
        Legendary
    }
    public enum Card
    {
        Knights_Move, // 0
        Arrow_Of_Madness, // 1
        Rush, // 2
        Heavy_Armor, // 3
        Teleport, // 4
        Thunder_Bolt, // 5
        Morale_Boost, // 6
        Clairvoyant, // 7
        King_Power, // 8
        Double_Shot, // 9
        Penetrating_Arrow, // 10
        Rage_Attack, // 11
        Swap, // 12
        Disarm, // 13
        Testudo, // 14
        Royal_Dagger, // 15
        Diagonal_Shot, // 16
        Longbow_Shot, // 17
        Spear_Throw, // 18
        Shield, // 19
        Demotion_Punishment, // 20
        Last_Ditch_Effort, // 21
        Losing_Sacrifice, // 22
        Disguise, // 23
    };

    public delegate bool CardFunction(GameObject piece, GameObject card);
    public class CardConfig
    {
        public static Dictionary<Card, (CardFunction, Piece, CardGrade)> card_dict = new Dictionary<Card, (CardFunction, Piece, CardGrade)>()
        {
            {   // Knight's Move Start
                Card.Knights_Move, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Knights_Move(나이트 빙의): 체스 나이트 움직임으로 공격 및 이동
                        if (!GameManager.executing)
                        {
                            int[,] move_list_core = { { 1, 2 }, { 2, 1 } };
                            List<int[]> move_list = new List<int[]>();
                            List<GameObject> dots = new List<GameObject>();

                            for (int i = 0; i < 2; i++)
                            {
                                for (int j = 0; j < 2; j++)
                                {
                                    for (int k = 0; k < move_list_core.GetLength(0); k++)
                                    {
                                        int diff_x = move_list_core[k, 0];
                                        int diff_y = move_list_core[k, 1];
                                        if (i == 1)
                                        {
                                            diff_x *= -1;
                                        }
                                        if (j == 1)
                                        {
                                            diff_y *= -1;
                                        }
                                        move_list.Add(new int[] { diff_x, diff_y });
                                    }
                                }
                            }
                            piece.GetComponent<ChessPiece>().createDots(move_list, card);
                        }
                        return true; // later to be changed 
                    },
                    Piece.Mage, // Card Target 
                    CardGrade.Normal // Card Grade
                )
            },  // Knight's Move End
            {   // Arrow Of Madness Start
                Card.Arrow_Of_Madness, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Arrow_Of_Madness(광기의 화살): 가로, 세로, 대각선 모든 방향으로 1발씩 쏨
                        if (!GameManager.executing)
                        {
                            List<GameObject> dots = new List<GameObject>();
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 1; j < 8; j++)
                                {
                                    int new_x_strike = piece.GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_surround[i, 0] * j);
                                    int new_y_strike = piece.GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_surround[i, 1] * j);

                                    // Check the location is out of bound 
                                    if(Helper.outOfBoard(new_x_strike, new_y_strike)) continue;
                                    GameObject new_cell_stirke = PieceConfig.cells[new_y_strike, new_x_strike];
                                    if (new_cell_stirke.gameObject.transform.childCount > 0)
                                    {
                                        // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                                        GameObject enemy = new_cell_stirke.transform.GetChild(0).gameObject;
                                        dots.Add(piece.GetComponent<ChessPiece>().createStrike(new_cell_stirke, enemy, card));

                                        break; // If there is any blocking piece, then further iteration is no needed (the piece further than the blocking piece cannot be attacked)
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        else
                        {
                            GameManager.dots[0].GetComponent<StrikeController>().deleteCard();
                            foreach (GameObject strike_dot in GameManager.dots)
                            {
                                GameObject destroy_piece = strike_dot.transform.parent.GetChild(0).gameObject;
                                int st_self = piece.GetComponent<ChessPiece>().offense_power;
                                int hp_enemy = destroy_piece.GetComponent<ChessPiece>().defense_power;
                                if (st_self < hp_enemy)
                                {
                                    destroy_piece.GetComponent<ChessPiece>().defense_power = hp_enemy - st_self;
                                    continue;
                                }
                                destroy_piece.BroadcastMessage(EventName.DestroyKing.ToString(), SendMessageOptions.DontRequireReceiver);
                                UnityEngine.Object.Destroy(destroy_piece);
                            }
                            GameManager.destroyAlldots();
                            GameManager.destroyAllIndicators();
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Archer, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Arrow_Of_Madness End
            {   // Rush Start
                Card.Rush, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Rush(돌진): 3칸 앞으로 전진하면서 경로에 있는 말 모두 제거

                        bool move_available = false;
                        List<GameObject> dots = new List<GameObject>();
                        if (!GameManager.executing)
                        {
                            for (int i = 1; i < 4; i++)
                            {
                                int new_x_strike = piece.GetComponent<ChessPiece>().getIndex().indexX;
                                int new_y_strike = piece.GetComponent<ChessPiece>().getIndex().indexY + i * piece.GetComponent<ChessPiece>().move_dir;

                                // Check the location is out of bound 
                                if(Helper.outOfBoard(new_x_strike, new_y_strike)) continue;

                                move_available = true;
                                GameObject new_cell = PieceConfig.cells[new_y_strike, new_x_strike];

                                if (new_cell.transform.childCount > 0)
                                {
                                    GameObject front_piece = new_cell.transform.GetChild(0).gameObject;
                                    dots.Add(piece.GetComponent<ChessPiece>().createStrike(new_cell, front_piece, card));
                                }
                                else {
                                    dots.Add(piece.GetComponent<ChessPiece>().createDot(new_cell, card));
                                }
                            }
                            GameManager.dots = dots;
                            return move_available;
                        }
                        else
                        {
                            if (GameManager.dots.Count > 0)
                            {
                                GameManager.dots[0].GetComponent<StrikeController>()?.deleteCard();
                                GameManager.dots[0].GetComponent<MoveController>()?.deleteCard();
                                foreach (GameObject strike_dot in GameManager.dots)
                                {

                                    strike_dot.GetComponent<StrikeController>()?.moveParent();
                                    strike_dot.GetComponent<MoveController>()?.moveParent();
                                    GameObject destroy_piece = strike_dot.transform.parent.GetChild(0).gameObject;
                                    destroy_piece.BroadcastMessage(EventName.DestroyKing.ToString(), SendMessageOptions.DontRequireReceiver);
                                    UnityEngine.Object.Destroy(destroy_piece);
                                }
                            }
                            GameManager.destroyAlldots();
                            GameManager.destroyAllIndicators();
                            GameManager.endTurn();
                            return true;
                        }
                    },
                    Piece.Warrior, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Rush End
            {   // Heavy_Armor Start
                Card.Heavy_Armor, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Heavy Arm(중무장): 방패+1, 공격+1
                        if (GameManager.executing)
                        {
                            piece.GetComponent<ChessPiece>().offense_power++;
                            piece.GetComponent<ChessPiece>().defense_power++;
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Epic);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Warrior, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },  // Heavy_Armor End
            {   // Teleport Start
                Card.Teleport, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Teleport(순간이동): 아무 위치로 순간이동
                        bool move_available = false;
                        List<GameObject> dots = new List<GameObject>();
                        if (!GameManager.executing)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    GameObject cell = PieceConfig.cells[j, i];
                                    if (cell.transform.childCount > 0)
                                    {
                                        continue;
                                    }
                                    move_available = true;
                                    GameObject dot = Helper.prefabNameToGameObject(Prefab.Move.ToString());
                                    dot.transform.SetParent(cell.transform, false);
                                    dot.transform.position = cell.transform.position;
                                    dot.GetComponent<MoveController>().parent = piece;
                                    dot.GetComponent<MoveController>().card = card;
                                    dots.Add(dot);
                                }
                            }
                            GameManager.dots = dots;
                            return move_available;
                        }
                        return true;
                    },
                    Piece.Mage, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Teleport End
            {   // Thunder_Bolt Start
                Card.Thunder_Bolt, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Thunder Bolt(썬더볼트): 인접 대상에 발동 가능, 피격 대상 공격 및 인접 대상을 2개까지 공격
                        bool move_available = false;
                        List<GameObject> dots = new List<GameObject>();
                        if (!GameManager.executing)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                int new_x_strike = piece.GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_surround[i, 0]);
                                int new_y_strike = piece.GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_surround[i, 1]);
                                // Check the location is out of bound 
                                if(Helper.outOfBoard(new_x_strike, new_y_strike)) continue;

                                move_available = true;

                                GameObject new_cell_Strike = PieceConfig.cells[new_y_strike, new_x_strike];
                                if (new_cell_Strike.transform.childCount > 0)
                                {
                                    GameObject enemy = new_cell_Strike.transform.GetChild(0).gameObject;
                                    if (Helper.isEnemy(piece, enemy))
                                    {
                                        GameObject dot = Helper.prefabNameToGameObject(Prefab.Thunder_Bolt.ToString());
                                        dot.transform.SetParent(new_cell_Strike.transform, false);
                                        dot.transform.position = new_cell_Strike.transform.position;
                                        dot.GetComponent<ThunderBoltController>().parent = piece;
                                        dot.GetComponent<ThunderBoltController>().card = card;
                                        dots.Add(dot);
                                    }
                                }
                            }
                            GameManager.dots = dots;
                            return move_available;
                        }
                        return true;
                    },
                    Piece.Mage, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Teleport End
            {   // Rage_Attack Start
                Card.Rage_Attack, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Rage Attack(분노의 일격): 방어력을 무시하고 공격
                        if (!GameManager.executing)
                        {
                            bool move_available = false;
                            List<GameObject> dots = new List<GameObject>();

                            int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX;
                            int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + piece.GetComponent<ChessPiece>().move_dir;

                            if(Helper.outOfBoard(new_x, new_y)) return move_available;

                            GameObject new_cell = PieceConfig.cells[new_y, new_x];
                            if (new_cell.transform.childCount > 0)
                            {
                                GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                dots.Add(piece.GetComponent<ChessPiece>().createStrike(new_cell, enemy, card));
                                move_available = true;
                            }
                            GameManager.dots = dots;
                            return move_available;
                        }
                        else
                        {
                            if (GameManager.dots.Count > 0)
                            {
                                GameManager.dots[0].GetComponent<StrikeController>().deleteCard();
                                foreach (GameObject strike_dot in GameManager.dots)
                                {
                                    strike_dot.GetComponent<StrikeController>().moveParent();
                                    UnityEngine.Object.Destroy(strike_dot.transform.parent.GetChild(0).gameObject);
                                }
                                GameManager.destroyAlldots();
                                GameManager.destroyAllIndicators();
                                GameManager.endTurn();
                                return true;
                            }
                            return false;
                        }
                    },
                    Piece.Warrior, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },  // Rage_Attack End
            {   // Morale_Boost Start
                Card.Morale_Boost, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Morale Boost(사기증진): 자신 주변 3x3 범위안에 있는 아군한테 모두 공격력 +1 부여
                        if(GameManager.executing) {
                            int curr_x = piece.GetComponent<ChessPiece>().getIndex().indexX;
                            int curr_y = piece.GetComponent<ChessPiece>().getIndex().indexY;

                            for(int i=0; i<8; i++) {
                                int new_x = curr_x + (PieceConfig.move_list_surround[i, 0]);
                                int new_y = curr_y + (PieceConfig.move_list_surround[i, 1]);

                                // Check the location is out of bound 
                                if(Helper.outOfBoard(new_x, new_y)) continue;

                                GameObject new_cell = PieceConfig.cells[new_y, new_x];

                                if(new_cell.transform.childCount > 0) {
                                    GameObject ally = new_cell.transform.GetChild(0).gameObject;
                                    if(!Helper.isEnemy(piece, ally)) {
                                        ally.GetComponent<ChessPiece>().offense_power++;
                                    }
                                }
                            }
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Legendary);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.King, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Morale_Boost End
            {   // Clairvoyant Start
                Card.Clairvoyant, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Clairvoyant(천리안): 장착효과, 사거리 무한대로 증가
                        if(GameManager.executing) {
                            piece.GetComponent<Archer>().attack_range = 8;
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Legendary);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Archer, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // Clairvoyant End
            {   // King_Power Start
                Card.King_Power, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //King's Power(왕의 일격): 범위에 있는 적 모두 섬멸
                        //     x     x     x
                        //       x   x   x
                        //         x x x
                        //           k
                        bool move_available = false;
                        List<GameObject> dots = new List<GameObject>();
                        if(!GameManager.executing){
                            for(int i=1; i<4; i++) {

                                for(int j = -i; j<i+1; j+=i){
                                    int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_surround[2, 0]) + j;
                                    int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_surround[2, 1])*i;

                                    // Check the location is out of bound 
                                    if(Helper.outOfBoard(new_x, new_y)) continue;

                                    GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                    if(new_cell.transform.childCount > 0) {
                                        GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                        if( Helper.isEnemy(piece, enemy) ) { 
                                            // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                                            dots.Add(piece.GetComponent<ChessPiece>().createStrike(new_cell, enemy, card));
                                            move_available = true;
                                        }
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        else
                        {
                            foreach(GameObject strike_dot in GameManager.dots) {
                                GameObject destroy_piece = strike_dot.transform.parent.GetChild(0).gameObject;
                                int st_self = piece.GetComponent<ChessPiece>().offense_power;
                                int hp_enemy = destroy_piece.GetComponent<ChessPiece>().defense_power;
                                if(st_self < hp_enemy) {
                                    destroy_piece.GetComponent<ChessPiece>().defense_power = hp_enemy - st_self;
                                    continue;
                                }
                                destroy_piece.BroadcastMessage(EventName.DestroyKing.ToString(), SendMessageOptions.DontRequireReceiver);
                                UnityEngine.Object.Destroy(destroy_piece);
                            }

                            GameManager.lastDotClicked(true);
                            return true;
                        }
                        return move_available;
                    },
                    Piece.King, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },  // King_Power End
            {   // Double_Shot Start
                Card.Double_Shot, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Double Shot(더블 샷): 가로세로 방향중 두 방향으로 한발씩 쏨. 
                        if(GameManager.executing) {
                            piece.GetComponent<Archer>().num_enemy = 0;
                            List<int[]> basic_moves = piece.GetComponent<ChessPiece>().basic_moves;
                            piece.GetComponent<Archer>().only_attack = true;
                            piece.GetComponent<Archer>().createArrowArcher(basic_moves, card, 2);
                            //ArrowController.numAttack = 2;
                        }
                        return ArrowController.num_attack>0;
                    },
                    Piece.Archer, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },  // Double_Shot End
            {   // Penetrating_Arrow Start
                Card.Penetrating_Arrow, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //Penetrating Arrow(관통 화살): 공격력+1로 화살을 발사. 
                        if(GameManager.executing){
                            piece.GetComponent<ChessPiece>().offense_power++;
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Epic);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Archer, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },  // Penetrating_Arrow End
            {   // Swap Start
                Card.Swap, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // Swap (위치교환): 아군 말과 자신의 위치를 바꾼다. 
                        // 아군을 모두 찾고 -> 찾은 모든 아군에 생성한 인디케이터를 차일드로 set
                        // 
                        
                        if(!GameManager.executing) {
                            List<GameObject> dots = new List<GameObject>();
                            for(int i=0; i<8; i++) {
                                for(int j=0; j<5; j++) {
                                    GameObject cell = PieceConfig.cells[i, j];
                                    if(cell.transform.childCount <= 0 ) {
                                        continue;
                                    }

                                    if(piece.GetComponent<ChessPiece>().getIndex().indexX == j &&
                                       piece.GetComponent<ChessPiece>().getIndex().indexY == i) continue;

                                    GameObject other_piece = cell.transform.GetChild(0).gameObject;
                                    if(!Helper.isEnemy(piece, other_piece)) {
                                        GameObject swap = Helper.prefabNameToGameObject(Prefab.Swap.ToString());
                                        swap.transform.SetParent(cell.transform, false);
                                        swap.transform.position = cell.transform.position;
                                        swap.GetComponent<SwapController>().mage = piece;
                                        swap.GetComponent<SwapController>().target = other_piece;
                                        swap.GetComponent<SwapController>().card = card;
                                        dots.Add(swap);
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return true;
                    },
                    Piece.Mage, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },  // Switch_Teleport End
            {   // Disarm Start
                Card.Disarm,
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        // 
                        
                        if(!GameManager.executing) {
                            List<GameObject> dots = new List<GameObject>();
                            for(int i=0; i<8; i++) {
                                for(int j=0; j<5; j++) {
                                    GameObject cell = PieceConfig.cells[i, j];
                                    if(cell.transform.childCount <= 0 ) {
                                        continue;
                                    }

                                    if(piece.GetComponent<ChessPiece>().getIndex().indexX == j && piece.GetComponent<ChessPiece>().getIndex().indexY == i) continue;

                                    GameObject other_piece = cell.transform.GetChild(0).gameObject;
                                    if(Helper.isEnemy(piece, other_piece)) {
                                        GameObject disarm = Helper.prefabNameToGameObject(Prefab.Disarm.ToString());
                                        disarm.transform.SetParent(cell.transform, false);
                                        disarm.transform.position = cell.transform.position;
                                        disarm.GetComponent<DisarmController>().target = other_piece;
                                        disarm.GetComponent<DisarmController>().card = card;
                                        dots.Add(disarm);
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return true;
                    },
                    Piece.Mage, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },
            {   // Testudo Start
                Card.Testudo, // Key 
                (
                    (GameObject piece, GameObject card) => { // Card Logic
                        //
                        if(GameManager.executing) {
                            int curr_x = piece.GetComponent<ChessPiece>().getIndex().indexX;
                            int curr_y = piece.GetComponent<ChessPiece>().getIndex().indexY;

                            for(int i=0; i<8; i++) {
                                int new_x = curr_x + (PieceConfig.move_list_surround[i, 0]);
                                int new_y = curr_y + (PieceConfig.move_list_surround[i, 1]);

                                // Check the location is out of bound 
                                if(Helper.outOfBoard(new_x, new_y)) continue;

                                GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                if(new_cell.transform.childCount > 0)
                                {
                                    GameObject ally = new_cell.transform.GetChild(0).gameObject;
                                    if(!Helper.isEnemy(piece, ally))
                                    {
                                        ally.GetComponent<ChessPiece>().defense_power++;
                                    }
                                }
                            }
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Legendary);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Warrior, // Card Target 
                    CardGrade.Legendary // Card Grade
                )
            },
            {   // Royal_Dagger Start
                Card.Royal_Dagger,
                (
                    (GameObject piece, GameObject card) => {

                        bool move_available = false;
                        if(!GameManager.executing){

                            List<GameObject> dots = new List<GameObject>();
                            for (int i = 0; i < 8; i++)
                            {
                                int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_surround[i, 0]);
                                int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_surround[i, 1]);

                                // Check the location is out of bound 
                                if(Helper.outOfBoard(new_x, new_y)) continue;


                                GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                if (new_cell.gameObject.transform.childCount > 0)
                                {
                                    GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                    if(Helper.isEnemy(piece, enemy)){
                                        dots.Add(piece.GetComponent<ChessPiece>().createStrike(new_cell, enemy , card));
                                        move_available = true;
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return move_available;
                    },
                    Piece.King,
                    CardGrade.Rare
                )
            },
            {   // Diagonal_Shot Start
                Card.Diagonal_Shot,
                (
                    (GameObject piece, GameObject card) => {

                        bool move_available = false;
                        if(!GameManager.executing){

                            List<GameObject> dots = new List<GameObject>();
                            int attack_range = piece.GetComponent<Archer>().attack_range;

                            for (int i = 0; i < 3; i++) {
                                for(int j = 1; j<attack_range; j++) {

                                    int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_diagonal[i, 0]*j);
                                    int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_diagonal[i, 1]*j);
                                    
                                    // Check the location is out of bound 
                                    if(Helper.outOfBoard(new_x, new_y)) continue;

                                    GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                    if(new_cell.transform.childCount > 0) {                                        
                                        // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                                        GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                        if(Helper.isEnemy(piece, enemy.gameObject) ) {
                                            dots.Add(piece.GetComponent<Archer>().createStrikeDot(new_cell, enemy, card, piece));
                                            move_available = true;
                                        }
                                        // If there is any blocking piece, then further iteration is no needed 
                                        // (the piece further than blocking piece cannot be attacked)
                                        break;
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return move_available;
                    },
                    Piece.Archer,
                    CardGrade.Rare
                )
            },
            {   // Longbow_Shot Start
                Card.Longbow_Shot,
                (
                    (GameObject piece, GameObject card) => {

                        bool move_available = false;
                        if(!GameManager.executing){

                            int num_piece = 0;
                            List<GameObject> dots = new List<GameObject>();
                            int attack_range = piece.GetComponent<Archer>().attack_range;
                            List<int[]> move_list = piece.GetComponent<ChessPiece>().basic_moves;

                            for(int i=0; i<3; ++i) {
                                for(int j=0; j<attack_range; ++j){

                                    // Get an index of cell where to attack
                                    int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX + (move_list[i][0] * j);
                                    int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + (move_list[i][1] * j);

                                    if(Helper.outOfBoard(new_x, new_y)) continue;

                                    GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                    if(new_cell.transform.childCount > 0 && num_piece < 3) {

                                        // If the enemy's Piece is located within the range of archer's attack, 
                                        // AND
                                        // If the numEnemy is less than 3 (Longbow shot can shot any enemy blocked by piece)
                                        // then create a strike dot
                                        GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                        if(Helper.isEnemy(piece, enemy) ) {
                                            dots.Add(piece.GetComponent<Archer>().createStrikeDot(new_cell, enemy, card, piece));
                                            num_piece++;
                                            move_available = true;
                                        }
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return move_available;
                    },
                    Piece.Archer,
                    CardGrade.Rare
                )
            },
            {   // Spear_Throw Start
                Card.Spear_Throw,
                (
                    (GameObject piece, GameObject card) => {

                        bool move_available = false;
                        if(!GameManager.executing){

                            int num_piece = 0;
                            List<GameObject> dots = new List<GameObject>();
                            List<int[]> move_list = piece.GetComponent<ChessPiece>().basic_moves;
                            for(int i = 0; i<3; i++){
                                for(int j=1; j<3; j++) {

                                    // Get an index of cell where to attack
                                    int new_x = piece.GetComponent<ChessPiece>().getIndex().indexX + (move_list[i][0] * j);
                                    int new_y = piece.GetComponent<ChessPiece>().getIndex().indexY + (move_list[i][1] * j);

                                    if(new_x > 4 || new_x < 0) {
                                        continue;
                                    }
                                    if(new_x > 7 || new_y < 0 ) {
                                        continue;
                                    }

                                    GameObject new_cell = PieceConfig.cells[new_y, new_x];
                                    if(new_cell.transform.childCount > 0 && num_piece < 2) {

                                        GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                                        if(Helper.isEnemy(piece, enemy)){
                                            dots.Add(piece.GetComponent<Warrior>().createStrike(new_cell, enemy, card, piece));
                                            num_piece++;
                                            move_available = true;
                                        }
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return move_available;
                    },
                    Piece.Warrior,
                    CardGrade.Epic
                )
            },
            {   // Shield Start
                Card.Shield,
                (
                    (GameObject piece, GameObject card) => { // Card Logic

                        if(GameManager.executing){
                            piece.GetComponent<ChessPiece>().defense_power++;
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Epic);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.Warrior, // Card Target 
                    CardGrade.Epic // Card Grade
                )
            },
            {   // Demolition_Punishment Start
                Card.Demotion_Punishment,
                (
                    (GameObject piece, GameObject card) => {

                        if(!GameManager.executing) {
                            List<GameObject> dots = new List<GameObject>();
                            for(int i=0; i<8; i++) {
                                for(int j=0; j<5; j++) {
                                    GameObject cell = PieceConfig.cells[i, j];
                                    if(cell.transform.childCount <= 0 ) {
                                        continue;
                                    }


                                    if(piece.GetComponent<ChessPiece>().getIndex().indexX == j &&
                                       piece.GetComponent<ChessPiece>().getIndex().indexY == i) continue;

                                    GameObject ally = cell.transform.GetChild(0).gameObject;
                                    if(!Helper.isEnemy(piece, ally)) {
                                        GameObject demolition = Helper.prefabNameToGameObject(Prefab.Demolition.ToString());
                                        demolition.transform.SetParent(cell.transform, false);
                                        demolition.transform.position = cell.transform.position;
                                        demolition.GetComponent<DemolitionController>().target = ally;
                                        demolition.GetComponent<DemolitionController>().card = card;
                                        dots.Add(demolition);
                                    }
                                }
                            }
                            GameManager.dots = dots;
                        }
                        return true;
                    },
                    Piece.King,
                    CardGrade.Rare
                )
            },
            {   // Last_Ditch_Effort Start
                Card.Last_Ditch_Effort,
                (
                    (GameObject piece, GameObject card) => {

                        if(GameManager.executing) {
                            piece.GetComponent<King>().last_ditch_effort = true;
                            GameManager.destroyAllIndicators();
                            card.GetComponent<CardController>().destroyCard();
                            card.GetComponent<CardController>().destroyMana((int)CardGrade.Epic);
                            GameManager.endTurn();
                        }
                        return true;
                    },
                    Piece.King,
                    CardGrade.Epic
                )

            }
        };
    }

}