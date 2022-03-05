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
    public bool activated = false; // either selected by card or clicking the piece itself
    public int [,] basic_moves; 
    void Start()
    {
        if (player == 1)
        {
            player_data = Game_Manager.player1;
            basic_moves = new int[,]{{-1, 0}, {1, 0}, {0, 1}}; 
        }
        else
        {
            player_data = Game_Manager.player2;
            basic_moves = new int[,]{{-1, 0}, {1, 0}, {0, -1}}; 
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // exits if not piece owner's turn 
        if (player != Game_Manager.turn) return;
        // deactivate previously selected piece
        if(Game_Manager.selected_piece) {
            Game_Manager.selected_piece.GetComponent<ChessPiece>().activated = false;  
        }
        // remove all indicators and dots
        Game_Manager.destroyAllIndicators(); 
        Game_Manager.destroyAlldots(); 

        // If the piece is active //
        if (activated)
        {
            activated = false; 
            return;
        }
        activated = true; 
        // create indicator around the piece itself
        createIndicator(); 
        if(chessPieceType == cardSave.Piece.King) {
            GetComponent<King>().createDots();
        } else {
            createDots(); 
        }
        // Game_Manager.selected_piece = gameObject;

    }

    public void createDots() {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < basic_moves.GetLength(0); i++) {
            int newIndexX = GetComponent<ChessPiece>().indexX + (basic_moves[i,0]);
            int newIndexY = GetComponent<ChessPiece>().indexY + (basic_moves[i,1]);
            if(newIndexX > 4 || newIndexX < 0) {
                continue;
            }
            if(newIndexY > 7 || newIndexY < 0 ) {
                continue;
            }
            GameObject newCell = cardSave.cells[newIndexX, newIndexY];
            
            if(newCell.gameObject.transform.childCount > 0) {
                if(newCell.transform.GetChild(0).name == "dot_move(Clone)") {
                } else {
                    if(newCell.transform.GetChild(0).GetComponent<ChessPiece>().player != GetComponent<ChessPiece>().player ) {
                        // 말이 적일 경우
                        dots.Add(createStrike(newCell, newIndexX, newIndexY));
                    }
                    continue;
                }
            }

            Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/dot_move.prefab", typeof(GameObject));
            GameObject dot = GameObject.Instantiate(prefab) as GameObject;
            dot.transform.SetParent(newCell.transform, false);
            dot.transform.position = newCell.transform.position;
            dot.GetComponent<dotController>().parent = gameObject; 
            dots.Add(dot);
        }
        Game_Manager.dots = dots; 
    }

    public void createIndicator()
    {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject selected_indicator = Instantiate(prefab) as GameObject;
        selected_indicator.transform.SetParent(gameObject.transform);
        selected_indicator.transform.position = transform.position;
        Game_Manager.indicators.Add(selected_indicator); 
    }

    public GameObject createStrike(GameObject cell, int indexX, int indexY) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<strikeController>().indexX = indexX;
        striking.GetComponent<strikeController>().indexY = indexY;
        return striking;
    }

    public void destroyIndicator()
    {
        if (this.transform.childCount != 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    public void createIndicatorForCard()
    {
        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject indicator = Instantiate(selected) as GameObject;
        player_data.indicator.Add(indicator);
        //Debug.Log("indicator is: " + indicator.transform.position.x); 
        indicator.transform.SetParent(this.gameObject.transform);
        indicator.transform.position = transform.position;
        // Destroy(indicator); 
        //Debug.Log("id is: " + indicator.GetInstanceID()); 
    }

    
    // public void destroyAllIndicators() {
    //     foreach (GameObject indicator in Game_Manager.indicators)
    //     {
    //         Destroy(indicator);
    //     }
    // }

    // public static void destroyAlldots() {
    //     foreach (GameObject dot in Game_Manager.dots)
    //     {
    //         Destroy(dot);
    //     }
    // }
}
