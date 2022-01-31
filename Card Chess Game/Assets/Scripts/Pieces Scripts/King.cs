using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class King : ChessPiece {

    public King(int player, cardSave.Piece type, GameObject obj, int indexX, int indexY) : base(player, type, obj, indexX, indexY) {}


    public override void createDotMove() {
        Debug.Log("createDotMove from King");
        // 왕-이동: 상하좌우대각선 1칸
        for (int i = 0; i < 8; i++) {
            int newIndexX = indexX + (cardSave.position[i,0]);
            int newIndexY = indexY + (cardSave.position[i,1]);
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
}
