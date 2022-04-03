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
            int random = Random.Range(10, 11);
            string card_name = cardSave.Card_List[random].Item1; 
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/cardTest.prefab", typeof(GameObject));
            GameObject card = Instantiate(prefab) as GameObject;
            card.transform.SetParent(handsPlayer1.transform, true);
            card.GetComponent<dragDrop>().player = 1;
            string fileLocation = "Sprites/" + card_name;
            Sprite card_sprite =  Resources.Load<Sprite>(fileLocation);
            
            if(card_sprite == null) {
                Debug.Log("Not working " + fileLocation);
            }

            card.transform.GetChild(0).GetComponent<Image>().sprite = card_sprite;
            card.transform.GetChild(1).GetComponent<Text>().text = card_name;
            card.GetComponent<dragDrop>().pieceType = cardSave.Card_List[random].Item2; 
            card.GetComponent<dragDrop>().card_name = card_name; 
            card.GetComponent<dragDrop>().handIndex = i; 
            Game_Manager.player1.cards_in_hand.Add(card);
        }

        Game_Manager.turn = 2;
        // Player2
        for(int i=0; i<3-cardGenerator.result; i++) {
            endButtonController.drawCard(handsPlayer2);
        }

        //Game_Manager.turn = 1;
    
        if(cardGenerator.result == 0) {
            Game_Manager.turn = 1;
        }else {
            Game_Manager.turn = 2;
        }

    }

}
