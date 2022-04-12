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
        //     Object prefab = AssetDatabase.LoadAssetAtPath(CardSave.pathInGame[CardSave.cardList[i]], typeof(GameObject));
        //     GameObject card = Instantiate(prefab) as GameObject;
        //     card.transform.SetParent(handPlayer1.transform, true); 
        // }

        // Player1
        for(int i=0; i<cardGenerator.result+2; i++) {
            var temp = 2; 
            int random = Random.Range(temp, temp + 1);
            //int random = Random.Range(CardSave.Card_List.Length - 1, CardSave.Card_List.Length);            
            string card_name = CardSave.Card_List[random].Item1; 
            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/cardTest.prefab", typeof(GameObject));
            GameObject card = Instantiate(prefab) as GameObject;
            card.transform.SetParent(handsPlayer1.transform, true);
            card.GetComponent<dragDrop>().player = 1;
            string fileLocation = "Sprites/" + card_name;
            Sprite card_sprite =  Resources.Load<Sprite>(fileLocation);
            

            card.transform.GetChild(0).GetComponent<Image>().sprite = card_sprite;
            card.transform.GetChild(1).GetComponent<Text>().text = card_name;
            card.GetComponent<dragDrop>().pieceType = CardSave.Card_List[random].Item2; 
            card.GetComponent<dragDrop>().card_name = card_name; 
            card.GetComponent<dragDrop>().handIndex = i; 
            GameManager.player1.cards_in_hand.Add(card);
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
