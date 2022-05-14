using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = $"Winner is Player {PlayerPrefs.GetInt("winner").ToString()}!!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
