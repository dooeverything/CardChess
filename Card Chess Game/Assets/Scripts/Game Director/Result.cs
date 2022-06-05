using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Config; 
public class Result : MonoBehaviour
{
    [SerializeField] Text player1;
    [SerializeField] Text result;
    [SerializeField] WheelController wheel_script;

    [SerializeField] private Image result_image;
    // Start is called before the first frame update

    [SerializeField] private float delay = 10.0f;
    //[SerializeField] private string sceneToLoad;
    private float time_elapsed;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(wheel_script.finish()) {
            result_image.gameObject.SetActive(true);
            result.text = (wheel_script.result() == (int) Constants.P1_First) ? "P1 First!" : "P2 First!"; 
            time_elapsed += Time.deltaTime;
            if(time_elapsed > delay) {
                PlayerPrefs.SetInt("result", (int) wheel_script.result());
                PlayerPrefs.SetInt("player", 1);
                loadNextScene(); 
            }
        }
    }
    private void loadNextScene() {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }
}
