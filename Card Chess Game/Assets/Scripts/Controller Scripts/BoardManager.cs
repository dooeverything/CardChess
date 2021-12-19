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
        int xpos = 1;
        Object prefab_board_cell = AssetDatabase.LoadAssetAtPath(cardSave.board_cell_path, typeof(GameObject));
        for(int x = -2; x <= 2; x++){
            int ypos = 1;
            for(int y = -3; y <= 4; y++) {
                cellPrefab = Instantiate(prefab_board_cell) as GameObject;
                cellPrefab.name = ("Cell(x, y): "+ xpos + " " + ypos);
                //RectTransform rectTransform = cellPrefab.GetComponent<RectTransform>();
                //rectTransform.anchoredPosition = new Vector2( 150*x, (150*y) - 75 );
                cellPrefab.transform.position = new Vector2( 150*x, (150*y) - 75 );
                cellPrefab.transform.SetParent(gameObject.transform, false);
                cardSave.cells[x+2, y+3] = cellPrefab;
                if((x + y + 5) % 2 == 0) {
                    cellPrefab.GetComponent<Image>().color =  cell_Brown; //Color.black;
                } else {
                    cellPrefab.GetComponent<Image>().color =  cell_Dark_Brown; //Color.white;
                }
                ypos++;
            }
            xpos++;
        }

        // Create a ChessPiece
        for(int i=0; i<2; i++) {
            for(int j = 0; j < 5; j++) {
            // Create a Warrior
            ChessPiece newPiece = ChessPiece.createDerivedChessPiece(cardSave.pieces[i,j], cardSave.cells[j, i], j, i);
            newPiece.createPiece(cardSave.pieces[i,j]);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
