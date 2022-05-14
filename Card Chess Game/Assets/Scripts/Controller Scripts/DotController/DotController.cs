using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour
{
    public GameObject card;
    public GameObject parent; 

    // Start is called before the first frame update
    public void deleteCard()
    {
        if (card) {
            int hand_index = card.GetComponent<CardController>().hand_index;
            card.GetComponent<CardController>().player_data.hand.RemoveAt(hand_index);
            Destroy(card);
        }
    }

    public void moveParent(){
            Transform cellTransform = gameObject.transform.parent; // parent is a Cell gameobject

            parent.transform.SetParent(cellTransform); // selected piece의 부모는 selected piece가 이동할 cell
            parent.transform.position = cellTransform.position; // 위치 조정   
    }
}
