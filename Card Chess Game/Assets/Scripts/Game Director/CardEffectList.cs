using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System; 

public class CardEffect
{
    public static int [,] move_list = {{1, 0}, {1, 1}, {0, 1}, {-1, 1}, {-1, 0}, {-1, -1}, {0, -1}, {1, -1}}; 

    public static bool execute = false; 

    public static void Knights_Move(GameObject piece, GameObject card) {
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
        if(execute) {
            piece.GetComponent<ChessPiece>().offensePower++;
            piece.GetComponent<ChessPiece>().defensePower++;
            Game_Manager.destroyAllIndicators();
            card.GetComponent<dragDrop>().destoryCard();
        }
    }

    public static void Teleport(GameObject piece, GameObject card)
    {
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
}
