using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

using Config; 
public class dragAndDrop : MonoBehaviour
{
    public int index;
    public int cardType; // 무슨 카드인지
    public bool canMove = true; // 드래그할수 있는지
    private float firstPosX;
    private float firstPosY;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private Card card; 
    private GameObject trashCan;
    private int player; 
    public GameManager player_data;
    void Start() {
        firstPosX = transform.localPosition.x;
        firstPosY = transform.localPosition.y;
        trashCan = GameObject.Find("trashcan_closed");
    }
    void Update()
    {
        if(isBeingHeld == true) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }else {
            if(trashCan.GetComponent<BoxCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                createNewCard();
            }
        }
        //onMouseUp();
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
    }

    private void createNewCard() {
        Sprite card_sprite = Config.Helper.generateSprite(card.ToString());
        transform.GetChild(0).GetComponent<Image>().sprite = card_sprite;
        transform.GetChild(1).GetComponent<Text>().text = card.ToString(); 
    }

    private void OnMouseDown() {
        if(!canMove) return; 
        if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); // convert screen mouse to world(game)

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void OnMouseUp() {
        
        isBeingHeld = false;
        gameObject.transform.localPosition = new Vector3(firstPosX, firstPosY, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        
    }
}
