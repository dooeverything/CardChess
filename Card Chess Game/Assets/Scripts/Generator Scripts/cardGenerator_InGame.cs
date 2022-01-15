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
        GameObject hands = GameObject.Find("Hands");
        // for(int i=0; i<cardGenerator.result+1; i++) {
        //     Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.pathInGame[cardSave.cardList[i]], typeof(GameObject));
        //     GameObject card = Instantiate(prefab) as GameObject;
        //     card.transform.SetParent(handPlayer1.transform, true); 
        // }


        for(int i=0; i<cardGenerator.result+2; i++) {
            Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.test[Game_Manager.card_ingame[i]], typeof(GameObject));
            GameObject card = Instantiate(prefab) as GameObject;
            card.transform.SetParent(hands.transform, true); 
            card.GetComponent<dragDrop>().pieceType = cardSave.test2[Game_Manager.card_ingame[i], 0];
            card.GetComponent<dragDrop>().behaviour = cardSave.test2[Game_Manager.card_ingame[i], 1];
            Game_Manager.cards_in_hand.Add(card);
            //Game_Manager.myDeckCount--;

        }

    }

}
