using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

// Create every pieces on the chess board
public class BoardManager : MonoBehaviour
{
    GameObject cellPrefab;
    GameObject piecePrefab;
    Color cell_Brown;
    Color cell_Dark_Brown;
    // Start is called before the first frame update
    void Start()
    {
        // Set two colors for cells
        ColorUtility.TryParseHtmlString("#ECDBAC", out cell_Brown);
        ColorUtility.TryParseHtmlString("#71552E", out cell_Dark_Brown);

        
        // Create a board with cell prefab
        int xpos = 0;
        Object prefab_board_cell = AssetDatabase.LoadAssetAtPath(cardSave.board_cell_path, typeof(GameObject));
        for(int x = -2; x <= 2; x++){
            int ypos = 0;
            for(int y = -3; y <= 4; y++) {
                cellPrefab = Instantiate(prefab_board_cell) as GameObject;
                cellPrefab.name = ("Cell(x, y): "+ xpos + " " + ypos);
                //RectTransform rectTransform = cellPrefab.GetComponent<RectTransform>();
                //rectTransform.anchoredPosition = new Vector2( 150*x, (150*y) - 75 );
                cellPrefab.transform.position = new Vector2( 150*x, (150*y) - 75 );
                cellPrefab.transform.SetParent(gameObject.transform, false);
                cellPrefab.GetComponent<cellController>().indexX = xpos;
                cellPrefab.GetComponent<cellController>().indexY = ypos;
                cardSave.cells[xpos, ypos] = cellPrefab;
                if((x + y + 5) % 2 == 0) {
                    cellPrefab.GetComponent<Image>().color =  cell_Brown; //Color.black;
                } else {
                    cellPrefab.GetComponent<Image>().color =  cell_Dark_Brown; //Color.white;
                }
                ypos++;
            }
            xpos++;
        }

        // Create a ChessPiece for player1, player2
        for(int i=0; i<2; i++) {
            for(int j = 0; j < 5; j++) {

                //Chesspiece for player1
                Object prefabPiece = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.pieces[i,j]], typeof(GameObject)); // Get Location of the prefab to create a prefab
                GameObject piece = GameObject.Instantiate(prefabPiece) as GameObject;; // Instantiate prefab
                piece.transform.SetParent(cardSave.cells[j, i].transform);
                piece.transform.position = cardSave.cells[j, i].transform.position;
                piece.GetComponent<ChessPiece>().player = 1;
                piece.GetComponent<ChessPiece>().chessPieceType = cardSave.pieces[i,j];
                piece.GetComponent<ChessPiece>().indexX = j;
                piece.GetComponent<ChessPiece>().indexY = i;
            
                //Chesspiece for player2
                GameObject piece2 = GameObject.Instantiate(prefabPiece) as GameObject;; // Instantiate prefab
                piece2.transform.SetParent(cardSave.cells[j, 7-i].transform);
                piece2.transform.position = cardSave.cells[j, 7-i].transform.position;
                piece2.GetComponent<ChessPiece>().player = 2;
                piece2.GetComponent<ChessPiece>().chessPieceType = cardSave.pieces[i,j];
                piece2.GetComponent<ChessPiece>().indexX = j;
                piece2.GetComponent<ChessPiece>().indexY = 7-i;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
