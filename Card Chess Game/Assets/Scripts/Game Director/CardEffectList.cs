// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// using System; 

// public class CardEffect
// {

//     public static int[,] move_list = { { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 }, { -1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };

//     public static bool execute = false;

//     public static bool Knights_Move(GameObject piece, GameObject card)
//     {
//         if (!execute)
//         {
//             int[,] move_list_core = { { 1, 2 }, { 2, 1 } };
//             List<int[]> move_list = new List<int[]>();
//             List<GameObject> dots = new List<GameObject>();

//             for (int i = 0; i < 2; i++)
//             {
//                 for (int j = 0; j < 2; j++)
//                 {
//                     for (int k = 0; k < move_list_core.GetLength(0); k++)
//                     {
//                         int diffX = move_list_core[k, 0];
//                         int diffY = move_list_core[k, 1];
//                         if (i == 1)
//                         {
//                             diffX *= -1;
//                         }
//                         if (j == 1)
//                         {
//                             diffY *= -1;
//                         }
//                         move_list.Add(new int[] { diffX, diffY });
//                     }
//                 }
//             }
//             piece.GetComponent<ChessPiece>().createDots(move_list, card);
//         }
//         return true; // later to be changed 
//     }

//     public static bool ArrowOfMadness(GameObject piece, GameObject card)
//     {
//         if (!execute)
//         {
//             List<GameObject> dots = new List<GameObject>();
//             for (int i = 0; i < 8; i++)
//             {
//                 for (int j = 1; j < 8; j++)
//                 {
//                     int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[i, 0] * j);
//                     int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[i, 1] * j);

//                     // Check the location is out of bound 
//                     if (newX_strike > 4 || newX_strike < 0)
//                     {
//                         continue;
//                     }
//                     if (newY_strike > 7 || newY_strike < 0)
//                     {
//                         continue;
//                     }

//                     GameObject newCell_Stirke = CardSave.cells[newX_strike, newY_strike];
//                     if (newCell_Stirke.gameObject.transform.childCount > 0)
//                     {
//                         //if(newCell_Stirke.gameObject.transform.GetChild(0).name == "dot_move(Clone)" ) continue;

//                         // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
//                         dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell_Stirke, newCell_Stirke.transform.GetChild(0).gameObject, card));

//                         break; // If there is any blocking piece, then further iteration is no needed (the piece further than the blocking piece cannot be attacked)
//                     }
//                 }
//             }
//             Game_Manager.dots = dots;
//         }
//         else
//         {
//             Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
//             foreach (GameObject strike_dot in Game_Manager.dots)
//             {
//                 GameObject destoryPiece = strike_dot.transform.parent.GetChild(0).gameObject;
//                 int st_self = piece.GetComponent<ChessPiece>().offensePower;
//                 int hp_enemy = destoryPiece.GetComponent<ChessPiece>().defensePower;
//                 if (st_self < hp_enemy)
//                 {
//                     destoryPiece.GetComponent<ChessPiece>().defensePower = hp_enemy - st_self;
//                     continue;
//                 }
//                 UnityEngine.Object.Destroy(destoryPiece);
//             }
//             Game_Manager.destroyAlldots();
//             Game_Manager.destroyAllIndicators();
//         }
//         return true; 
//     }

//     public static bool Rush(GameObject piece, GameObject card)
//     {
//         //Rush(돌진): 3칸 앞으로 전진하면서 경로에 있는 말 모두 제거

//         bool move_available = false; 
//         List<GameObject> dots = new List<GameObject>();
//         if (!execute)
//         {
//             for (int i = 1; i < 4; i++)
//             {
//                 int newX_strike = piece.GetComponent<ChessPiece>().indexX;
//                 int newY_strike = piece.GetComponent<ChessPiece>().indexY + i * piece.GetComponent<ChessPiece>().move_dir;

//                 // Check the location is out of bound 
//                 if (newX_strike > 4 || newX_strike < 0)
//                 {
//                     continue;
//                 }
//                 if (newY_strike > 7 || newY_strike < 0)
//                 {
//                     continue;
//                 }
//                 Debug.Log($"Index is {i}");
//                 move_available = true; 
//                 GameObject newCell = CardSave.cells[newX_strike, newY_strike];

