using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerController : MonoBehaviour
{
    public float timer = 30;
    GameObject hands1;
    GameObject hands2;
    
    void setTimer(int time){

        timer = time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        hands1 = GameObject.Find("Hands");
        hands2 = GameObject.Find("Hands_Opponent");
        // hands = GameObject.Find("Hands");
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        GameObject hands; 
        if(GameManager.turn == 1) {
            hands = hands1; 
        } else {
            hands = hands2; 
        }
        if(timer <=0 ){
            timer = 30;
            endButtonController.switchTurn();
            endButtonController.drawCard(hands);
        }
        this.GetComponent<Text>().text = Mathf.FloorToInt(timer).ToString();
    }
}
