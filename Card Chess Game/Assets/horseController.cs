using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class horseController : MonoBehaviour
{
    GameObject circle2;
    GameObject circle3;
    GameObject archer1;
    GameObject cardTest;
    GameObject selectedObject;

    void Start()
    {   
        circle2 = GameObject.Find("Circle Test_2");
        circle3 = GameObject.Find("Circle Test_3");
        cardTest = GameObject.Find("cardTest(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if(dragDrop.beingHeld) {
            // circle2.GetComponent<SpriteRenderer>().sortingOrder = 2;
            // circle3.GetComponent<SpriteRenderer>().sortingOrder = 2;
            gameObject.GetComponent<Image>().color = Color.blue;

            // if(gameObject.GetComponent<CircleCollider2D>().IsTouching(cardTest.GetComponent<BoxCollider2D>())) {
            // //when card is dropped to horse piece
            //     Debug.Log("Selected This Piece!");
            // }
        }else {
            //circle1.GetComponent<SpriteRenderer>().sortingOrder = 0;
            // circle2.GetComponent<SpriteRenderer>().sortingOrder = 0;
            // circle3.GetComponent<SpriteRenderer>().sortingOrder = 0;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
