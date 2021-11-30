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
            Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.test[3], typeof(GameObject));
            GameObject card = Instantiate(prefab) as GameObject;
            card.transform.SetParent(hands.transform, true); 
        }

    }

}
