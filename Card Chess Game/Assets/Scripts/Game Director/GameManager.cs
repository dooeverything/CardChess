using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config; 
public class GameManager
{
    public static int turn = 1;
    public static GameObject handsPlayer1 = GameObject.Find("Hands");
    public static GameObject handsPlayer2 = GameObject.Find("Hands_Opponent");
    public static bool executing = false;
    public static void destroyAllIndicators()
    {

        foreach (GameObject indicator in indicators)
        {
            indicator.transform.SetParent(null); 
            Object.Destroy(indicator);
        }

        indicators = new List<GameObject>();
    } 

    public static void destroyAlldots()
    {
        foreach (GameObject dot in dots)
        {
            if(dot == null) continue; 
            dot.transform.SetParent(null); 
            Object.Destroy(dot);
        }
        dots = new List<GameObject>(); 
    }
    public static GameManager player1 = new GameManager();
    public static GameManager player2 = new GameManager();

    public List<GameObject> piecesOnBoard = new List<GameObject>(); 

    public List<GameObject> filterList(Piece type) {
        List<GameObject> temp = new List<GameObject>();
        foreach(GameObject piece in piecesOnBoard) {
            if(piece == null) {
                continue;
            }
            if(piece.GetComponent<ChessPiece>().chessPieceType == type) {
                temp.Add(piece);
            }
        }
        return temp;
    }
    public static List<GameObject> dots = new List<GameObject>();

    public List<GameObject> indicator = new List<GameObject>();

    public static List<GameObject> indicators = new List<GameObject>();
    public List<int> card_ingame = new List<int>();

    public List<GameObject> cards_in_hand = new List<GameObject>();
    public List<GameObject> strike = new List<GameObject>();
    public GameObject selected_card;
    public int myDeckCount = 20;

    public List<int> deck = new List<int>();
}
