using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;
public class Archer : MonoBehaviour {
    public int attack_range = 4;
    public int num_enemy = 0;
    public bool only_attack = false;
    public GameObject createStrikeDot(GameObject cell, GameObject enemy, GameObject card, GameObject parent) {
        GameObject striking = Helper.prefabNameToGameObject(Prefab.Arrow.ToString());
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<ArrowController>().enemy = enemy;
        striking.GetComponent<ArrowController>().card = card;
        striking.GetComponent<ArrowController>().moveWhenAttack = false;
        striking.GetComponent<ArrowController>().parent = parent;
        return striking;
    }

    public GameObject createMoveDot(GameObject new_cell, GameObject card) {
        GameObject move = Helper.prefabNameToGameObject(Prefab.Move.ToString());
        move.transform.SetParent(new_cell.transform, false);
        move.transform.position = new_cell.transform.position;
        move.GetComponent<MoveController>().parent = gameObject; 
        move.GetComponent<MoveController>().card = card;
        return move;
    }

    public void createArrowArcher(List<int[]> move_list, GameObject card = null, int num_attack = 1)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < 3; i++) {
            for(int j = 1; j<attack_range; j++) {
                int new_x = GetComponent<ChessPiece>().getIndex().indexX + (move_list[i][0]*j);
                int new_y = GetComponent<ChessPiece>().getIndex().indexY + (move_list[i][1]*j);
                
                // Check the location is out of bound 
                if(Helper.outOfBoard(new_x, new_y)) continue;

                GameObject new_cell = PieceConfig.cells[new_y, new_x];
                if(new_cell.transform.childCount > 0) {

                    // The enemy's Piece is located within the range of archer's attack, then create a strike dot  
                    GameObject enemy = new_cell.transform.GetChild(0).gameObject;
                    if(Helper.isEnemy(gameObject, enemy) ) {
                        dots.Add(createStrikeDot(new_cell, enemy, card, this.gameObject));
                        num_enemy++;
                    }

                    // If there is any blocking piece, then further iteration is no needed 
                    // (the piece further than blocking piece cannot be attacked)
                    break; 
                }else if(!only_attack){
                    // If there is no blocking piece on the location where archer can move to
                    if(j==1){
                        dots.Add(createMoveDot(new_cell, card));
                    }
                }
            }
        }
        ArrowController.num_attack = System.Math.Min(num_attack, num_enemy); 
        only_attack = false;
        GameManager.dots = dots; 
    }

}
