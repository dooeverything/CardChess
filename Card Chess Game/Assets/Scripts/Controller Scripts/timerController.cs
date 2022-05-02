using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerController : MonoBehaviour
{
    private float timer = 30;
    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        if(timer <=0 ){
            timer = 30;
            GameManager.endTurn();
        }
        this.GetComponent<Text>().text = Mathf.Floor(timer).ToString();
    }
}
