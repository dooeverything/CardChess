using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // To find the mouse position in UI
    GameObject canvas;
    GameObject hands;
    public static bool beingHeld;
    public GameObject pieces;
    public static string pieceName;
    void Start() {
        canvas = GameObject.Find("Canvas");
        hands = GameObject.Find("Hands");
        pieces = GameObject.Find("Chess Piece");
    }
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        transform.SetParent(canvas.transform);
        beingHeld = true;
    }

    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;
    }
    public static bool moved = false;
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        foreach(Transform child in pieces.transform) {
            if(child.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log(child.gameObject.name + " will be moved");
                pieceName = child.gameObject.name;
                moved = true;
                Destroy(gameObject);
            }
        }
        
            //when card is dropped to horse piece

            /*indicator.transform.position = horsePiece1.transform.position;
            indicator.transform.SetParent(horsePiece1.transform);
            //horsePiece1.transform.position = new Vector2(horsePiece1.transform.position.x, horsePiece1.transform.position.y+160);

            // in case of moving archer
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot.prefab", typeof(GameObject));
            GameObject dot = Instantiate(prefab) as GameObject;
            dot.transform.SetParent(moveIndicator.transform, false);
            GameObject dot_2 = Instantiate(prefab) as GameObject;
            dot_2.transform.SetParent(moveIndicator.transform, false);
            dot.transform.position = new Vector2(horsePiece1.transform.position.x+160, horsePiece1.transform.position.y);
            dot_2.transform.position = new Vector2(horsePiece1.transform.position.x, horsePiece1.transform.position.y+160);*/

        //else if(horsePiece2.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
        //     Debug.Log("Card will be destroyed and horsepiece2 will move");
        //     indicator.transform.position = horsePiece2.transform.position;
        //     indicator.transform.SetParent(horsePiece2.transform);
        //     Destroy(gameObject);
        //}
        transform.SetParent(hands.transform);
        beingHeld = false;
    }

    /*public void Update() {
        clickToMove();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void clickToMove() {
        if (Input.GetKey(KeyCode.Mouse0)) {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);
            
            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                Debug.Log(result.gameObject.name);
                if(result.gameObject.name == "dot(Clone)") {
                    horsePiece1.transform.position = result.gameObject.transform.position;
                    indicator.transform.position = new Vector2(-1000, 1000);
                    
                    foreach(Transform child in moveIndicator.transform) {
                        Destroy(child.gameObject);
                    }                    
                }
            }
        }
    }*/
}

// create selected indicator -->
            // selected = Resources.Load<Sprite>("Assets/Image/selected picture.png");
            // selectedObject.AddComponent(typeof(Image));
            // selectedObject.GetComponent<Image>().sprite = selected;
            // selectedObject.transform.position = gameObject.transform.position;
            // selectedObject.transform.localScale = new Vector2(0.3f, 0.3f);            
