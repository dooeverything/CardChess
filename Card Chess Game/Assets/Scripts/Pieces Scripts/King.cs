using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class King : MonoBehaviour {
    public int defensePower = 1;
    public void createDots() {
        List<GameObject> dots = new List<GameObject>();
        // 왕-이동: 상하좌우대각선 1칸
        for (int i = 0; i < 8; i++) {
            //int newIndexX = indexX + (CardSave.position[i,0]);
            //int newIndexY = indexY + (CardSave.position[i,1]);
            int newIndexX = GetComponent<ChessPiece>().indexX + (CardSave.position[i,0]);
            int newIndexY = GetComponent<ChessPiece>().indexY + (CardSave.position[i,1]);

            if(newIndexX > 4 || newIndexX < 0) {
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                continue;
            }
            GameObject newCell = CardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                continue;
            }

            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            dot.GetComponent<dotController>().parent = gameObject; 
            dots.Add(dot);
        }
        GameManager.dots = dots; 
    }
}
