using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System; 

public class CardEffect
{
    public static int [,] move_list = {{1, 0}, {1, 1}, {0, 1}, {-1, 1}, {-1, 0}, {-1, -1}, {0, -1}, {1, -1}}; 

    public static bool execute = false; 

    public static void Knights_Move(GameObject piece, GameObject card) {
        //Knight’s Move(나이트 빙의): 체스 나이트 움직임으로 공격 및 이동
        if(!execute) {
            int [,] move_list_core = {{1, 2}, {2, 1}}; 
            List<int[]> move_list = new List<int[]>();
            List<GameObject> dots = new List<GameObject>();

            for(int i = 0; i < 2; i++) {
                for(int j = 0; j < 2; j++) {
                    for(int k = 0; k < move_list_core.GetLength(0); k++) {
                        int diffX = move_list_core[k, 0];
                        int diffY = move_list_core[k, 1];
                        if(i == 1) {
                            diffX *= -1; 
                        }
                        if(j == 1) {
                            diffY *= -1; 
                        }
                        move_list.Add(new int[]{diffX, diffY}); 
                    }
                }
            }
            piece.GetComponent<ChessPiece>().createDots(move_list, card); 
        }
    }

    public static void ArrowOfMadness(GameObject piece, GameObject card)
    {
        //Arrow of Madness(광기의 화살): 가로세로대각선 모든 방향으로 1발씩 쏨
        if(!execute) {

            List<GameObject> dots = new List<GameObject>();
            for (int i = 0; i < 8; i++) {
                for(int j = 1; j<8; j++) {
                    Debug.Log("The location added is " + move_list[i, 0]+ " and " + move_list[i, 1]);
                    int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[i, 0]*j);
                    int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[i, 1]*j);
                    Debug.Log(newX_strike + " and " + newY_strike);
                    
                    // Check the location is out of bound 
                    if(newX_strike > 4 || newX_strike < 0) {
                        continue;
                    }
                    if(newY_strike > 7 || newY_strike < 0 ) {
                        continue;
                    }

                    GameObject newCell_Stirke = cardSave.cells[newX_strike, newY_strike ];
                    if(newCell_Stirke.gameObject.transform.childCount > 0) {
                        Debug.Log("Some piece is blocking!");
                        //if(newCell_Stirke.gameObject.transform.GetChild(0).name == "dot_move(Clone)" ) continue;
                        
                        // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                        dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell_Stirke, newCell_Stirke.transform.GetChild(0).gameObject, card));

                        break; // If there is any blocking piece, then further iteration is no needed (the piece further than the blocking piece cannot be attacked)
                    }
                }
            }
            Game_Manager.dots = dots; 
        } else {
            Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
            foreach(GameObject strike_dot in Game_Manager.dots) {
                GameObject destoryPiece = strike_dot.transform.parent.GetChild(0).gameObject;
                int st_self = piece.GetComponent<ChessPiece>().offensePower;
                int hp_enemy = destoryPiece.GetComponent<ChessPiece>().defensePower;
                if(st_self < hp_enemy) {
                    destoryPiece.GetComponent<ChessPiece>().defensePower = hp_enemy - st_self;
                    continue;
                }
                UnityEngine.Object.Destroy(destoryPiece);
            }   
            Game_Manager.destroyAlldots();
            Game_Manager.destroyAllIndicators();
        }

    }

    public static void Rush(GameObject piece, GameObject card)
    {
        //Rush(돌진): 3칸 앞으로 전진하면서 경로에 있는 말 모두 제거
        List<GameObject> dots = new List<GameObject>();
        if(!execute) {
            for(int i=1; i<4; i++) {
                int newX_strike = piece.GetComponent<ChessPiece>().indexX;
                int newY_strike = piece.GetComponent<ChessPiece>().indexY + i;

                // Check the location is out of bound 
                if(newX_strike > 4 || newX_strike < 0) {
                    continue;
                }
                if(newY_strike > 7 || newY_strike < 0 ) {
                    continue;
                }
                GameObject newCell_Stirke = cardSave.cells[newX_strike, newY_strike ];

                if(newCell_Stirke.gameObject.transform.childCount > 0) {
                    // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                    dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell_Stirke, newCell_Stirke.transform.GetChild(0).gameObject, card));
                }
            }
            Game_Manager.dots = dots; 
        } else {
            if(Game_Manager.dots.Count > 0) {
                Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
                foreach(GameObject strike_dot in Game_Manager.dots) {
                    strike_dot.GetComponent<strikeController>().moveParent();
                    UnityEngine.Object.Destroy(strike_dot.transform.parent.GetChild(0).gameObject);
                }   
            }
            Game_Manager.destroyAlldots();
            Game_Manager.destroyAllIndicators();
        }
    }

    public static void Heavy_Armor(GameObject piece, GameObject card) 
    {
        //Heavy Arm(중무장): 방패+1, 공격+1
        if(execute) {
            piece.GetComponent<ChessPiece>().offensePower++;
            piece.GetComponent<ChessPiece>().defensePower++;
            Game_Manager.destroyAllIndicators();
            card.GetComponent<dragDrop>().destoryCard();
        }
    }

    public static void Teleport(GameObject piece, GameObject card)
    {
        //Teleport(순간이동): 아무 위치로 순간이동
        List<GameObject> dots = new List<GameObject>();
        if(!execute) {
            for(int i=0; i<5; i++) {
                for(int j=0; j<8; j++) {
                    GameObject cell = cardSave.cells[i, j];
                    if(cell.transform.childCount > 0 ) {
                        continue;
                    }   
                    UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
                    GameObject dot = GameObject.Instantiate(prefab) as GameObject;
                    dot.transform.SetParent(cell.transform, false);
                    dot.transform.position = cell.transform.position;
                    dot.GetComponent<dotController>().parent = piece; 
                    dot.GetComponent<dotController>().card = card;
                    dots.Add(dot);
                }
            }
            Game_Manager.dots = dots;
        }
    }

    public static void Thunder_Bolt(GameObject piece, GameObject card) 
    {
        //Thunder Bolt(썬더볼트): 인접 대상에 발동 가능, 피격 대상 공격 및 인접 대상을 2개까지 공격
        List<GameObject> dots = new List<GameObject>();
        if(!execute) {

            for(int i=0; i<8; i++) {
                int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[i, 0]);
                int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[i, 1]);
                
                // Check the location is out of bound 
                if(newX_strike > 4 || newX_strike < 0) {
                    continue;
                }
                if(newY_strike > 7 || newY_strike < 0 ) {
                    continue;
                }
                
                GameObject newCell_Strike = cardSave.cells[newX_strike, newY_strike ];
                if(newCell_Strike.transform.childCount > 0) {
                    GameObject enemyPiece = newCell_Strike.transform.GetChild(0).gameObject;
                    if(enemyPiece.GetComponent<ChessPiece>().player != piece.GetComponent<ChessPiece>().player) {
                        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/ThunderBolt.prefab", typeof(GameObject));
                        GameObject dot = GameObject.Instantiate(prefab) as GameObject;
                        dot.transform.SetParent(newCell_Strike.transform, false);
                        dot.transform.position = newCell_Strike.transform.position;
                        dot.GetComponent<thunderBoltStrikeController>().parent = piece; 
                        dot.GetComponent<thunderBoltStrikeController>().card = card;
                        dots.Add(dot);
                    }
                }
            }

            Game_Manager.dots = dots;

        }
    }

    public static void Morale_Boost(GameObject piece, GameObject card) 
    {
        //Morale Boost(사기증진): 자신 주변 3x3 범위안에 있는 아군한테 모두 공격력 +1 부여
        if(execute) {
            int currX = piece.GetComponent<ChessPiece>().indexX;
            int currY = piece.GetComponent<ChessPiece>().indexY;

            for(int i=0; i<8; i++) {
                int newX = currX + (move_list[i, 0]);
                int newY = currY + (move_list[i, 1]);

                // Check the location is out of bound 
                if(newX > 4 || newX < 0) {
                    continue;
                }
                if(newY > 7 || newY < 0 ) {
                    continue;
                }
                GameObject newCell = cardSave.cells[newX, newY];
                if(newCell.transform.childCount > 0) {
                    GameObject friendlyPiece = newCell.transform.GetChild(0).gameObject;
                    if(piece.GetComponent<ChessPiece>().player == friendlyPiece.GetComponent<ChessPiece>().player) {
                        friendlyPiece.GetComponent<ChessPiece>().offensePower++;
                    }
                }
            }
            Game_Manager.destroyAllIndicators();
            card.GetComponent<dragDrop>().destoryCard();
        }
    }
    public static void Clairvoyant(GameObject piece, GameObject card) 
    {
        //Clairvoyant(천리안): 장착효과, 사거리 무한대로 증가
        if(execute) {
            int currY = piece.GetComponent<ChessPiece>().indexY;
            int newRange = 7 - currY;
            piece.GetComponent<Archer>().attackRange = newRange + 1;
            Game_Manager.destroyAllIndicators();
            card.GetComponent<dragDrop>().destoryCard();
        }
    }
    public static void King_Power(GameObject piece, GameObject card) 
    {
        //King's Power(왕의 일격): 범위에 있는 적 모두 섬멸
        //     x     x     x
        //       x   x   x
        //         x x x
        //           k
        
        List<GameObject> dots = new List<GameObject>();
        if(!execute){
            for(int i=1; i<4; i++) {
                
                for(int j = -i; j<i+1; j+=i){
                    int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[2, 0]) + j;
                    int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[2, 1])*i;

                    // Check the location is out of bound 
                    if(newX_strike > 4 || newX_strike < 0) {
                        continue;
                    }
                    if(newY_strike > 7 || newY_strike < 0 ) {
                        continue;
                    }

                    Debug.Log("NewX: " + newX_strike + " NewY: " + newY_strike);
                    GameObject newCell_Strike = cardSave.cells[newX_strike, newY_strike ];
                    if(newCell_Strike.gameObject.transform.childCount > 0) {
                        GameObject enemyPiece = newCell_Strike.transform.GetChild(0).gameObject;
                        if(enemyPiece.GetComponent<ChessPiece>().player != piece.GetComponent<ChessPiece>().player) { 
                            // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                            dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell_Strike, newCell_Strike.transform.GetChild(0).gameObject, card));
                        }
                    }
                }
            }
            Game_Manager.dots = dots;
        }else {
            Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
            foreach(GameObject strike_dot in Game_Manager.dots) {
                GameObject destoryPiece = strike_dot.transform.parent.GetChild(0).gameObject;
                int st_self = piece.GetComponent<ChessPiece>().offensePower;
                int hp_enemy = destoryPiece.GetComponent<ChessPiece>().defensePower;
                if(st_self < hp_enemy) {
                    destoryPiece.GetComponent<ChessPiece>().defensePower = hp_enemy - st_self;
                    continue;
                }
                UnityEngine.Object.Destroy(destoryPiece);
            }   
            Game_Manager.destroyAlldots();
            Game_Manager.destroyAllIndicators();
        }
    }

    public static void Double_Shot(GameObject piece, GameObject card) 
    
    {
        //Double Shot(더블 샷): 가로세로 방향중 두 방향으로 한발씩 쏨. 
        if(execute) {
            piece.GetComponent<Archer>().numEnemy = 0;
            List<int[]> basic_moves = piece.GetComponent<ChessPiece>().basic_moves;
            piece.GetComponent<Archer>().onlyAttack = true;
            piece.GetComponent<Archer>().createDots_Archer(basic_moves, card);
            strikeController.numAttack = 2;
        }

    }

    public static void Penetrating_Arrow(GameObject piece, GameObject card)
    {
        //Penetrating Arrow(관통 화살): 공격력+1로 화살을 발사. 
        if(execute){
            piece.GetComponent<ChessPiece>().offensePower++;
            Game_Manager.destroyAllIndicators();
            card.GetComponent<dragDrop>().destoryCard();
        }
    }

    public static void Switch_Teleport(GameObject piece, GameObject card)
    {
        // Switch Teleport(위치교환): 아군 말과 자신의 위치를 바꾼다. 
        // 아군을 모두 찾고 -> 찾은 모든 아군에 생성한 인디케이터를 차일드로 set
        // 

        if(!execute) {
            for(int i=0; i<5; i++) {
                for(int j=0; j<8; j++) {
                    GameObject cell = cardSave.cells[i, j];
                    if(cell.transform.childCount <= 0 ) {
                        continue;
                    }
                    
                }
            }
        }
    }

    public static void Disguise(GameObject piece, GameObject card)
    {
        //Disguise(변장): 아군 말 1개와 위치&모습을 바꾼다.

    }

    public static void Testudo(GameObject piece, GameObject card)
    {
        //Testudo(방패진형): 자신 주변 3x3 범위안에 있는 아군한테 모두 방패 부여
    }

    public static void Royal_Dagger(GameObject piece, GameObject card)
    {
        //Royal Dagger(왕실의 단도): 주위에 있는 적 한개 공격
    }

    public static void Losing_Sacrifice(GameObject piece, GameObject card)
    {
        //Losing Sacrifice(대의를 위한 희생): 아군 말 2개를 희생해서 상대 말 1개를 즉시 죽임(범위제한 없음)
    }

    public static void Rage_Attack(GameObject piece, GameObject card) 
    {
        //Rage Attack(분노의 일격): 방어력을 무시하고 공격
    }

    public static void Last_Ditch_Effor(GameObject piece, GameObject card)
    {
        //Last-Ditch Effort(최후의 발악): 왕이 3턴통안 무적 상태가 된다. 3턴 후에는 즉시 죽는다. 
    }

    public static void Demotion_Punishment(GameObject piece, GameObject card)
    {
        //Demotion Punishment(강등 처벌): 아군 말 한개를 무작위 등급으로 강등시킨다.
    }

    public static void Diagonal_Shot(GameObject piece, GameObject card)
    {
        //Diagonal Shot(대각선 발사): 대각선 방향으로 공격
    }

    public static void Disarm(GameObject piece, GameObject card)
    {
        //Disarm(무장해제): 상대방 말 한개의 모든 강화효과를 제거한다 (어느 위치에 있든). 
    }

    public static void Longbow_Shot(GameObject piece, GameObject card)
    {
        //Longbow Shot(장궁 발사): 말 한개를 건너뛰고 공격
    }

    public static void Spear_Throw(GameObject piece, GameObject card)
    {
        //Spear Throw(창 투척): 사거리+1
    }

    public static void Shield(GameObject piece, GameObject card)
    {
        //Warrior's Shield(전사의 방패): 방패+1
    }
}