//                 if (newCell.gameObject.transform.childCount > 0)
//                 {
//                     // If a piece of any side is located within the range of archer's attack, then create a strike dot    
//                     dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell, newCell.transform.GetChild(0).gameObject, card));
//                 }
//                 else {
//                     dots.Add(piece.GetComponent<ChessPiece>().createDot(newCell, card));
//                 }
//             }
//             Game_Manager.dots = dots;
//             return move_available; 
//         }
//         else
//         {
//             if (Game_Manager.dots.Count > 0)
//             {
//                 Game_Manager.dots[0].GetComponent<strikeController>()?.deleteCard();
//                 Game_Manager.dots[0].GetComponent<dotController>()?.deleteCard();
//                 foreach (GameObject dot in Game_Manager.dots)
//                 {

//                     dot.GetComponent<strikeController>()?.moveParent();
//                     dot.GetComponent<dotController>()?.moveParent();
//                     UnityEngine.Object.Destroy(dot.transform.parent.GetChild(0).gameObject);
//                 }
//                 endButtonController.switchTurn(); 
//             }
//             Game_Manager.destroyAlldots();
//             Game_Manager.destroyAllIndicators();
//             return true; 
//         }
//     }

//     public static bool Heavy_Armor(GameObject piece, GameObject card)
//     {
//         if (execute)
//         {
//             piece.GetComponent<ChessPiece>().offensePower++;
//             piece.GetComponent<ChessPiece>().defensePower++;
//             Game_Manager.destroyAllIndicators();
//             card.GetComponent<dragDrop>().destoryCard();
//         }
//         return true; 
//     }

//     public static bool Teleport(GameObject piece, GameObject card)
//     {
//         bool move_available = false;
//         List<GameObject> dots = new List<GameObject>();
//         if (!execute)
//         {
//             for (int i = 0; i < 5; i++)
//             {
//                 for (int j = 0; j < 8; j++)
//                 {
//                     GameObject cell = CardSave.cells[i, j];
//                     if (cell.transform.childCount > 0)
//                     {
//                         continue;
//                     }
//                     move_available = true; 
//                     UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
//                     GameObject dot = GameObject.Instantiate(prefab) as GameObject;
//                     dot.transform.SetParent(cell.transform, false);
//                     dot.transform.position = cell.transform.position;
//                     dot.GetComponent<dotController>().parent = piece;
//                     dot.GetComponent<dotController>().card = card;
//                     dots.Add(dot);
//                 }
//             }
//             Game_Manager.dots = dots;
//             return move_available; 
//         }
//         return true; 
//     }

//     public static bool Thunder_Bolt(GameObject piece, GameObject card)
//     {
//         bool move_available = false; 
//         List<GameObject> dots = new List<GameObject>();
//         if (!execute)
//         {

//             for (int i = 0; i < 8; i++)
//             {
//                 int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[i, 0]);
//                 int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[i, 1]);

//                 // Check the location is out of bound 
//                 if (newX_strike > 4 || newX_strike < 0)
//                 {
//                     continue;
//                 }
//                 if (newY_strike > 7 || newY_strike < 0)
//                 {
//                     continue;
//                 }
//                 move_available = true; 

//                 GameObject newCell_Strike = CardSave.cells[newX_strike, newY_strike];
//                 if (newCell_Strike.transform.childCount > 0)
//                 {
//                     GameObject enemyPiece = newCell_Strike.transform.GetChild(0).gameObject;
//                     if (enemyPiece.GetComponent<ChessPiece>().player != piece.GetComponent<ChessPiece>().player)
//                     {
//                         UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/ThunderBolt.prefab", typeof(GameObject));
//                         GameObject dot = GameObject.Instantiate(prefab) as GameObject;
//                         dot.transform.SetParent(newCell_Strike.transform, false);
//                         dot.transform.position = newCell_Strike.transform.position;
//                         dot.GetComponent<thunderBoltStrikeController>().parent = piece;
//                         dot.GetComponent<thunderBoltStrikeController>().card = card;
//                         dots.Add(dot);
//                     }
//                 }
//             }

