using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class horseController : MonoBehaviour
{
    private GraphicRaycaster gr;
    protected int boardIndexX;
    protected int boardIndexY;
    protected GameObject canvas;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    protected GameObject child;
    protected Object prefab;

    //Abstract Method, (Overriding Function), To create a valid move indicator, different by each pieces
    protected virtual void createDotMove(Object prefab) {
        return;
    }

    protected virtual void createDotStrike(Object prefab) {
        return;
    }

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        moveIndicator = GameObject.Find("Move Indicator");

        gr = GetComponent<GraphicRaycaster>();
    }

    // To find the click area
    protected virtual void clickToMove(GameObject obj) {
        if (Input.GetKey(KeyCode.Mouse0)) {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results) {
                //Debug.Log(result.gameObject.name);
                if (result.gameObject.name == "dot(Clone)")
                {
                    Debug.Log(obj.name);
                    obj.transform.position = result.gameObject.transform.position;
                    //  = result.gameObject.transform.position;
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
