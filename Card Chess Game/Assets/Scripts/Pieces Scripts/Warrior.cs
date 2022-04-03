using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class Warrior : MonoBehaviour {

    public int offensePower = 1;
    public int defensePower = 1;

    public List<GameObject> createIndicator() {
        // 검사-이동: 상하좌우 1칸
        List<GameObject> indicators = new List<GameObject>();
        for (int i = 0; i < 4; i++) {
            int newIndexX = GetComponent<ChessPiece>().indexX + (cardSave.position[i,0]);
            int newIndexY = GetComponent<ChessPiece>().indexY + (cardSave.position[i,1]);
            if(newIndexX > 4 || newIndexX < 0) {
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                continue;
            }
            GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                if(newCell.transform.GetChild(0).name == "dot_move(Clone)") {
                    //break;
                } else {
                    if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
                        // 말이 적일 경우
                        indicators.Add(createStrike(newCell, newIndexX, newIndexY));
                    }
                    continue;
                }
            }

            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;

            //newCell.GetComponent<Image>().color = Color.black;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            indicators.Add(dot);
            //this.indexX = newIndexX;
            //this.indexY = newIndexY;
        }
        return indicators;
    }

    public GameObject createStrike(GameObject cell, int indexX, int indexY) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        return striking;
    }
}