//             Game_Manager.dots = dots;
//             return move_available;
//         }
//         return true; 
//     }
//     public static bool Rage_Attack(GameObject piece, GameObject card)
//     {
//         Debug.Log("Rage Attack Called"); 
//         if (!execute)
//         {
//             bool move_available = false; 
//             List<GameObject> dots = new List<GameObject>();
//             // int[] coordinates = (piece.GetComponent<ChessPiece>().player == 1) ? new int[]{0, 1} : new int[]{0, -1}; 
//             int newIndexX = piece.GetComponent<ChessPiece>().indexX;  
//             int newIndexY = piece.GetComponent<ChessPiece>().indexY + piece.GetComponent<ChessPiece>().move_dir;
//                 if(newIndexX > 4 || newIndexX < 0) {
//                     goto GOTO1;
//                 }
//                 if(newIndexY > 7 || newIndexY < 0 ) {
//                     goto GOTO1;
//                 }
//                 move_available = true; 
//                 GameObject newCell = CardSave.cells[newIndexX, newIndexY];
//                 if (newCell.gameObject.transform.childCount > 0)
//                 {
//                     // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
//                     dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell, newCell.transform.GetChild(0).gameObject, card));
//                 }
//             Game_Manager.dots = dots; 
//             GOTO1: return move_available; 
//         } else {
//             if (Game_Manager.dots.Count > 0)
//             {
//                 Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
//                 foreach (GameObject strike_dot in Game_Manager.dots)
//                 {
//                     strike_dot.GetComponent<strikeController>().moveParent();
//                     UnityEngine.Object.Destroy(strike_dot.transform.parent.GetChild(0).gameObject);
//                 }
//                 endButtonController.switchTurn(); 
//             }
//             Game_Manager.destroyAlldots();
//             Game_Manager.destroyAllIndicators();
//             return true; 
//         }
//     }
//     public static bool Morale_Boost(GameObject piece, GameObject card) 
//     {
//         //Morale Boost(사기증진): 자신 주변 3x3 범위안에 있는 아군한테 모두 공격력 +1 부여
//         if(execute) {
//             int currX = piece.GetComponent<ChessPiece>().indexX;
//             int currY = piece.GetComponent<ChessPiece>().indexY;

//             for(int i=0; i<8; i++) {
//                 int newX = currX + (move_list[i, 0]);
//                 int newY = currY + (move_list[i, 1]);

//                 // Check the location is out of bound 
//                 if(newX > 4 || newX < 0) {
//                     continue;
//                 }
//                 if(newY > 7 || newY < 0 ) {
//                     continue;
//                 }
//                 GameObject newCell = CardSave.cells[newX, newY];
//                 if(newCell.transform.childCount > 0) {
//                     GameObject friendlyPiece = newCell.transform.GetChild(0).gameObject;
//                     if(piece.GetComponent<ChessPiece>().player == friendlyPiece.GetComponent<ChessPiece>().player) {
//                         friendlyPiece.GetComponent<ChessPiece>().offensePower++;
//                     }
//                 }
//             }
//             Game_Manager.destroyAllIndicators();
//             card.GetComponent<dragDrop>().destoryCard();
//         }
//         return true; 
//     }
//     public static bool Clairvoyant(GameObject piece, GameObject card) 
//     {
//         //Clairvoyant(천리안): 장착효과, 사거리 무한대로 증가
//         if(execute) {
//             // int currY = piece.GetComponent<ChessPiece>().indexY;
//             // int newRange = 7 - currY;
//             piece.GetComponent<Archer>().attackRange = 8;
//             Game_Manager.destroyAllIndicators();
//             card.GetComponent<dragDrop>().destoryCard();
//         }
//         return true; 
//     }
//     public static bool King_Power(GameObject piece, GameObject card) 
//     {
//         //King's Power(왕의 일격): 범위에 있는 적 모두 섬멸
//         //     x     x     x
//         //       x   x   x
//         //         x x x
//         //           k
        
//         List<GameObject> dots = new List<GameObject>();
//         if(!execute){
//             for(int i=1; i<4; i++) {
                
//                 for(int j = -i; j<i+1; j+=i){
//                     int newX_strike = piece.GetComponent<ChessPiece>().indexX + (move_list[2, 0]) + j;
//                     int newY_strike = piece.GetComponent<ChessPiece>().indexY + (move_list[2, 1])*i;

//                     // Check the location is out of bound 
//                     if(newX_strike > 4 || newX_strike < 0) {
//                         continue;
//                     }
//                     if(newY_strike > 7 || newY_strike < 0 ) {
//                         continue;
//                     }

//                     GameObject newCell_Strike = CardSave.cells[newX_strike, newY_strike ];
//                     if(newCell_Strike.gameObject.transform.childCount > 0) {
//                         GameObject enemyPiece = newCell_Strike.transform.GetChild(0).gameObject;
//                         if(enemyPiece.GetComponent<ChessPiece>().player != piece.GetComponent<ChessPiece>().player) { 
//                             // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
//                             dots.Add(piece.GetComponent<ChessPiece>().createStrike(newCell_Strike, newCell_Strike.transform.GetChild(0).gameObject, card));
//                         }
//                     }
//                 }
//             }
//             Game_Manager.dots = dots;
//         }else {
//             Game_Manager.dots[0].GetComponent<strikeController>().deleteCard();
//             foreach(GameObject strike_dot in Game_Manager.dots) {
//                 GameObject destoryPiece = strike_dot.transform.parent.GetChild(0).gameObject;
//                 int st_self = piece.GetComponent<ChessPiece>().offensePower;
//                 int hp_enemy = destoryPiece.GetComponent<ChessPiece>().defensePower;
//                 if(st_self < hp_enemy) {
//                     destoryPiece.GetComponent<ChessPiece>().defensePower = hp_enemy - st_self;
//                     continue;
//                 }
//                 UnityEngine.Object.Destroy(destoryPiece);
//             }   
//             Game_Manager.destroyAlldots();
//             Game_Manager.destroyAllIndicators();
//         }
//         return true;
//     }
//     public static bool Double_Shot(GameObject piece, GameObject card) 
    
