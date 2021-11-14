using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class warriorController : horseController {
    protected override void createDotMove(Object prefab) {
        // 이동/공격 상하좌우 1칸
        for (int i = 0; i < 4; i++) {
            GameObject dot = Instantiate(prefab) as GameObject;
            dot.transform.SetParent(moveIndicator.transform, false);
            dot.transform.position = new Vector2(transform.position.x + cardSave.positionMove[i, 0] * 160, transform.position.y + cardSave.positionMove[i, 1] * 160);
        }
    }

    void Update() {
        // if the piece has a child (selected indicator)
        if(transform.childCount > 0) {
            clickToMove(gameObject);
        }

        if(dragDrop.cardName == "cardTest3(Clone)" || dragDrop.cardName == "cardTest6(Clone)") {
            if (dragDrop.beingHeld) {
                gameObject.GetComponent<Image>().color = Color.blue;
            }else {
                gameObject.GetComponent<Image>().color = Color.white;

                if (dragDrop.selected) {
                    if (dragDrop.obj_id == transform.GetInstanceID()) {
                        // Create a selected indicator prefab
                        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
                        indicator = Instantiate(selected) as GameObject;
                        indicator.transform.SetParent(this.transform);
                        indicator.transform.position = transform.position;

                        // Create a dot move indicator prefab 
                        prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot.prefab", typeof(GameObject));

                        // If the selected object is Archer
                        // use of inheritance and overriding
                        createDotMove(prefab);

                        dragDrop.selected = false;
                    }
                }
            }
        }
    }
}
