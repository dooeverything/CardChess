using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class horseController : MonoBehaviour
{
    private GraphicRaycaster gr;
    int boardIndexX;
    int boardIndexY;
    GameObject canvas;
    GameObject circle2;
    GameObject circle3;
    GameObject archer1;
    GameObject cardTest;
    GameObject indicator;
    GameObject moveIndicator;
    GameObject child;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        circle2 = GameObject.Find("Circle Test_2");
        circle3 = GameObject.Find("Circle Test_3");
        cardTest = GameObject.Find("cardTest(Clone)");
        moveIndicator = GameObject.Find("Move Indicator");

        gr = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        clickToMove();

        if (dragDrop.beingHeld)
        {
            // circle2.GetComponent<SpriteRenderer>().sortingOrder = 2;
            // circle3.GetComponent<SpriteRenderer>().sortingOrder = 2;
            gameObject.GetComponent<Image>().color = Color.blue;
            // if(gameObject.GetComponent<CircleCollider2D>().IsTouching(cardTest.GetComponent<BoxCollider2D>())) {
            // //when card is dropped to horse piece
            //     Debug.Log("Selected This Piece!");
            // }
        }
        else
        {
            //circle1.GetComponent<SpriteRenderer>().sortingOrder = 0;
            // circle2.GetComponent<SpriteRenderer>().sortingOrder = 0;
            // circle3.GetComponent<SpriteRenderer>().sortingOrder = 0;
            gameObject.GetComponent<Image>().color = Color.white;

            if (dragDrop.moved)
            {
                if (dragDrop.obj_id == transform.GetInstanceID())
                {
                    Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
                    indicator = Instantiate(selected) as GameObject;
                    indicator.transform.SetParent(canvas.transform);
                    indicator.transform.position = transform.position;
                    // indicator.transform.SetParent(gameObject.transform);
                    //horsePiece1.transform.position = new Vector2(horsePiece1.transform.position.x, horsePiece1.transform.position.y+160);

                    // in case of moving archer
                    Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot.prefab", typeof(GameObject));

                    // If the selected object is Archer
                    for (int i = 0; i < 4; i++)
                    {
                        GameObject dot = Instantiate(prefab) as GameObject;
                        dot.transform.SetParent(moveIndicator.transform, false);
                        dot.transform.position = new Vector2(transform.position.x + cardSave.positionMove[i, 0] * 160, transform.position.y + cardSave.positionMove[i, 1] * 160);
                    }
                    dragDrop.moved = false;
                }

            }
        }
    }

    public void clickToMove()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                //Debug.Log(result.gameObject.name);
                if (result.gameObject.name == "dot(Clone)")
                {
                    transform.position = result.gameObject.transform.position;
                    Destroy(indicator);

                    foreach (Transform child in moveIndicator.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}
