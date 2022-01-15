using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timerController : MonoBehaviour
{
    public float timer = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;
        this.GetComponent<Text>().text = Mathf.FloorToInt(timer).ToString();
    }
}
