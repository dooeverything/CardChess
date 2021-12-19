using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ChessPiece
{
    private GraphicRaycaster gr;
    GameObject parent;
    
    protected float x;
    protected float y;
    protected int indexX;
    protected int indexY;
    public int chessPieceType;
    protected GameObject canvas;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    protected GameObject child;
    protected Object prefab;


    // Constructor
    public ChessPiece(int type, GameObject obj, int indexX, int indexY) {
        chessPieceType = type;
        parent = obj;
        //x = obj.GetComponent<RectTransform>().anchoredPosition.x;
        //y = obj.GetComponent<RectTransform>().anchoredPosition.y;
        this.indexX = indexX;
        this.indexY = indexY;
    }

    // Spawning a Chess Piece
    public Object createPiece() {
        Object prefabPiece = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) chessPieceType], typeof(GameObject));
        GameObject piece = GameObject.Instantiate(prefabPiece) as GameObject;
        //piece.transform.position = new Vector2(x, y);
        piece.transform.SetParent(parent.transform, false);
        Debug.Log(x + " " + y);
        return piece;
    }

    // Abstract Method, (Overriding Function), To create a valid move indicator, which is different for each piece
    protected virtual void createDotMove(Object prefab) {
        return;
    }

    protected virtual void createDotStrike(Object prefab) {
        return;
    }

    void Start()
    {
        //canvas = GameObject.Find("Canvas");
        //moveIndicator = GameObject.Find("Move Indicator");

        //gr = GetComponent<GraphicRaycaster>();
    }

    protected virtual void movePiece() {

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
                    GameObject.Destroy(indicator);

                    foreach (Transform child in moveIndicator.transform)
                    {
                        GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
    }
}
