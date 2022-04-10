using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class Archer : MonoBehaviour {
    public int offensePower = 1;
    public int defensePower = 1;
    public int attackRange = 4;
    public int numEnemy = 0;
    public bool onlyAttack = false;
    public GameObject create_strikeDot(GameObject cell, GameObject enemy, GameObject card, GameObject parent) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<strikeController>().enemy = enemy;
        striking.GetComponent<strikeController>().card = card;
        striking.GetComponent<strikeController>().moveWhenAttack = false;
        striking.GetComponent<strikeController>().parent = parent;
        return striking;
    }

    public GameObject create_moveDot(GameObject newCell, GameObject card) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
        GameObject dot = GameObject.Instantiate(prefab) as GameObject;
        dot.transform.SetParent(newCell.transform, false);
        dot.transform.position = newCell.transform.position;
        dot.GetComponent<dotController>().parent = gameObject; 
        dot.GetComponent<dotController>().card = card;
        return dot;
    }

    public void createDots_Archer(List<int[]> move_list, GameObject card = null)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            for(int j = 1; j<attackRange; j++) {
                int newX_strike = GetComponent<ChessPiece>().indexX + (move_list[i][0]*j);
                int newY_strike = GetComponent<ChessPiece>().indexY + (move_list[i][1]*j);
                
                // Check the location is out of bound 
                if(newX_strike > 4 || newX_strike < 0) {
                    continue;
                }
                if(newY_strike > 7 || newY_strike < 0 ) {
                    continue;
                }

                GameObject newCell_Stirke = CardSave.cells[newX_strike, newY_strike ];
                if(newCell_Stirke.gameObject.transform.childCount > 0) {
                    //if(newCell_Stirke.gameObject.transform.GetChild(0).name == "dot_move(Clone)" ) continue;
                    
                    // The enemy's Piece is located within the range of archer's attack, then create a strike dot    
                    if(newCell_Stirke.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) { 
                        dots.Add(create_strikeDot(newCell_Stirke, newCell_Stirke.transform.GetChild(0).gameObject, card, this.gameObject));
                        numEnemy++;
                    }

                    // If there is any blocking piece, then further iteration is no needed 
                    // (the piece further than blocking piece cannot be attacked)
                    break; 
                }else if(!onlyAttack){
                    // If there is no blocking piece on the location where archer can move to
                    if(j==1){
                        dots.Add(create_moveDot(newCell_Stirke, card));
                    }
                }
            }
        }
        strikeController.numAttack = System.Math.Min(2, numEnemy); 
        onlyAttack = false;
        Game_Manager.dots = dots; 
    }

}
