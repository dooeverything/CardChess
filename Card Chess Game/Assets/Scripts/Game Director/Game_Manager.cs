using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class Game_Manager
{
    public static int turn = 1;

    public static GameObject handsPlayer1 = GameObject.Find("Hands");
    public static GameObject handsPlayer2 = GameObject.Find("Hands_Opponent");

    async public static void destroyAllIndicators()
    {
        Debug.Log("Destroy Indicators");
        Debug.Log("Size of indicators is: " + Game_Manager.indicators.Count);
        foreach (GameObject indicator in Game_Manager.indicators)
        {
            indicator.transform.SetParent(null); 
            UnityEngine.Object.Destroy(indicator);
        }

        indicators = new List<GameObject>();
    } 

    public static void destroyAlldots()
    {
        foreach (GameObject dot in Game_Manager.dots)
        {
            dot.transform.SetParent(null); 
            UnityEngine.Object.Destroy(dot);
        }
        dots = new List<GameObject>(); 
    }
    public static Game_Manager player1 = new Game_Manager();
    public static Game_Manager player2 = new Game_Manager();

    public List<GameObject> piecesOnBoard = new List<GameObject>(); 

    public List<GameObject> filterList(cardSave.Piece type) {
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
