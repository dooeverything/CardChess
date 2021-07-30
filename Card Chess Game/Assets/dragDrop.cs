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

    public static int obj_id;
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        foreach(Transform child in pieces.transform) {
            if(child.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log(child.gameObject.name + " will be moved");
                pieceName = child.gameObject.name;
                moved = true;
                obj_id = child.GetInstanceID();
                Destroy(gameObject);
            }
        }
        transform.SetParent(hands.transform);
        beingHeld = false;
    }
}