//     {
//         //Double Shot(더블 샷): 가로세로 방향중 두 방향으로 한발씩 쏨. 
//         if(execute) {
//             piece.GetComponent<Archer>().numEnemy = 0;
//             List<int[]> basic_moves = piece.GetComponent<ChessPiece>().basic_moves;
//             piece.GetComponent<Archer>().onlyAttack = true;
//             piece.GetComponent<Archer>().createDots_Archer(basic_moves, card);
//             strikeController.numAttack = 2;
//         }
//                 return true;

//     }

//     public static bool Penetrating_Arrow(GameObject piece, GameObject card)
//     {
//         //Penetrating Arrow(관통 화살): 공격력+1로 화살을 발사. 
//         if(execute){
//             piece.GetComponent<ChessPiece>().offensePower++;
//             Game_Manager.destroyAllIndicators();
//             card.GetComponent<dragDrop>().destoryCard();
//         }
//                 return true;
//     }

//     public static bool Switch_Teleport(GameObject piece, GameObject card)
//     {
//         // Switch Teleport(위치교환): 아군 말과 자신의 위치를 바꾼다. 
//         // 아군을 모두 찾고 -> 찾은 모든 아군에 생성한 인디케이터를 차일드로 set
//         // 
//         if(!execute) {
//             for(int i=0; i<5; i++) {
//                 for(int j=0; j<8; j++) {
//                     GameObject cell = CardSave.cells[i, j];
//                     if(cell.transform.childCount <= 0 ) {
//                         continue;
//                     }
                    
//                 }
//             }
//         }
//                 return true;
//     }

//     public static bool Disguise(GameObject piece, GameObject card)
//     {
//         //Disguise(변장): 아군 말 1개와 위치&모습을 바꾼다.
//                 return true;

//     }

//     public static bool Testudo(GameObject piece, GameObject card)
//     {
//         //Testudo(방패진형): 자신 주변 3x3 범위안에 있는 아군한테 모두 방패 부여
//                 return true;
//     }

//     public static bool Royal_Dagger(GameObject piece, GameObject card)
//     {
//         //Royal Dagger(왕실의 단도): 주위에 있는 적 한개 공격
//                 return true;
//     }

//     public static bool Losing_Sacrifice(GameObject piece, GameObject card)
//     {
//         //Losing Sacrifice(대의를 위한 희생): 아군 말 2개를 희생해서 상대 말 1개를 즉시 죽임(범위제한 없음)
//                 return true;
//     }

//     public static bool Last_Ditch_Effor(GameObject piece, GameObject card)
//     {
//         //Last-Ditch Effort(최후의 발악): 왕이 3턴통안 무적 상태가 된다. 3턴 후에는 즉시 죽는다. 
//                 return true;
//     }

//     public static bool Demotion_Punishment(GameObject piece, GameObject card)
//     {
//         //Demotion Punishment(강등 처벌): 아군 말 한개를 무작위 등급으로 강등시킨다.
//                 return true;
//     }

//     public static bool Diagonal_Shot(GameObject piece, GameObject card)
//     {
//         //Diagonal Shot(대각선 발사): 대각선 방향으로 공격
//                 return true;
//     }

//     public static bool Disarm(GameObject piece, GameObject card)
//     {
//         //Disarm(무장해제): 상대방 말 한개의 모든 강화효과를 제거한다 (어느 위치에 있든). 
//                 return true;
//     }

//     public static bool Longbow_Shot(GameObject piece, GameObject card)
//     {
//         //Longbow Shot(장궁 발사): 말 한개를 건너뛰고 공격
//                 return true;
//     }

//     public static bool Spear_Throw(GameObject piece, GameObject card)
//     {
//         //Spear Throw(창 투척): 사거리+1
//                 return true;
//     }

//     public static bool Shield(GameObject piece, GameObject card)
//     {
//         //Warrior's Shield(전사의 방패): 방패+1
//         return true;
//     }
// }
