using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect
{
    public static async void Knights_Move(GameObject piece) {
        
        int [,] move_list = {{1, 2}, {2, 1}}; 

        for(int i = 0; i < 2; i++) {
            for(int j = 0; j < 2; j++) {
                for(int k = 0; k < move_list.GetLength(0); k++) {
                    int diffX = move_list[k, 0];
                    int diffY = move_list[k, 1];
                    if(i == 1) {
                        diffX *= -1; 
                    }
                    if(j == 1) {
                        diffY *= -1; 
                    }
                    int newIndexX = piece.GetComponent<ChessPiece>().indexX + 
                }
            }
        }

        // List<GameObject> dots = new List<GameObject>();
        // for (int i = 0; i < basic_moves.GetLength(0); i++) {
        //     int newIndexX = GetComponent<ChessPiece>().indexX + (basic_moves[i,0]);
        //     int newIndexY = GetComponent<ChessPiece>().indexY + (basic_moves[i,1]);
        //     if(newIndexX > 4 || newIndexX < 0) {
        //         continue;
        //     }
        //     if(newIndexY > 7 || newIndexY < 0 ) {
        //         continue;
        //     }
        //     GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
        //     if(newCell.gameObject.transform.childCount > 0) {
        //         if(newCell.transform.GetChild(0).name == "dot_move(Clone)") {
        //         } else {
        //             if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
        //                 // 말이 적일 경우
        //                 dots.Add(createStrike(newCell, newIndexX, newIndexY));
        //             }
        //             continue;
        //         }
        //     }

        //     Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
        //     GameObject dot = GameObject.Instantiate(prefab) as GameObject;
        //     dot.transform.SetParent(newCell.transform, false);
        //     dot.transform.position = newCell.transform.position;
        //     dot.GetComponent<dotController>().parent = gameObject; 
        //     dots.Add(dot);
        // }
        // Game_Manager.dots = dots; 
    }
}
