using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class cardGenerator : MonoBehaviour
{
    // public List<int> cardList = new List<int>();
    //public GameObject[] cards;
    int numCard = 0;
    public static int result;


    void OnEnable()
    {
        // result = PlayerPrefs.GetInt("result");
        result = 1;
    }

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            int km = 6;
            int aa = 0;
            int ma = 1;
            GameManager.player1.deck.Add(km);
            GameManager.player2.deck.Add(km);
            GameManager.player1.deck.Add(aa);
            GameManager.player2.deck.Add(aa);
            GameManager.player1.deck.Add(ma);
            GameManager.player2.deck.Add(ma);
        }

        for (int i = 0; i < 6; i++)
        {
            int am = 3;
            int mm = 4;
            int wa = 2;
            GameManager.player1.deck.Add(am);
            GameManager.player1.deck.Add(mm);
            GameManager.player1.deck.Add(wa);
            GameManager.player2.deck.Add(am);
            GameManager.player2.deck.Add(mm);
            GameManager.player2.deck.Add(wa);
        }

        for (int i = 0; i < 12; i++)
        {
            int wm = 5;
            GameManager.player1.deck.Add(wm);
            GameManager.player2.deck.Add(wm);

        }
        for (int i = 0; i < 3; i++)
        {
            createCard();
        }

    }

    public void createCard()
    {
        // result가 0일때 선공
        if (result == 0 && numCard >= 2) return;
        // result가 1일때 후공
        if (result == 1 && numCard >= 3) return;

        int randomIndex = Random.Range(0, GameManager.player1.myDeckCount);
        int randomCard = GameManager.player1.deck[randomIndex];
        GameManager.player1.deck.RemoveAt(randomIndex);
        GameManager.player1.myDeckCount--;
        Object prefab = AssetDatabase.LoadAssetAtPath(CardSave.pathMulligan[randomCard], typeof(GameObject));
        GameObject card = Instantiate(prefab) as GameObject;

        dragAndDrop component = card.GetComponent<dragAndDrop>();
        GameManager.player1.card_ingame.Add(randomCard);
        component.cardType = randomCard;
        component.handPos = numCard;

        //if player1 is the second
        if (result == 1)
        {
            if (numCard < 3)
            {
                if (numCard == 0)
                {
                    card.transform.position = new Vector3(1.6f, 8.5f, 0);
                }
                else if (numCard == 1)
                {
                    card.transform.position = new Vector3(4, 8.5f, 0);
                }
                else
                {
                    card.transform.position = new Vector3(6.5f, 8.5f, 0);
                }
                numCard++;
            }
        }
        else
        {
            if (numCard < 2)
            {
                card.transform.position = new Vector3(2 + 4 * numCard, 8.5f, 0);
                numCard++;
            }
        }

    }
}
