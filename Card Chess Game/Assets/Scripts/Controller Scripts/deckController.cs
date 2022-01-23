using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deckController : MonoBehaviour
{
    [SerializeField] public int player;
    Game_Manager player_data;
    // Start is called before the first frame update
    void Start()
    {
        if(player == 1) {
            player_data = Game_Manager.player1;
        }else {
            player_data = Game_Manager.player2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = player_data.myDeckCount.ToString();
    }
}
