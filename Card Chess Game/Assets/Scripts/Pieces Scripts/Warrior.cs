using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;

public class Warrior : MonoBehaviour {

    public int offensePower = 1;
    public int defensePower = 1;
    public int attackRange = 1;

    // public List<GameObject> W() {
    //     // 검사-이동: 상하좌우 1칸
    //     List<GameObject> indicators = new List<GameObject>();
    //     for (int i = 0; i < 4; i++) {
    //         for (int j = 0; j < attackRange; j++) {
    //             int newIndexX = GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_basic[i,0]*j);
    //             int newIndexY = GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_basic[i,1]*j);
    //             if(newIndexX > 4 || newIndexX < 0) {
    //                 continue;
    //             }
    //             if(newIndexY > 7 || newIndexY < 0 ) {
    //                 continue;
    //             }
    //             GameObject newCell = PieceConfig.cells[newIndexY,newIndexX];
                
    //             if(newCell.gameObject.transform.childCount > 0) {
    //                 if(newCell.transform.GetChild(0).name == "dot_move(Clone)") {
    //                     //break;
    //                 } else {
    //                     if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
    //                         // 말이 적일 경우
    //                         indicators.Add(createStrike(newCell, newIndexX, newIndexY));
    //                     }
    //                     continue;
    //                 }
    //             }

    //             GameObject dot = Helper.prefabNameToGameObject(Prefab.Dot_Move.ToString());

    //             //newCell.GetComponent<Image>().color = Color.black;
    //             dot.transform.SetParent(newCell.transform, false);
    //             dot.transform.position = newCell.transform.position;
    //             indicators.Add(dot);
    //             //this.indexX = newIndexX;
    //             //this.indexY = newIndexY;
    //         }
    //     }
    //     return indicators;
    // }
    public GameObject createStrike(GameObject cell, GameObject enemy, GameObject card, GameObject parent) {
        GameObject attacking = Helper.prefabNameToGameObject(Prefab.Attacking.ToString());
        attacking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        attacking.transform.position = cell.transform.position;
        attacking.GetComponent<StrikeController>().enemy = enemy;
        attacking.GetComponent<StrikeController>().card = card;
        attacking.GetComponent<StrikeController>().parent = parent;

        return attacking;
    }
}
