using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config; 

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
        int ypos = 0;
        GameObject prefab_board_cell = Helper.prefabNameToGameObject(Prefab.Cell.ToString()); 
        for(int y = -3; y <= 4; y++){
            int xpos = 0;
            for(int x = -2; x <= 2; x++) {
                cellPrefab.name = ("Cell(x, y): "+ xpos + " " + ypos);
                cellPrefab.transform.position = new Vector2( 150 * x, 150 * y - 75 );
                cellPrefab.transform.SetParent(gameObject.transform, false);
                cellPrefab.GetComponent<cellController>().indexX = xpos;
                cellPrefab.GetComponent<cellController>().indexY = ypos;
                PieceConfig.cells[xpos, ypos] = cellPrefab;
                if((x + y + 5) % 2 == 0) {
                    cellPrefab.GetComponent<Image>().color =  cell_Brown; //Color.black;
                } else {
                    cellPrefab.GetComponent<Image>().color =  cell_Dark_Brown; //Color.white;
                }
                xpos++;
            }
            ypos++;
        }

        // Create a ChessPiece for player1, player2
        for(int i=0; i<2; i++) {
            for(int j = 0; j < 5; j++) {
                Piece type = PieceConfig.pieces_on_board[i, j]; 
                //Chesspiece for player1
                GameObject piece = Helper.prefabNameToGameObject(type.ToString()); 
                piece.transform.SetParent(PieceConfig.cells[i, j].transform);
                piece.transform.position = PieceConfig.cells[i, j].transform.position;
                piece.GetComponent<ChessPiece>().player = 1;
                piece.GetComponent<ChessPiece>().chessPieceType = type; 
                piece.GetComponent<ChessPiece>().indexX = j;
                piece.GetComponent<ChessPiece>().indexY = i;
                addPiece(piece, 1);

                //Chesspiece for player2
                GameObject piece2 = Helper.prefabNameToGameObject(type.ToString()); 
                piece2.transform.SetParent(PieceConfig.cells[7-i, j].transform);
                piece2.transform.position = PieceConfig.cells[7-i, j].transform.position;
                piece2.GetComponent<ChessPiece>().player = 2;
                piece2.GetComponent<ChessPiece>().chessPieceType = type; 
                piece2.GetComponent<ChessPiece>().indexX = j;
                piece2.GetComponent<ChessPiece>().indexY = 7-i;
                addPiece(piece2, 2);
            }
        }
    }

    void addPiece(GameObject piece, int player){

        GameManager player_data;

        if (player == 1)
        {
            player_data = GameManager.player1;
        }
        else
        {
            player_data = GameManager.player2;
        }
        player_data.piecesOnBoard.Add(piece); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
