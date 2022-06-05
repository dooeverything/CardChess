using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config; 

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject selected_piece = null;
    public Piece piece_type;
    public int player = 1;
    public GameManager player_data;
    public Card card; 
    private bool move_available = false; 
    private Transform hand;
    private List<GameObject> target_pieces = null;
    private int index_selected = -1;
    public int hand_index = -1;
    public int n_mana_required = 0;
    void Start()
    {
        if (player == 1)
        {
            player_data = GameManager.player1;
        }
        else
        {
            player_data = GameManager.player2;
        }

        hand = this.transform.parent;

        // Get information about a card
        target_pieces = player_data.filterList(piece_type); 
    }
    public void init(int player, int hand_index, Card card)
    {
        this.hand_index = hand_index;
        this.card = card;
        this.player = player;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(GameManager.turn != player || !isManaEnough() ) return;

        hand_index = findHandIndex();

        target_pieces = player_data.filterList(piece_type); 
        player_data.selected_card = null; 

        if(selected_piece) {
            selected_piece.GetComponent<ChessPiece>().activated = false;
        }

        foreach (Image card in transform.parent.GetComponentsInChildren<Image>())
        {
            //GameObject card_object = card.gameObject;
            if(card == transform.parent.gameObject.GetComponent<Image>()) continue;
            Color color = card.color;
            color.a = 1;
            card.color = color;
        }

        foreach (GameObject obj in GameManager.dots)
        {
            Destroy(obj); 
        }
        
        GameManager.destroyAllIndicators();

        foreach (GameObject obj in player_data.strike) {
            Destroy(obj);
        }

        transform.SetParent(this.transform.root);        
        if(player == GameManager.turn) {
            foreach (GameObject obj in target_pieces) {
                obj.GetComponent<ChessPiece>().addIndicator();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(!isManaEnough()) return;

        if(GameManager.turn != player) {
            returnToInitPos();
            return;
        }

        GameManager.destroyAlldots();
        index_selected = -1;
        transform.position = eventData.position;
        float min_distance = float.MaxValue;
        
        for(int i=0; i<target_pieces.Count; i++) {
            if(GetComponent<Collider2D>().IsTouching(target_pieces[i].GetComponent<Collider2D>())) {
            float dist = Vector2.Distance(target_pieces[i].transform.position, gameObject.transform.position);
                if(min_distance > dist) {
                    min_distance = dist;
                    index_selected = i;
                }
            }
        }

        foreach(GameObject indicator in GameManager.indicators) {
            indicator.GetComponent<Image>().color = Color.red;
        }

        if(index_selected >= 0) {
            GameObject target_piece = target_pieces[index_selected]; 
            move_available = CardConfig.card_dict[card].Item1(target_piece, gameObject);
            if(move_available) {
                GameManager.indicators[index_selected].GetComponent<Image>().color = Color.black;
            }
        }
    }    
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if(GameManager.turn != player || !isManaEnough() ) return;

        // Create a dot
        if(index_selected >= 0  && move_available) {
            GameObject target_piece = target_pieces[index_selected]; 
            target_piece.GetComponent<ChessPiece>().activated = true; 
            GameObject temp = null;

            // Clear all indicators(except the selected chesspiece) and dots
            foreach ( GameObject obj in target_pieces ) {
                if(obj.GetComponent<ChessPiece>().activated == true) {
                    temp = obj.transform.GetChild(0).gameObject;
                    int index=0;
                    for(int i=0; i<GameManager.indicators.Count; i++){
                        if(GameManager.indicators[i].GetInstanceID() == temp.GetInstanceID()) {
                            index = i;
                            break;
                        }
                    }
                    GameManager.indicators.RemoveAt(index);
                    break;
                }
            }

            GameManager.destroyAllIndicators();
            GameManager.indicators.Add(temp);

            Color color = gameObject.GetComponent<Image>().color; // make a card transparency which means the card is dragged and applied to the piece
            color.a = 0.5f;
            gameObject.GetComponent<Image>().color = color;            
            GameManager.executing = true; 
            move_available = CardConfig.card_dict[card].Item1(target_piece, gameObject); 
            GameManager.executing = false; 
            target_piece.GetComponent<ChessPiece>().activated = false;
        }else {
            GameManager.destroyAllIndicators();
        }
        returnToInitPos();
    }

    public void destroyCard() {
        CardController temp = GetComponent<CardController>(); 
        temp.player_data.hand.RemoveAt(hand_index);
        Destroy(gameObject); 
    }
    public void returnToInitPos(){
        if(transform.parent == hand) return;
        transform.SetParent(hand);
        transform.SetSiblingIndex(hand_index);
    }
    public int findHandIndex(){
        for(int i = 0; i < hand.childCount; i++) {
            Transform card_transform = hand.GetChild(i);
            if(transform == card_transform) {
                return i;
            }
        }
        return -1;
    }


    public void EndTurn() 
    {
        Debug.Log("Set color after end turn");
        Color color = gameObject.GetComponent<Image>().color; // make a card transparency which means the card is dragged and applied to the piece
        color.a = 1.0f;
        gameObject.GetComponent<Image>().color = color;  
    }

    public bool isManaEnough()
    {
        n_mana_required = (int)CardConfig.card_dict[card].Item3;

        int n_curr_mana = player_data.mana.Count;

        return (n_curr_mana >= n_mana_required);
    }

    public void destroyMana(int n_mana)
    {
        // Delete a number of mana that is used for executing a card
        for(int i = 0; i<n_mana; i++){
            Destroy(player_data.mana[i]);
        }
        
        for(int i=0; i<n_mana; i++){
            player_data.mana.RemoveAt(0);
        }
    }

}