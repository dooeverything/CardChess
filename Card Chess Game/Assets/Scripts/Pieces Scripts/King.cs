using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class King : MonoBehaviour {

    public List<GameObject> createIndicator() {
        Debug.Log("createDotMove from King");

        List<GameObject> indicators = new List<GameObject>();
        // 왕-이동: 상하좌우대각선 1칸
        for (int i = 0; i < 8; i++) {
            //int newIndexX = indexX + (cardSave.position[i,0]);
            //int newIndexY = indexY + (cardSave.position[i,1]);
            int newIndexX = GetComponent<ChessPiece>().indexX + (cardSave.position[i,0]);
            int newIndexY = GetComponent<ChessPiece>().indexY + (cardSave.position[i,1]*2);

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
                if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
                    // 말이 적일 경우
                    indicators.Add(createStrike(newCell, newIndexX, newIndexY));
                }
                continue;
            }

            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;

            //newCell.GetComponent<Image>().color = Color.black;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            //Debug.Log( (i+1) + "th dot: " + newIndexX + " " + newIndexY);
            //player_data.dots.Add(dot);
            //Debug.Log( (i+1) + "th dot is added!");
            //this.indexX = newIndexX;
            //this.indexY = newIndexY;
            indicators.Add(dot);
        }
        return indicators;
    }



    public GameObject createStrike(GameObject cell, int indexX, int indexY) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<strikeController>().indexX = indexX;
        striking.GetComponent<strikeController>().indexY = indexY;
        return striking;
    }
}
