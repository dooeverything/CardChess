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
        GameObject handsPlayer1 = GameObject.Find("Hands");
        GameObject handsPlayer2 = GameObject.Find("Hands_Opponent");

        // Player1
        for(int i=0; i<cardGenerator.result+2; i++) {
            Card card = GameManager.player1.hand[i]; 
            GameObject card_object = Config.Helper.cardToGameObject(card);
            

            card_object.transform.SetParent(handsPlayer1.transform, true);
 
            card_object.GetComponent<dragDrop>().init(player: 1, hand_index: i); 
            GameManager.player1.cards_in_hand.Add(card_object);
        }

        GameManager.turn = 2;
        // Player2
        for(int i=0; i<3-cardGenerator.result; i++) {
            endButtonController.drawCard(handsPlayer2);
        }

        //GameManager.turn = 1;
    
        if(cardGenerator.result == 0) {
            GameManager.turn = 1;
        }else {
            GameManager.turn = 2;
        }

    }

}
