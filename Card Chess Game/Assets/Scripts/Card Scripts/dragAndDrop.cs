using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;

using Config; 
public class dragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPos;
    public int index;
    public int cardType; // 무슨 카드인지
    public bool canMove = true; // 드래그할수 있는지
    private Card card; 
    private GameObject trashCan;
    private int player; 
    public GameManager player_data;
    void Start() {
        startPos = transform.position;
        trashCan = GameObject.Find("TrashCan");
    }
    void Update()
    {
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

    public void OnBeginDrag(PointerEventData eventData) 
    {
        //Debug.Log("Begin");

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        //Debug.Log("On Dragging");
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if(GetComponent<BoxCollider2D>().IsTouching(trashCan.GetComponent<BoxCollider2D>())) {
            //Debug.Log("Touching Box");
            changeCard();
        }
        
        transform.position = startPos;
    }
}
