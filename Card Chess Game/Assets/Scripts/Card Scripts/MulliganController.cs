using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

using Config; 
public class MulliganController : MonoBehaviour, IPointerDownHandler
{
    Vector3 startPos;
    public int index;
    public int cardType; // 무슨 카드인지
    public bool canMove = true; // 드래그할수 있는지
    private Card card; 
    private GameObject trashCan;
    private int player; 
    public GameManager player_data;
    //private bool lock_dragging = false;
    private bool is_clicked = false;
    void Start() {
        startPos = transform.position;
        trashCan = GameObject.Find("TrashCan");
    }
    void Update()
    {

    }

    public void init(Card card, int player, int index) {
        this.card = card; 
        this.player = player; 
        this.index = index;
        if (this.player == 1)
        {
            player_data = GameManager.player1;
        }
        else
        {
            player_data = GameManager.player2;
        }
        createNewCard();
    }

    private void changeCard() {
        this.card = player_data.replaceCard(index);
        createNewCard();
        //lock_dragging = true;
        GetComponent<Outline>().enabled = true;
    }

    private void createNewCard() {
        Sprite card_sprite = Config.Helper.generateSprite(card.ToString());
        transform.GetChild(0).GetComponent<Image>().sprite = card_sprite;
        transform.GetChild(1).GetComponent<Text>().text = card.ToString(); 
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if(!is_clicked){ // If a mulligan is not clicked before 
            
            // Unselect all mulligans
            foreach( Transform child in transform.parent) {
                child.GetComponent<MulliganController>().is_clicked = false;
                child.GetComponent<Outline>().enabled = false;
            }

            is_clicked = true;
            GetComponent<Outline>().enabled = true;

            if(player == 1) {
                //Debug.Log("Player1 start card: " + index);
                GameManager.selected_mulligan_player1 = index;
            }else{
                //Debug.Log("Player2 start card: " + index);
                GameManager.selected_mulligan_player2 = index;
            }
            
        }else { // If a mulligan is clicked before
            is_clicked = false;
            GetComponent<Outline>().enabled = false;

            if(player == 1) {
                //Debug.Log("Player1 start card: " + index);
                GameManager.selected_mulligan_player1 = -1;
            }else{
                //Debug.Log("Player2 start card: " + index);
                GameManager.selected_mulligan_player2 = -1;
            }
        }
    }

    // public void OnBeginDrag(PointerEventData eventData) 
    // {
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     if(lock_dragging) return;
    //     transform.position = eventData.position;
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     if(GetComponent<BoxCollider2D>().IsTouching(trashCan.GetComponent<BoxCollider2D>())) {
    //         changeCard();
    //     }
        
    //     transform.position = startPos;
    // }
}
