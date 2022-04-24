using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Config; 
public class cardGenerator_InGame : MonoBehaviour
{

    void Start()
    {
        Debug.Log("Game Start!");
        
        GameObject handsPlayer1 = GameObject.Find("Hand_P1");
        GameObject handsPlayer2 = GameObject.Find("Hand_P2");

        Debug.Log(GameManager.player1.hand.Count);

        // Player1
        for(int i=0; i<GameManager.player1.hand.Count; i++) {
            Card card = GameManager.player1.hand[i]; 
            GameObject card_object = Config.Helper.cardToGameObject(card);
            

            card_object.transform.SetParent(handsPlayer1.transform, true);

            card_object.GetComponent<dragDrop>().init(player: 1, hand_index: i, card: card); 
        }

        // Player2
        for(int i=0; i<GameManager.player2.hand.Count; i++) {
            Card card = GameManager.player2.hand[i]; 
            GameObject card_object = Config.Helper.cardToGameObject(card);
            

            card_object.transform.SetParent(handsPlayer2.transform, true);

            card_object.GetComponent<dragDrop>().init(player: 2, hand_index: i, card: card); 
        }
        //GameManager.turn = 1;

        GameManager.turn = PlayerPrefs.GetInt("result");

    }

}
