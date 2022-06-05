using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Config; 
public class CardGenerator : MonoBehaviour
{

    void Start()
    {
        
        GameObject hand_player1 = GameObject.Find("Hand_P1");
        GameObject hand_player2 = GameObject.Find("Hand_P2");

        // Player1
        for(int i=0; i<GameManager.player1.mulligan.Count; i++) {
            if(i != GameManager.selected_mulligan_player1) {
                GameManager.player1.pushBackCard(i);
                continue;
            } 

            Card card = GameManager.player1.mulligan[i]; 
            
            // Add selected Mulligan to hand
            GameManager.player1.hand.Add(card);
            GameObject card_object = Config.Helper.cardToGameObject(card);
            
            card_object.transform.SetParent(hand_player1.transform, true);

            card_object.GetComponent<CardController>().init(player: 1, hand_index: 0, card: card);
        }

        // Player2
        for(int i=0; i<GameManager.player2.mulligan.Count; i++) {

            if(i != GameManager.selected_mulligan_player2) {
                GameManager.player2.pushBackCard(i);
                continue;
            } 

            Card card = GameManager.player2.mulligan[i]; 

            // Add selected Mulligan to hand
            GameManager.player2.hand.Add(card);

            GameObject card_object = Config.Helper.cardToGameObject(card);

            card_object.transform.SetParent(hand_player2.transform, true);

            card_object.GetComponent<CardController>().init(player: 2, hand_index: 0, card: card); 
        }
        //GameManager.turn = 1;

        GameManager.turn = PlayerPrefs.GetInt("result");
    }

}
