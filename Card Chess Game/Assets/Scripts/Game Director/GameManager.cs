using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;
using static UnityEngine.Debug;
using UnityEngine.SceneManagement;

public class GameManager
{
    /***** Information for managing a game *****/
    public static int selected_mulligan_player1 = -1;
    public static int selected_mulligan_player2 = -1;
    public static float chess_time = 10;
    public static float timer = 10;
    public static int turn = 1;
    public static bool executing = false;
    public static int init_deck_count = 20;
    public static GameManager player1 = new GameManager();
    public static GameManager player2 = new GameManager();
    public static List<GameObject> dots = new List<GameObject>();
    public static List<GameObject> indicators = new List<GameObject>();
    public static GameObject[] kings = new GameObject[2];
    public static bool[] kingDead = {false, false}; 

    /********** Information for each player **********/
    public GameObject selected_card;
    public List<GameObject> piecesOnBoard = new List<GameObject>();
    public List<GameObject> strike = new List<GameObject>();
    public List<GameObject> mana = new List<GameObject>();
    public List<Card> hand = new List<Card>();
    public List<Card> deck = new List<Card>();
    public List<Card> mulligan = new List<Card>();
    public List<int> card_ingame = new List<int>();
    
    public GameManager()
    {
        // Initializes Each Player's Deck
        int index = 0;
        foreach (Card card in System.Enum.GetValues(typeof(Card)))
        {
            index++;
            deck.Insert(Random.Range(0, index), card);
            if (index == init_deck_count) break;
        }
        //Debug.Log("Number of Deck: " + deck.Count);
    }

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
            if (dot == null) continue;
            dot.transform.SetParent(null);
            Object.Destroy(dot);
        }
        dots = new List<GameObject>();
    }
    public static void endTurn() {
        switchTurn(); 
        GameManager player_data;
        destroyAllIndicators();
        destroyAlldots();
        foreach(CardController card in GameObject.Find("Canvas").GetComponentsInChildren<CardController>()) {
            //Log("return to initpos hand1");
            card.returnToInitPos();
        }

        if (GameManager.turn == 1)
        {
            player_data = GameManager.player1;
        }
        else
        {
            player_data = GameManager.player2;
        }

        if(player_data.hand.Count < 4 && player_data.deck.Count > 0 ) {
            addCardToHand();
        }

        if(player_data.mana.Count > 0 || player_data.mana.Count < 9) {
            //Debug.Log("Create Mana!");
            createMana();
        }       

        if(isKingDead()) {
            int current = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(current+1);
        }
        
    }
    public static void switchTurn()
    {
        // 만약 king이 last_ditch_effort를 썼으면 3턴 후에 즉시 사망처리
        
        turn = (turn == 1 ? 2 : 1);
        timer = chess_time;
    }
    public static void addCardToHand()
    {
        GameManager player_data;
        GameObject hand;

        if (GameManager.turn == 1)
        {
            player_data = GameManager.player1;
            hand = GameObject.Find("Hand_P1");
        }
        else
        {
            player_data = GameManager.player2;
            hand = GameObject.Find("Hand_P2");
        }
        Card card = player_data.drawCard();

        GameObject card_object = Config.Helper.cardToGameObject(card);

        //Debug.Log(" Player has "  + player_data.hand.Count);

        card_object.transform.SetParent(hand.transform, true);
        card_object.GetComponent<CardController>().player = GameManager.turn;
        card_object.GetComponent<CardController>().init(GameManager.turn, player_data.hand.Count-1, card);
    }
    public List<GameObject> filterList(Piece type)
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject piece in piecesOnBoard)
        {
            if (piece == null)
            {
                continue;
            }
            if (piece.GetComponent<ChessPiece>().piece_type == type)
            {
                temp.Add(piece);
            }
        }
        return temp;
    }

    public Card drawCard()
    {
        // Draw A Card From The Deck
        
        //Debug.Log("Draw Card!");
        Card card = deck[0];
        deck.RemoveAt(0);
        hand.Add(card);
        return card;
    }
    public Card drawMullgan()
    {
        Card card = deck[0];
        deck.RemoveAt(0);
        mulligan.Add(card);
        return card;
    }
    public Card replaceCard(int index)
    {
        deck.Insert(Random.Range(0, deck.Count + 1), hand[index]);
        hand.RemoveAt(index);
        int index_deck = Random.Range(0, deck.Count);
        Card card = deck[index_deck];
        deck.RemoveAt(index_deck);

        hand.Insert(index, card);
        //Debug.Log(deck.Count);
        return card;
    }
    public void pushBackCard(int index)
    {
        //Debug.Log("Push Back " + deck.Count);
        deck.Insert(Random.Range(0, deck.Count + 1), mulligan[index]);
    }
    public static void lastDotClicked(bool has_card) {
        if(has_card) {
            GameManager.dots[0].GetComponent<DotController>().deleteCard();
        }

        GameManager.destroyAlldots();
        GameManager.destroyAllIndicators();
        GameManager.endTurn();
    }

    public static void createMana()
    {
        GameManager player_data;
        GameObject mana_stack;
        if (GameManager.turn == 1)
        {
            player_data = GameManager.player1;
            mana_stack = GameObject.Find("Mana_P1");
        }
        else
        {
            player_data = GameManager.player2;
            mana_stack = GameObject.Find("Mana_P2");
        }

        if(player_data.mana.Count >= 10) return;

        GameObject new_mana = Helper.prefabNameToGameObject(Prefab.Mana.ToString());

        player_data.mana.Add(new_mana);
        new_mana.transform.SetParent(mana_stack.transform, true);
        //Debug.Log("Create Mana: " +  player_data.mana.Count);
    }
    
    public static bool isKingDead()
    {

        bool king_dead = false;

        if(GameManager.kingDead[1]){
            Debug.Log("Player 2 King is dead, player 1 win....");
            PlayerPrefs.SetInt("winner", 1);
            king_dead = true;
        }else if(GameManager.kingDead[0]){
            Debug.Log("Player 1 King is dead, player 2 win....");
            PlayerPrefs.SetInt("winner", 2);
            king_dead = true;
        }

        return king_dead;
        //PlayerPrefs.SetInt("winner", GetComponent<ChessPiece>().player == 1 ? 2 : 1);
    }
}