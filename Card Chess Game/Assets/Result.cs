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
    int playerResult = 0;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        wheelScript = FindObjectOfType<WheelController>();
        if(wheelScript.finish()) {
            resultImage.enabled = true;
            //Debug.Log("Good!");
            if(wheelScript.result() == 1 ) {
                result.text = "Tail";
            }else {
                //Debug.Log("Head");
                result.text = "Head";
            }

            timeElapsed += Time.deltaTime;
            if(timeElapsed > delay) {
                int current = SceneManager.GetActiveScene().buildIndex;
                if(player1.text == result.text) {
                    Debug.Log("True");
                    playerResult = 1;
                    //SceneManager.LoadScene(current+1);
                }else {
                    Debug.Log("False");
                    playerResult = 0;
                    //SceneManager.LoadScene(current+2);
                }
                SceneManager.LoadScene("Choose Card Scene");
            }
        }
    }

    void OnDisable() {
        PlayerPrefs.SetInt("result", playerResult);
    }
    public Text getResult() {
        return result;
    }
}
