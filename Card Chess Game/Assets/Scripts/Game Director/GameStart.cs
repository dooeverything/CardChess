using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameStart : MonoBehaviour
{
    [SerializeField] public Text player1;
    [SerializeField] public Text player2;

    // Start is called before the first frame update
    void Start()
    {
        headOrTail();
    }
    
    void headOrTail() {
        int randomIndex = Random.Range(0, 2);
        if(randomIndex == 0) {
            player1.text = "Tail";
            player2.text = "Head";
        }else {
            player1.text = "Head";
            player2.text = "Tail";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Text getPlayer1() {
        return player1;
    }
}
