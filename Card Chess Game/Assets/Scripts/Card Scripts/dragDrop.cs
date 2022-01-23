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

    public int player = 1;
    Game_Manager player_data;
    void Start()
    {
        if (player == 1)
        {
            player_data = Game_Manager.player1;
        }
        else
        {
            player_data = Game_Manager.player2;
        }

        cardName = this.gameObject.name;
        hand = this.transform.parent;

        // Get information about a card
        switch (pieceType)
        {
            case "archer":
                if (behaviour == "move")
                {
                    temp = player_data.archerOnBoard;
                    temp2 = player_data.archerConstructors;
                }
                else
                {
                    temp = player_data.archerOnBoard;
                    temp2 = player_data.archerConstructors;
                }
                break;
            case "warrior":
                if (behaviour == "move")
                {
                    temp = player_data.warriorOnBoard;
                    temp2 = player_data.warriorConstructors;
                }
                else
                {
                    temp = player_data.warriorOnBoard;
                    temp2 = player_data.warriorConstructors;
                }
                break;
            case "mage":
                if (behaviour == "move")
                {
                    temp = player_data.mageOnBoard;
                    temp2 = player_data.mageConstructors;
                }
                else
                {
                    temp = player_data.mageOnBoard;
                    temp2 = player_data.mageConstructors;
                }
                break;
            case "king":
                if (behaviour == "move")
                {
                    Debug.Log("King(move) card is selected");
                    temp = player_data.kingOnBoard;
                    temp2 = player_data.kingConstructors;
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

        player_data.selected_card = null; 
        // for (int i = 0; i < temp.Count; i++)
        // {
        //     temp[i].GetComponent<PieceController>().selected = false; 
        // }
        Debug.Log("Debugging" + selectedPiece);
        if(selectedPiece) {
            selectedPiece.GetComponent<PieceController>().selected = false;
        }

        foreach (GameObject obj in player_data.cards_in_hand)
        {
            Color color = obj.GetComponent<Image>().color;
            color.a = 1;
            obj.GetComponent<Image>().color = color;
        }

        foreach (GameObject obj in player_data.dots)
        {
            Destroy(obj); 
        }

        foreach(GameObject obj in player_data.warriorOnBoard) {
            Debug.Log(obj.GetComponent<PieceController>().selected);
        }
        //Debug.Log("count before: " + player_data.indicator.Count);

        foreach (GameObject obj in player_data.indicator)
        {
            //Debug.Log("obj is: " + player_data.indicator.Count);
            //Debug.Log("id is: " + obj.GetInstanceID()); 
            Destroy(obj); 
        }
        player_data.indicator = new List<GameObject>();
        //Debug.Log("count after: " + player_data.indicator.Count);

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

        //Debug.Log(player_data.indicator.Count);
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

        foreach(GameObject indicator in player_data.indicator) {
            indicator.GetComponent<Image>().color = Color.red;
        }

        if(indexSelected >= 0) {
            player_data.indicator[indexSelected].GetComponent<Image>().color = Color.blue;
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
            player_data.selected_card = gameObject; 

            GameObject selectedIndicator = player_data.indicator[indexSelected];
            // Clear all indicators
            player_data.indicator = new List<GameObject>();
            player_data.indicator.Add(selectedIndicator);
            selectedPiece = temp[indexSelected];
            Debug.Log(selectedPiece);
        }else {
            player_data.indicator = new List<GameObject>();
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