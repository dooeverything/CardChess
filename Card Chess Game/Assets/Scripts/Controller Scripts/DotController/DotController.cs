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
            int n_mana = card.GetComponent<CardController>().n_mana_required;
            card.GetComponent<CardController>().destroyMana(n_mana);
            Destroy(card);
        }
    }

    public void moveParent()
    {
            Transform cell_transform = gameObject.transform.parent; // parent is a Cell gameobject

            parent.transform.SetParent(cell_transform); // selected piece의 부모는 selected piece가 이동할 cell
            parent.transform.position = cell_transform.position; // 위치 조정   
    }
}
