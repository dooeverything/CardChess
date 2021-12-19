using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class Warrior : ChessPiece {
    
    public Warrior(cardSave.Piece type, GameObject obj, int indexX, int indexY) : base(type, obj, indexX, indexY) {}

    // protected override void createDotMove(Object prefab) {
    //     // 이동/공격 상하좌우 1칸
    //     for (int i = 0; i < 4; i++) {
    //         GameObject dot = GameObject.Instantiate(prefab) as GameObject;
    //         dot.transform.SetParent(moveIndicator.transform, false);
    //         GameObject cell = cardSave.cells[indexX+cardSave.positionMove[i,0], indexY+cardSave.positionMove[i,1]];
    //         dot.transform.position = new Vector2(cell.transform.position.x, cell.transform.position.y);
    //     }
    // }

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
