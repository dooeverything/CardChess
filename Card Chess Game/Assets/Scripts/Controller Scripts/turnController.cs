using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = GameManager.turn.ToString();
    }
}
