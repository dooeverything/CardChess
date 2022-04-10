using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] Text player1;
    [SerializeField] Text result;
    [SerializeField] WheelController wheelScript;

    [SerializeField] private Image resultImage;
    // Start is called before the first frame update

    [SerializeField] private float delay = 10.0f;
    //[SerializeField] private string sceneToLoad;
    private float timeElapsed;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(wheelScript.finish()) {
            resultImage.enabled = true;
            result.text = (wheelScript.result() == 1) ? "Tail" : "Head"; 
            timeElapsed += Time.deltaTime;
            if(timeElapsed > delay) {
                int playerResult = (player1.text == result.text) ? 1 : 0; 
                PlayerPrefs.SetInt("result", playerResult);
                loadNextScene(); 
            }
        }
    }
    private void loadNextScene() {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }
}
