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
        if (card)
        {
            dragDrop temp = card.GetComponent<dragDrop>();
            List<GameObject> list = temp.player_data.cards_in_hand;
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (card == list[i])
                {
                    index = i;
                    break;
                }
            }
            temp.player_data.cards_in_hand.RemoveAt(index);
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

        Game_Manager.destroyAlldots();
        Game_Manager.destroyAllIndicators();

        if (card)
        {
            dragDrop temp = card.GetComponent<dragDrop>();
            List<GameObject> list = temp.player_data.cards_in_hand;
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (card == list[i])
                {
                    index = i;
                    break;
                }

            }
            temp.player_data.cards_in_hand.RemoveAt(index);
            Destroy(card);
        }

    }
}
