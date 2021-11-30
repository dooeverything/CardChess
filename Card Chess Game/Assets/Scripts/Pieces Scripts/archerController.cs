using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class archerController : horseController {
    // Move: createDot for archer
    protected override void createDotMove(Object prefab) {
        Debug.Log("createDotMove from archer");
        // 궁수-이동: 상하좌우 2칸
        for (int i = 0; i < 4; i++) {
            GameObject dot = Instantiate(prefab) as GameObject;
            dot.transform.SetParent(moveIndicator.transform, false);
            dot.transform.position = new Vector2(transform.position.x + cardSave.positionMove[i, 0] * 160 * 2, transform.position.y + cardSave.positionMove[i, 1] * 160 * 2);
            float x = dot.transform.position.x;
            float y = dot.transform.position.y;
            // Debug.Log("Xpos is: " + x);
            // Debug.Log("recttransform is: " + dot.GetComponent<RectTransform>());
            // Debug.Log("position is: " + new Vector2(transform.position.x + cardSave.positionMove[i, 0] * 160 * 2, transform.position.y + cardSave.positionMove[i, 1] * 160 * 2)); 
            Debug.Log("x: " + (transform.position.x + cardSave.positionMove[i, 0] * 160 * 2));
            if(x < 220 || x > 860) {
                Destroy(dot);
            } 
        }     
    }

    protected override void createDotStrike(Object prefab) {
        Debug.Log("striking from archer!!");
        float[,] ratio = { {-320f, 0}, {0, -570f}, {320f, 0}, {0, 550f} };
        // 궁수-공격: 상하좌우 전체
        for(int i = 0; i < 4; i++) {
            GameObject dot = Instantiate(prefab) as GameObject;
            dot.transform.SetParent(moveIndicator.transform, false);
            dot.transform.position = new Vector2(transform.position.x + ratio[i, 0], transform.position.y + ratio[i, 1]);
        }
    }

    void Update() {
        // if the piece has a child (if a piece has selected indicators), it is ready to move to different location
        if(transform.childCount > 0) {
            clickToMove(gameObject);
        }

        // if the card is Strike_Archer
        if(dragDrop.cardName == "cardTest(Clone)") {
            if (dragDrop.beingHeld) {
                gameObject.GetComponent<Image>().color = Color.red; // indicator of strike is red
            }else {
                gameObject.GetComponent<Image>().color = Color.white;

                if (dragDrop.selected) {
                    if (dragDrop.obj_id == transform.GetInstanceID()) {
                        // Create a selected indicator prefab
                        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
                        indicator = Instantiate(selected) as GameObject;
                        indicator.transform.SetParent(this.gameObject.transform);
                        indicator.transform.position = transform.position;

                        // Create a dot-strike_indicator prefab 
                        prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_strike.prefab", typeof(GameObject));

                        // If the selected object is Archer
                        // use of inheritance and overriding
                        createDotStrike(prefab);

                        dragDrop.selected = false;
                    }
                }
            }
        // if the card is Move_Archer
        } else if (dragDrop.cardName == "cardTest4(Clone)") { 
            if (dragDrop.beingHeld) {
                gameObject.GetComponent<Image>().color = Color.green; // indicator of move is green
            }else {
                gameObject.GetComponent<Image>().color = Color.white;

                if (dragDrop.selected) {
                    if (dragDrop.obj_id == transform.GetInstanceID()) {
                        // Create a selected indicator prefab
                        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
                        indicator = Instantiate(selected) as GameObject;
                        indicator.transform.SetParent(this.gameObject.transform);
                        indicator.transform.position = transform.position;

                        // Create a dot-move_indicator prefab 
                        prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));

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
