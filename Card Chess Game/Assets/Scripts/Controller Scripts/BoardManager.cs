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
                ChessPiece newPiece1 = ChessPiece.createDerivedChessPiece(1, cardSave.pieces[i,j], cardSave.cells[j, i], j, i);
                ChessPiece newPiece2 = ChessPiece.createDerivedChessPiece(2, cardSave.pieces[i,j], cardSave.cells[j, 7-i], j, 7-i);
                cardSave.Piece type  = newPiece1.chessPieceType;
                switch(type) {
                    case cardSave.Piece.Archer:
                        Game_Manager.player1.archerConstructors.Add(newPiece1);
                        Game_Manager.player1.archerOnBoard.Add(newPiece1.createPiece(1));
                        Game_Manager.player2.archerConstructors.Add(newPiece2);
                        Game_Manager.player2.archerOnBoard.Add(newPiece2.createPiece(2));  
                        break;
                    case cardSave.Piece.Mage:
                        Game_Manager.player1.mageConstructors.Add(newPiece1);
                        Game_Manager.player1.mageOnBoard.Add(newPiece1.createPiece(1));
                        Game_Manager.player2.mageConstructors.Add(newPiece2);
                        Game_Manager.player2.mageOnBoard.Add(newPiece2.createPiece(2));
                        break;
                    case cardSave.Piece.Warrior:
                        Game_Manager.player1.warriorConstructors.Add(newPiece1);
                        Game_Manager.player1.warriorOnBoard.Add(newPiece1.createPiece(1));
                        Game_Manager.player2.warriorConstructors.Add(newPiece2);
                        Game_Manager.player2.warriorOnBoard.Add(newPiece2.createPiece(2));
                        break;
                    case cardSave.Piece.King:
                        Game_Manager.player1.kingConstructors.Add(newPiece1);
                        Game_Manager.player1.kingOnBoard.Add(newPiece1.createPiece(1));
                        Game_Manager.player2.kingConstructors.Add(newPiece2);
                        Game_Manager.player2.kingOnBoard.Add(newPiece2.createPiece(2));
                        break;
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
