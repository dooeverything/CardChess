using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static bool beingHeld;
    private GameObject pieces;
    public static string pieceName;
    public static string cardName;

    public string pieceType; 
    public string behaviour; 
    Transform hand;
    void Start() {
        cardName = this.gameObject.name;
        hand = this.transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        transform.SetParent(this.transform.root);
        beingHeld = true;
        switch(pieceType) {
            case "archer": 
                if(behaviour == "move") {
                    List<GameObject> temp = Game_Manager.archerOnBoard_player1;  
                    for(int i = 0; i < temp.Count; i++) {
                        temp[i].GetComponent<PieceController>().createIndicator(); 
                    }
                } else {
                    List<GameObject> temp = Game_Manager.archerOnBoard_player1;  
                    for(int i = 0; i < temp.Count; i++) {
                        temp[i].GetComponent<PieceController>().createIndicator(); 
                    }              
                }
                break; 
            default: return; 
        }
    }
    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;
    }
    public static bool selected = false;
    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        List<GameObject> temp = null;
        List<ChessPiece> temp2 = null;

        // Get information about a card
        switch(pieceType) {
            case "archer": 
                if(behaviour == "move") {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1; 
                } else {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1;             
                }
                break; 
            default: return; 
        }

        // Create a dot
        for(int i=0; i < temp.Count; i++) {
            if( temp[i].GetComponent<CircleCollider2D>().IsTouching(this.gameObject.GetComponent<BoxCollider2D>() )) {
                temp[i].GetComponent<PieceController>().selected = true;
                temp[i].GetComponent<PieceController>().createDot( temp2[i] );
                Destroy(this.gameObject);
            }
        }

        // Destroy Indicator
        for(int i = 0; i < temp.Count; i++) {
            if(temp[i].GetComponent<PieceController>().selected == true) {
                continue;
            }
            temp[i].GetComponent<PieceController>().destroyIndicator(); 
        }         
        transform.SetParent(hand);
        beingHeld = false;
        
        /*foreach(Transform child in pieces.transform) {
        if(child.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log(child.gameObject.name + " will move or attack");
                pieceName = child.gameObject.name;
                selected = true;
                obj_id = child.GetInstanceID();
                Destroy(gameObject);
            }
        }*/
    }
}