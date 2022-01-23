using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadNextScene() {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }

    public void loadInGameScene() {
        // for(int i=0; i<cardSave.cardList.Length; i++) {
        //     Debug.Log(cardSave.cardList[i]);
        // }
        SceneManager.LoadScene("InGame Scene"); 
    }

    
}
