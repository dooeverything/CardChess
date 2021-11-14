using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boxController : MonoBehaviour
{
    public Sprite trashcan_close;
    public Sprite trashcan_open;


    //public cardGenerator cardScript;
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Enter");
        this.gameObject.GetComponent<SpriteRenderer>().sprite = trashcan_open;
        this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        /*if(other.gameObject.name == "Card(Clone)") {
            Debug.Log("hi");
            gameObject.GetComponent<SpriteRenderer>().sprite = trashcan_close;
            Debug.Log("not working?");
        }*/
        Debug.Log("Exit");
        gameObject.GetComponent<SpriteRenderer>().sprite = trashcan_close;
    }
}
