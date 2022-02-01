using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ChessPiece : MonoBehaviour, IPointerDownHandler
{
    //GameObject parent; // Cell GameObject
    public int indexX;
    public int indexY;
    public cardSave.Piece chessPieceType;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    public int player;
    protected Game_Manager player_data;
    public List<GameObject> indicators = null;
    void Start()
    {
        if(player == 1) {
            player_data = Game_Manager.player1;
        }else {
            player_data = Game_Manager.player2;
        }
    }


    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("HIHI");
        switch (chessPieceType)
        {
            case cardSave.Piece.Archer:
                indicators = GetComponent<Archer>().createIndicator();
                break;

            case cardSave.Piece.Warrior:
                indicators = GetComponent<Warrior>().createIndicator();
                break;

            case cardSave.Piece.Mage:
                indicators = GetComponent<Mage>().createIndicator();
                break;

            case cardSave.Piece.King:
                indicators = GetComponent<King>().createIndicator();
                break;

            default:
                break;
        }
    } 
}
