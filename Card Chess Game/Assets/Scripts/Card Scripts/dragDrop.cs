using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool selected = false;
    public static bool beingHeld;
    public static GameObject selectedPiece = null;
    public cardSave.Piece pieceType;
    public string behaviour;
    Transform hand;
    private List<GameObject> target_pieces = null;
    private GameObject placeHolder = null;
    int indexSelected = -1;
    public int player = 1;
    public Game_Manager player_data;
    public string card_name; 
    public int handIndex = -1;
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

        hand = this.transform.parent;

        // Get information about a card
        target_pieces = player_data.filterList(pieceType); 
    }

    void update() {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        player_data.selected_card = null; 

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

        foreach (GameObject obj in Game_Manager.indicators)
        {
            Destroy(obj); 
        }

        foreach (GameObject obj in player_data.strike) {
            Destroy(obj);
        }

        //player_data.indicator = new List<GameObject>();
        transform.SetParent(this.transform.root);
        beingHeld = true;
        
        if(player == Game_Manager.turn) {
            foreach (GameObject obj in target_pieces) {
                obj.GetComponent<ChessPiece>().addIndicator();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        indexSelected = -1;
        transform.position = eventData.position;
        int newSiblingIndex = hand.transform.childCount;
        float minDistance = float.MaxValue;

        // Changing the card Index 
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

        for(int i=0; i<target_pieces.Count; i++) {
            if(GetComponent<Collider2D>().IsTouching(target_pieces[i].GetComponent<Collider2D>())) {
            float dist = Vector2.Distance(target_pieces[i].transform.position, gameObject.transform.position);
                if(minDistance > dist) {
                    minDistance = dist;
                    indexSelected = i;
                }
            }
        }

        foreach(GameObject indicator in Game_Manager.indicators) {
            indicator.GetComponent<Image>().color = Color.red;
        }
        Debug.Log("index is: " + indexSelected); 
        if(indexSelected >= 0) {
            Game_Manager.indicators[indexSelected].GetComponent<Image>().color = Color.blue;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(placeHolder);

        // Clear all indicators
        foreach ( GameObject obj in target_pieces ) {
            if(obj.GetComponent<ChessPiece>().activated == true) {
                continue;
            }
            obj.GetComponent<ChessPiece>().destroyIndicator();
        }

        // Create a dot
        if(indexSelected >= 0) {
            Game_Manager.destroyAllIndicators(); 
            Game_Manager.destroyAlldots(); 
            
            GameObject target_piece = target_pieces[indexSelected]; 
            target_piece.GetComponent<ChessPiece>().activated = true; 

            Color color = gameObject.GetComponent<Image>().color;
            color.a = 0.5f;
            gameObject.GetComponent<Image>().color = color;            
            Debug.Log(card_name);
            typeof(CardEffect).GetMethod(card_name).Invoke(null, new Object[]{target_piece, gameObject}); 
        }

        
        transform.SetParent(hand);
        beingHeld = false; // It indicates the player has dropped the card
        this.transform.SetSiblingIndex( placeHolder.transform.GetSiblingIndex() );
    }
}