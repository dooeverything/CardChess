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
        result = PlayerPrefs.GetInt("result");
    }

    void Start()
    {
        Names result = (Names)PlayerPrefs.GetInt("result");

        if(player == 1 && result == Names.P1_First || player == 2 && result == Names.P2_First) {
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
            GameObject card = Helper.prefabNameToGameObject(Names.Mulligan.ToString()); 
            dragAndDrop component = card.GetComponent<dragAndDrop>();
            component.init(Card,)
            Vector3 center_pos = new Vector3(4, 8.5f, 0); 
            
        }

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
