using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class cardGenerator_InGame : MonoBehaviour
{

    void Start()
    {
        //GameObject handPlayer1 = GameObject.Find("Hand_player1");
        GameObject handsPlayer1 = GameObject.Find("Hands");
        GameObject handsPlayer2 = GameObject.Find("Hands_Opponent");
        // for(int i=0; i<cardGenerator.result+1; i++) {
        //     Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.pathInGame[cardSave.cardList[i]], typeof(GameObject));
        //     GameObject card = Instantiate(prefab) as GameObject;
        //     card.transform.SetParent(handPlayer1.transform, true); 
        // }

        // Player1
        for(int i=0; i<cardGenerator.result+2; i++) {
            Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.test[Game_Manager.player1.card_ingame[i]], typeof(GameObject));
            GameObject card = Instantiate(prefab) as GameObject;
            card.transform.SetParent(handsPlayer1.transform, true);
            card.GetComponent<dragDrop>().player = 1;
            card.GetComponent<dragDrop>().pieceType = cardSave.test2[Game_Manager.player1.card_ingame[i], 0];
            card.GetComponent<dragDrop>().behaviour = cardSave.test2[Game_Manager.player1.card_ingame[i], 1];
            Game_Manager.player1.cards_in_hand.Add(card);
        }

        Game_Manager.turn = 2;
        // Player2
        for(int i=0; i<3-cardGenerator.result; i++) {
            endButtonController.drawCard(handsPlayer2);
        }

        Game_Manager.turn = 1;
    }

}
