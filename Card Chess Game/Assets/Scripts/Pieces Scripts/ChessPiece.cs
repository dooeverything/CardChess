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
    public int indexX;
    public int indexY;
    public cardSave.Piece chessPieceType;
    protected GameObject canvas;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    protected GameObject child;
    protected Object prefab;

    public static ChessPiece createDerivedChessPiece(cardSave.Piece type, GameObject obj, int indexX, int indexY) {
        switch(type) {
            case cardSave.Piece.Archer: 
                return new Archer(type, obj, indexX, indexY);
            case cardSave.Piece.King: 
                return new King(type, obj, indexX, indexY);
            case cardSave.Piece.Mage: 
                return new Mage(type, obj, indexX, indexY);
            case cardSave.Piece.Warrior: 
                return new Warrior(type, obj, indexX, indexY);
        }
        return null; 
    }


    // Constructor
    public ChessPiece(cardSave.Piece type, GameObject obj, int indexX, int indexY) {
        chessPieceType = type;
        parent = obj;
        //x = obj.GetComponent<RectTransform>().anchoredPosition.x;
        //y = obj.GetComponent<RectTransform>().anchoredPosition.y;
        this.indexX = indexX;
        this.indexY = indexY;
    }

    // Spawning a Chess Piece
    public GameObject createPiece(cardSave.Piece type) {
        Object prefabPiece = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) chessPieceType], typeof(GameObject));
        GameObject piece = GameObject.Instantiate(prefabPiece) as GameObject;
        //piece.transform.position = new Vector2(x, y);
        piece.transform.SetParent(parent.transform, false);
        //Debug.Log(x + " " + y);
        // switch(type) {
        //     case cardSave.Piece.Archer: 
        //         Game_Manager.archerOnBoard_player1.Add(piece); 
        //         break; 
        // }
        return piece;
    }

    // Abstract Method, (Overriding Function), To create a valid move indicator, which is different for each piece
    public virtual void createDotMove() {
        return;
    }

    public virtual void createDotStrike() {
        return;
    }

    void Start()
    {
    }

}
