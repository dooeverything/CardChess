using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartController()
    {
        SceneManager.LoadScene(1);

    }

    public void sceneOneButtonController(){
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }

    public void sceneThreeButtonController(){



        int player = PlayerPrefs.GetInt("player");
        int current = SceneManager.GetActiveScene().buildIndex;

        if(player == 1 && GameManager.selected_mulligan_player1 >= 0) {
            PlayerPrefs.SetInt("player", 2);
            SceneManager.LoadScene(current);
        }
        else if (player ==2 && GameManager.selected_mulligan_player2 >= 0){
            SceneManager.LoadScene(current+1);
        }

    }
}
