using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool beingHeld;
    private GameObject pieces;
    public static string pieceName;
    public static string cardName;

    public string pieceType;
    public string behaviour;
    Transform hand;
    private List<GameObject> temp = null;
    private List<ChessPiece> temp2 = null;
    private GameObject placeHolder = null;

    public static GameObject selectedPiece = null;
    void Start()
    {
        cardName = this.gameObject.name;
        hand = this.transform.parent;

        // Get information about a card
        switch (pieceType)
        {
            case "archer":
                if (behaviour == "move")
                {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1;
                }
                break;
            case "warrior":
                if (behaviour == "move")
                {
                    temp = Game_Manager.warriorOnBoard_player1;
                    temp2 = Game_Manager.warriorConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.warriorOnBoard_player1;
                    temp2 = Game_Manager.warriorConstructors_player1;
                }
                break;
            case "mage":
                if (behaviour == "move")
                {
                    temp = Game_Manager.mageOnBoard_player1;
                    temp2 = Game_Manager.mageConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.mageOnBoard_player1;
                    temp2 = Game_Manager.mageConstructors_player1;
                }
                break;
            case "king":
                if (behaviour == "move")
                {
                    Debug.Log("King(move) card is selected");
                    temp = Game_Manager.kingOnBoard_player1;
                    temp2 = Game_Manager.kingConstructors_player1;
                }
                break;
            default: return;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {


        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        Game_Manager.selected_card = null; 
        // for (int i = 0; i < temp.Count; i++)
        // {
        //     temp[i].GetComponent<PieceController>().selected = false; 
        // }
        Debug.Log("Debugging" + selectedPiece);
        if(selectedPiece) {
            selectedPiece.GetComponent<PieceController>().selected = false;
        }

        foreach (GameObject obj in Game_Manager.cards_in_hand)
        {
            Color color = obj.GetComponent<Image>().color;
            color.a = 1;
            obj.GetComponent<Image>().color = color;
        }

        foreach (GameObject obj in Game_Manager.dots)
        {
            Destroy(obj); 
        }

        foreach(GameObject obj in Game_Manager.warriorOnBoard_player1) {
            Debug.Log(obj.GetComponent<PieceController>().selected);
        }
        //Debug.Log("count before: " + Game_Manager.indicator.Count);

        foreach (GameObject obj in Game_Manager.indicator)
        {
            //Debug.Log("obj is: " + Game_Manager.indicator.Count);
            //Debug.Log("id is: " + obj.GetInstanceID()); 
            Destroy(obj); 
        }
        Game_Manager.indicator = new List<GameObject>();
        //Debug.Log("count after: " + Game_Manager.indicator.Count);

        //Debug.Log("OnBeginDrag");
        transform.SetParent(this.transform.root);
        beingHeld = true;

        // Create indicators
        foreach (GameObject obj in temp) {
            obj.GetComponent<PieceController>().createIndicator();
        }
    }
    int indexSelected = -1;

    public void OnDrag(PointerEventData eventData)
    {
        indexSelected = -1;

        //Debug.Log(Game_Manager.indicator.Count);
        this.transform.position = eventData.position;
        int newSiblingIndex = hand.transform.childCount;
        for(int i=0; i<hand.transform.childCount; i++) {
            if(transform.position.x < hand.transform.GetChild(i).position.x) {
                newSiblingIndex = i;
                if(placeHolder.transform.GetSiblingIndex() < newSiblingIndex) {
                    newSiblingIndex--;
                }
                break;
            }
        }
        placeHolder.transform.SetSiblingIndex(newSiblingIndex);

        float minDistance = 1000000000;

        for(int i=0; i<temp.Count; i++) {
            float dist = Vector2.Distance(temp[i].transform.position, gameObject.transform.position);
            if( dist < 150) {
                if(minDistance > dist) {
                    minDistance = dist;
                    indexSelected = i;
                }
            }
        }

        foreach(GameObject indicator in Game_Manager.indicator) {
            indicator.GetComponent<Image>().color = Color.red;
        }

        if(indexSelected >= 0) {
            Game_Manager.indicator[indexSelected].GetComponent<Image>().color = Color.blue;
        }
    }
    public static bool selected = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(placeHolder);

        Debug.Log("OnEndDrag");
        //int index = -1;

        // Create a dot
        if(indexSelected >= 0) {
            temp[indexSelected].GetComponent<PieceController>().selected = true;
            temp[indexSelected].GetComponent<PieceController>().createDot(temp2[indexSelected], behaviour);
            Color color = gameObject.GetComponent<Image>().color;
            color.a = 0.5f;
            gameObject.GetComponent<Image>().color = color;
            Game_Manager.selected_card = gameObject; 

            GameObject selectedIndicator = Game_Manager.indicator[indexSelected];
            // Clear all indicators
            Game_Manager.indicator = new List<GameObject>();
            Game_Manager.indicator.Add(selectedIndicator);
            selectedPiece = temp[indexSelected];
            Debug.Log(selectedPiece);
        }else {
            Game_Manager.indicator = new List<GameObject>();
        }

        // Destroy Indicator
        // for (int i = 0; i < temp.Count; i++)
        // {
        //     if (temp[i].GetComponent<PieceController>().selected == true)
        //     {
        //         continue;
        //     }
        //     temp[i].GetComponent<PieceController>().destroyIndicator();
        // }

        foreach ( GameObject obj in temp ) {
            if(obj.GetComponent<PieceController>().selected == true) {
                continue;
            }
            obj.GetComponent<PieceController>().destroyIndicator();
        }
        
        transform.SetParent(hand);
        beingHeld = false;
        this.transform.SetSiblingIndex( placeHolder.transform.GetSiblingIndex() );
        


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