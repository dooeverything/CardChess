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
                }
                else
                {
                    temp = player_data.archerOnBoard;
                }
                break;
            case "warrior":
                if (behaviour == "move")
                {
                    temp = player_data.warriorOnBoard;
                }
                else
                {
                    temp = player_data.warriorOnBoard;
                }
                break;
            case "mage":
                if (behaviour == "move")
                {
                    temp = player_data.mageOnBoard;
                }
                else
                {
                    temp = player_data.mageOnBoard;
                }
                break;
            case "king":
                if (behaviour == "move")
                {
                    temp = player_data.kingOnBoard;
                }
                break;
            default: return;
        }
    }

    void update() {
        
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

        //Debug.Log("Debugging" + selectedPiece);
        if(selectedPiece) {
            selectedPiece.GetComponent<ChessPiece>().activated = false;
        }

        foreach (GameObject obj in player_data.cards_in_hand)
        {
            Color color = obj.GetComponent<Image>().color;
            color.a = 1;
            obj.GetComponent<Image>().color = color;
        }

        foreach (GameObject obj in Game_Manager.dots)
        {
            Destroy(obj); 
        }

        foreach (GameObject obj in player_data.indicator)
        {
            Destroy(obj); 
        }

        foreach (GameObject obj in player_data.strike) {
            Destroy(obj);
        }

        player_data.indicator = new List<GameObject>();
        transform.SetParent(this.transform.root);
        beingHeld = true;
        
        if(player == Game_Manager.turn) {
            foreach (GameObject obj in temp) {
                obj.GetComponent<ChessPiece>().createIndicatorForCard();
            }
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
        Debug.Log(" GOOD !");
    }
    public static bool selected = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(placeHolder);

        Debug.Log("OnEndDrag");
        Debug.Log("Player " + player + " dropped the card");
        //int index = -1;

        // Create a dot
        if(indexSelected >= 0) {
            temp[indexSelected].GetComponent<ChessPiece>().activated = true;
            //temp[indexSelected].GetComponent<PieceController>().createDot(temp2[indexSelected], behaviour);
            
            // Change an alpha value of a card  --------------------------//
            Color color = gameObject.GetComponent<Image>().color;
            color.a = 0.5f;
            gameObject.GetComponent<Image>().color = color;
            //------------------------------------------------------------//
            
            player_data.selected_card = gameObject; // Save this selected card to player_data

            GameObject selectedIndicator = player_data.indicator[indexSelected];
            player_data.indicator = new List<GameObject>();
            player_data.indicator.Add(selectedIndicator);
            selectedPiece = temp[indexSelected];
            Debug.Log("The player will use the card on " + selectedPiece);
        }else {
            player_data.indicator = new List<GameObject>();
        }

        // Clear all indicators
        foreach ( GameObject obj in temp ) {
            if(obj.GetComponent<ChessPiece>().activated == true) {
                continue;
            }
            obj.GetComponent<ChessPiece>().destroyIndicator();
        }
        
        transform.SetParent(hand);
        beingHeld = false; // It indicates the player has dropped the card
        this.transform.SetSiblingIndex( placeHolder.transform.GetSiblingIndex() );
    }
}