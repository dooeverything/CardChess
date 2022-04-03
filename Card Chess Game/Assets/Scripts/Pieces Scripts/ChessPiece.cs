using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ChessPiece : MonoBehaviour, IPointerDownHandler
{
    public int offensePower;
    public int defensePower;
    public int indexX;
    public int indexY;
    public cardSave.Piece chessPieceType;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    public int player;
    public Game_Manager player_data;
    public List<GameObject> indicators = null;
    public bool activated = false; // either selected by card or clicking the piece itself
    public List<int[]> basic_moves = new List<int[]>(); 
    public int move_dir; 

    void Start()
    {

        offensePower = 1;
        defensePower = 1;

        basic_moves.Add(new int[]{-1, 0}); 
        basic_moves.Add(new int[]{1, 0}); 
        if (player == 1)
        {
            player_data = Game_Manager.player1;
            basic_moves.Add(new int[]{0, 1}); 
            move_dir = 1; 

        }
        else
        {
            player_data = Game_Manager.player2;
            basic_moves.Add(new int[]{0, -1}); 
            move_dir = -1; 
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // exits if not piece owner's turn 
        if (player != Game_Manager.turn) return;
        // remove all indicators and dots
        Game_Manager.destroyAllIndicators(); 
        Game_Manager.destroyAlldots(); 
        bool activated = this.activated; 

        // If the piece is active //
        if (activated)
        {
            activated = false; 
            return;
        }
        activated = true; 

        // If the piece is mage, then do nothing --> mage cannot both attack and move without a card!
        // if(chessPieceType == cardSave.Piece.Mage)
        //     return;

        // create indicator around the piece itself
        addIndicator(); 
        if(chessPieceType == cardSave.Piece.King) {
            GetComponent<King>().createDots(); // Speical move for King
        } else if(chessPieceType == cardSave.Piece.Archer){
            GetComponent<Archer>().numEnemy = 0;
            GetComponent<Archer>().createDots_Archer(basic_moves);
        }else {
            createDots(basic_moves); 
        }
    }

    public GameObject createDot(GameObject cell, GameObject card) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
        GameObject dot = GameObject.Instantiate(prefab) as GameObject;
        dot.transform.SetParent(cell.transform, false);
        dot.transform.position = cell.transform.position;
        dot.GetComponent<dotController>().parent = gameObject; 
        dot.GetComponent<dotController>().card = card;
        return dot; 
    }

    public void createDots(List<int[]> move_list, GameObject card = null) {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < move_list.Count; i++) {
            int[] coordinates = move_list[i]; 
            int newIndexX = GetComponent<ChessPiece>().indexX + (move_list[i][0]);
            int newIndexY = GetComponent<ChessPiece>().indexY + (move_list[i][1]);
            if(newIndexX > 4 || newIndexX < 0) {
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                continue;
            }
            GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                //if(newCell.gameObject.transform.GetChild(0).name == "dot_move(Clone)" ) continue;
                // 말이 적일 경우
                if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
                    dots.Add(createStrike(newCell, newCell.transform.GetChild(0).gameObject, card));
                }
                continue;
            }

            dots.Add(createDot(newCell, card));
        }
        Game_Manager.dots = dots; 
    }

    public void addIndicator()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject selected_indicator = Instantiate(prefab) as GameObject;
        selected_indicator.transform.SetParent(gameObject.transform);
        selected_indicator.transform.position = transform.position;
        Game_Manager.indicators.Add(selected_indicator); 
    }

    public GameObject createStrike(GameObject cell, GameObject enemy, GameObject card) {
        Debug.Log("CreateStrike Called"); 
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<strikeController>().enemy = enemy;
        striking.GetComponent<strikeController>().card = card;
        striking.GetComponent<strikeController>().parent = gameObject;

        return striking;
    }

    public void destroyIndicator()
    {
        if (this.transform.childCount != 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }
}
