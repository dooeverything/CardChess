using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    GameObject canvas;
    GameObject hands;
    public static bool beingHeld;
    private GameObject pieces;
    public static string pieceName;
    public static string cardName;
    void Start() {
        canvas = GameObject.Find("Canvas");
        hands = GameObject.Find("Hands");
        pieces = GameObject.Find("Chess Piece");
        cardName = this.gameObject.name;
    }
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        transform.SetParent(canvas.transform);
        beingHeld = true;
    }
    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;
    }
    public static bool selected = false;
    public static int obj_id;
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        foreach(Transform child in pieces.transform) {
            if(child.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log(child.gameObject.name + " will move or attack");
                pieceName = child.gameObject.name;
                selected = true;
                obj_id = child.GetInstanceID();
                Destroy(gameObject);
            }
        }
        transform.SetParent(hands.transform);
        beingHeld = false;
    }
}