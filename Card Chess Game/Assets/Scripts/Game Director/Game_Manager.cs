using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class Game_Manager
{
    // public  int a = 1; 

    public static Game_Manager player1 = new Game_Manager();
    public static Game_Manager player2 = new Game_Manager();

    public List<GameObject> archerOnBoard = new List<GameObject>();
    public  List<ChessPiece> archerConstructors = new List<ChessPiece>();

    public  List<GameObject> mageOnBoard = new List<GameObject>();
    public  List<ChessPiece> mageConstructors = new List<ChessPiece>();

    public  List<GameObject> warriorOnBoard = new List<GameObject>();
    public  List<ChessPiece> warriorConstructors = new List<ChessPiece>();

    public  List<GameObject> kingOnBoard = new List<GameObject>();
    public  List<ChessPiece> kingConstructors = new List<ChessPiece>();

    public  List<GameObject> dots = new List<GameObject>();

    public  List<GameObject> indicator = new List<GameObject>();

    public  List<int> card_ingame = new List<int>();

    public  List<GameObject> cards_in_hand = new List<GameObject>();

    public  GameObject selected_card;

    public  int myDeckCount = 39;

    public  List<int> deck = new List<int>();

    public static int turn = 1;

    public static GameObject handsPlayer1 = GameObject.Find("Hands");
    public static GameObject handsPlayer2 = GameObject.Find("Hands_Opponent");
 

}
