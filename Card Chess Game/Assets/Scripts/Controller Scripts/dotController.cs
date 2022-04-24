using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dotController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public GameObject parent;
    public GameObject card;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void deleteCard()
    {
        if (card) {
            int hand_index = card.GetComponent<dragDrop>().hand_index;
            card.GetComponent<dragDrop>().player_data.hand.RemoveAt(hand_index);
            Destroy(card);
        }
    }

    public void moveParent()
    {
        Transform cellTransform = gameObject.transform.parent; // parent is a Cell gameobject

        parent.transform.SetParent(cellTransform); // selected piece의 부모는 selected piece가 이동할 cell
        parent.transform.position = cellTransform.position; // 위치 조정
        parent.GetComponent<ChessPiece>().indexX = cellTransform.gameObject.GetComponent<cellController>().indexX;
        parent.GetComponent<ChessPiece>().indexY = cellTransform.gameObject.GetComponent<cellController>().indexY;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveParent();

        // Switch turn after the piece move
        //endButtonController.switchTurn();

        GameManager.destroyAlldots();
        GameManager.destroyAllIndicators();

        deleteCard();

    }
}
