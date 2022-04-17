using System.Linq; 
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
    public static bool executing = false;
    public static int init_deck_count = 20; 
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
    public  GameObject hand_game_obj; 
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
    public List<Card> hand = new List<Card>(); 
    public List<Card> deck = new List<Card>(); 

    public GameManager() {
        // Initializes Each Player's Deck
        int index = 0; 
        foreach (Card card in System.Enum.GetValues(typeof(Card))) {
            index++; 
            player1.deck.Insert(Random.Range(0, index), card); 
            player2.deck.Insert(Random.Range(0, index), card); 
            if(index == 20) break; 
        }
        // Fetching Player Hands GameObject
        player1.hand_game_obj = GameObject.Find("Hand_P1");
        player2.hand_game_obj = GameObject.Find("Hands_P2");
    }

    // Draw A Card From The Deck
    public Card drawCard() {
        Card card = deck[0]; 
        deck.RemoveAt(0); 
        hand.Add(card);
        return card;  
    }
    public Card replaceCard(int index) {
        deck.Insert(Random.Range(0, deck.Count + 1), hand[index]); 
        index = Random.Range(0, deck.Count); 
        Card card = deck[Random.Range(0, deck.Count)]; 
        deck.RemoveAt(index); 
        return card; 
    }







}
