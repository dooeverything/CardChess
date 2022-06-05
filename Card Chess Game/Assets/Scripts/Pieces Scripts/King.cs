using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Config;

public class King : MonoBehaviour {
    public bool last_ditch_effort = false;
    public void createDots() {
        List<GameObject> dots = new List<GameObject>();
        // 왕-이동: 상하좌우대각선 1칸
        for (int i = 0; i < 8; i++) {
            int new_x = GetComponent<ChessPiece>().getIndex().indexX + (PieceConfig.move_list_surround[i,0]);
            int new_y = GetComponent<ChessPiece>().getIndex().indexY + (PieceConfig.move_list_surround[i,1]);

            if(new_x > 4 || new_x < 0) {
                continue;
            }
            if(new_y > 7 || new_y < 0 ) {
                continue;
            }
            GameObject new_cell = PieceConfig.cells[new_y, new_x];
            
            if(new_cell.gameObject.transform.childCount > 0) {
                continue;
            }

            GameObject dot = Helper.prefabNameToGameObject(Prefab.Move.ToString());
            dot.transform.SetParent(new_cell.transform, false);
            dot.transform.position = new_cell.transform.position;
            dot.GetComponent<MoveController>().parent = gameObject; 
            dots.Add(dot);
        }
        GameManager.dots = dots; 
    }
    
    public void DestroyKing(){        
        PlayerPrefs.SetInt("winner", GetComponent<ChessPiece>().player == 1 ? 2 : 1);

        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current+1);
    }

}
