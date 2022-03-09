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

    public static void destroyAllIndicators()
    {
        foreach (GameObject indicator in Game_Manager.indicators)
        {
            UnityEngine.Object.Destroy(indicator);
        }
    }

    public static void destroyAlldots()
    {
        foreach (GameObject dot in Game_Manager.dots)
        {
            UnityEngine.Object.Destroy(dot);
        }
    }
    public static Game_Manager player1 = new Game_Manager();
    public static Game_Manager player2 = new Game_Manager();

    public List<GameObject>[] piecesOnBoard = new List<GameObject>[4]; 

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
