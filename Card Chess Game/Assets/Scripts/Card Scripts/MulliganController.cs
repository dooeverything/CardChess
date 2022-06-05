using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

using Config; 
public class MulliganController : MonoBehaviour, IPointerDownHandler
{
    public int index;
    private Card card; 
    private int player; 
    public GameManager player_data;
    //private bool lock_dragging = false;
    private bool is_clicked = false;
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
                GameManager.selected_mulligan_player1 = index;
            }else{
                GameManager.selected_mulligan_player2 = index;
            }
            
        }else { // If a mulligan is clicked before
            is_clicked = false;
            GetComponent<Outline>().enabled = false;

            if(player == 1) {
                GameManager.selected_mulligan_player1 = -1;
            }else{
                GameManager.selected_mulligan_player2 = -1;
            }
        }
    }
}
