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
    GameObject indicator;
    GameObject moveIndicator;
    GameObject child;
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        moveIndicator = GameObject.Find("Move Indicator");

        gr = GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        clickToMove();

        if (dragDrop.beingHeld)
        {
            gameObject.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;

            if (dragDrop.moved)
            {
                if (dragDrop.obj_id == transform.GetInstanceID())
                {
                    // Create a selected indicator prefab
                    Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
                    indicator = Instantiate(selected) as GameObject;
                    indicator.transform.SetParent(canvas.transform);
                    indicator.transform.position = transform.position;

                    // Create a dot move indicator prefab 
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


    // To find the click area
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
