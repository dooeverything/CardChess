using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Config;

public class cardGenerator : MonoBehaviour
{
    public int player; 
    public static int result;
    private int num_cards; 

    void OnEnable()
    {
        player = PlayerPrefs.GetInt("player");
        result = PlayerPrefs.GetInt("result");
    }

    void Start()
    {
        int result = PlayerPrefs.GetInt("result");

        if(player == 1 && result == (int)Names.P1_First || player == 2 && result == (int)Names.P2_First) {
            num_cards = 3; 
        } else {
            num_cards = 4; 
        }
        createCards(); 
    }

    public void createCards()
    {
        GameManager player = (this.player == 1) ? GameManager.player1 : GameManager.player2; 
        for(int i = 0; i < num_cards; i++) {
            Card card_drawn = player.drawCard(); 
            GameObject card = Helper.prefabNameToGameObject(Prefab.Mulligan.ToString()); 
            dragAndDrop component = card.GetComponent<dragAndDrop>();
            component.init(card_drawn, this.player, i);
            int center_x = 4; 
            float displacement = 2.5f; 
            card.transform.position = new Vector3(center_x - ((num_cards - 1) / 2) * displacement + (i * displacement), 8.5f, 0);
        }
    }
}
