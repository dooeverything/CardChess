using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class Archer : ChessPiece {
    
    //Constructor for Archer
    public Archer(int player, cardSave.Piece type, GameObject obj, int indexX, int indexY) : base(player, type, obj, indexX, indexY) {}

    // Move: createDot for archer
    public override void createDotMove() {
        //Debug.Log("createDotMove from archer");
        // 궁수-이동: 상하좌우 2칸
        for (int i = 0; i < 4; i++) {
            int newIndexX = indexX + (cardSave.position[i,0]*2);
            int newIndexY = indexY + (cardSave.position[i,1]*2);
            if(newIndexX > 4 || newIndexX < 0) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " is out of bound");
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " is out of bound");
                continue;
            }
            GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " has children");
                continue;
            }

            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
            //newCell.GetComponent<Image>().color = Color.black;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            //Debug.Log( (i+1) + "th dot: " + newIndexX + " " + newIndexY);
            player_data.dots.Add(dot);
            //Debug.Log( (i+1) + "th dot is added!");
            //this.indexX = newIndexX;
            //this.indexY = newIndexY;
        }
    }

    public override void createDotStrike() {
        //Debug.Log("createDotMove from archer");
        // 궁수-이동: 상하좌우 2칸
        for (int i = 0; i < 4; i++) {
            int newIndexX = indexX + (cardSave.position[i,0]*4);
            int newIndexY = indexY + (cardSave.position[i,1]*4);
            if(newIndexX > 4 || newIndexX < 0) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " is out of bound");
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " is out of bound");
                continue;
            }
            GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                //Debug.Log( (i+1) + " th: " + newIndexX + " " + newIndexY + " has children");
                if(newCell.transform.GetChild(0).GetComponent<PieceController>().player == player) {
                    continue;                
                }else {
                    createStrike(newCell, newIndexX, newIndexY);
                    continue;
                }
            }
            
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_strike.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
            //newCell.GetComponent<Image>().color = Color.black;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            //Debug.Log( (i+1) + "th dot: " + newIndexX + " " + newIndexY);
            player_data.dots.Add(dot);
            //Debug.Log( (i+1) + "th dot is added!");
            //this.indexX = newIndexX;
            //this.indexY = newIndexY;
        }
    }
}
