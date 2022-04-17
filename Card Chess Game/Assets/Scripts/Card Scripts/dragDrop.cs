using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config; 

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool selected = false;
    public static bool beingHeld;
    public static GameObject selectedPiece = null;
    public Piece pieceType;
    public string behaviour;
    public int player = 1;
    public GameManager player_data;
    public Card card; 
    public int handIndex = -1;
    private bool move_available = false; 
    private Transform hand;
    private List<GameObject> target_pieces = null;
    private GameObject placeHolder = null;
    private int indexSelected = -1;
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
        target_pieces = player_data.filterList(pieceType); 
    }

    void update() {
        //target_pieces = player_data.filterList(pieceType); 
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        target_pieces = player_data.filterList(pieceType); 
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

        foreach (GameObject obj in GameManager.dots)
        {
            Destroy(obj); 
        }

        // foreach (GameObject obj in GameManager.indicators)
        // {
        //     Destroy(obj); 
        // }

        GameManager.destroyAllIndicators();

        foreach (GameObject obj in player_data.strike) {
            Destroy(obj);
        }

        //player_data.indicator = new List<GameObject>();
        transform.SetParent(this.transform.root);
        beingHeld = true;
        
        if(player == GameManager.turn) {
            foreach (GameObject obj in target_pieces) {
                obj.GetComponent<ChessPiece>().addIndicator();
            }
        }
    }

        public void OnDrag(PointerEventData eventData)
    {
        //target_pieces = player_data.filterList(pieceType); 
        GameManager.destroyAlldots();
        //GameManager.destroyAllIndicators();
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
        if(GameManager.indicators == null) {
        }
        foreach(GameObject indicator in GameManager.indicators) {
            indicator.GetComponent<Image>().color = Color.red;
        }
        if(indexSelected >= 0) {
            GameObject target_piece = target_pieces[indexSelected]; 
            move_available = CardConfig.card_dict[card].Item1(target_piece, gameObject); 
            if(move_available) {
                GameManager.indicators[indexSelected].GetComponent<Image>().color = Color.blue;
            }
        }
    }    
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(placeHolder);
        // Create a dot
        if(indexSelected >= 0  && move_available) {
            GameObject target_piece = target_pieces[indexSelected]; 
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
        transform.SetParent(hand);
        beingHeld = false; // It indicates the player has dropped the card
        this.transform.SetSiblingIndex( placeHolder.transform.GetSiblingIndex() );
    }

    public void destoryCard() {
        dragDrop temp = GetComponent<dragDrop>(); 
        List<GameObject> list = temp.player_data.cards_in_hand;
        temp.player_data.cards_in_hand.RemoveAt(handIndex);
        Destroy(this.gameObject); 
    }

}