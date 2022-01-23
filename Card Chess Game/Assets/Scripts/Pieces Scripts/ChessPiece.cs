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
    protected int player;
    protected Game_Manager player_data;
    public static ChessPiece createDerivedChessPiece(int player, cardSave.Piece type, GameObject obj, int indexX, int indexY) {
        switch(type) {
            case cardSave.Piece.Archer: 
                return new Archer(player, type, obj, indexX, indexY);
            case cardSave.Piece.King: 
                return new King(player, type, obj, indexX, indexY);
            case cardSave.Piece.Mage: 
                return new Mage(player, type, obj, indexX, indexY);
            case cardSave.Piece.Warrior: 
                return new Warrior(player, type, obj, indexX, indexY);
        }
        return null; 
    }


    // Constructor
    public ChessPiece(int player, cardSave.Piece type, GameObject obj, int indexX, int indexY) {
        this.player = player; 
        chessPieceType = type;
        parent = obj;
        //x = obj.GetComponent<RectTransform>().anchoredPosition.x;
        //y = obj.GetComponent<RectTransform>().anchoredPosition.y;
        this.indexX = indexX;
        this.indexY = indexY;
        if(player == 1) {
            player_data = Game_Manager.player1;
        }else {
            player_data = Game_Manager.player2;            
        }
    }

    // Spawning a Chess Piece
    public GameObject createPiece(int player) {
        Object prefabPiece = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) chessPieceType], typeof(GameObject));
        GameObject piece = GameObject.Instantiate(prefabPiece) as GameObject;
        piece.GetComponent<PieceController>().player = player;
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
