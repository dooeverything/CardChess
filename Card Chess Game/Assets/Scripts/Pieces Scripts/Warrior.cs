using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class Warrior : ChessPiece {
    
    public Warrior(int player, cardSave.Piece type, GameObject obj, int indexX, int indexY) : base(player, type, obj, indexX, indexY) {}

    public override void createDotMove() {
        //Debug.Log("createDotMove from Warrior");
        // 검사-이동: 상하좌우 1칸
        for (int i = 0; i < 4; i++) {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
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
        //Debug.Log("createDotStrike from Warrior");
        // 검사-공격: 상하좌우 1칸
        for (int i = 0; i < 4; i++) {
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_strike.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
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


    // void Update() {
    //     // if the piece has a child (selected indicator)
    //     if(gameObject.transform.childCount > 0) {
    //         clickToMove(gameObject);
    //     }

    //     if(dragDrop.cardName == "cardTest3(Clone)" || dragDrop.cardName == "cardTest6(Clone)") {
    //         if (dragDrop.beingHeld) {
    //             gameObject.GetComponent<Image>().color = Color.blue;
    //         }else {
    //             gameObject.GetComponent<Image>().color = Color.white;

    //             if (dragDrop.selected) {
    //                 if (dragDrop.obj_id == transform.GetInstanceID()) {
    //                     // Create a selected indicator prefab
    //                     Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
    //                     indicator = Instantiate(selected) as GameObject;
    //                     indicator.transform.SetParent(this.transform);
    //                     indicator.transform.position = transform.position;

    //                     // Create a dot move indicator prefab 
    //                     prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot.prefab", typeof(GameObject));

    //                     // If the selected object is Archer
    //                     // use of inheritance and overriding
    //                     createDotMove(prefab);

    //                     dragDrop.selected = false;
    //                 }
    //             }
    //         }
    //     }
    // }
}
