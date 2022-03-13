using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class strikeController : MonoBehaviour, IPointerDownHandler
{
    public GameObject enemy;
    public GameObject card;
    public int indexX;
    public int indexY;
    public bool moveWhenAttack = true;
    public GameObject parent; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        if(moveWhenAttack) {
            Transform cellTransform = gameObject.transform.parent; // parent is a Cell gameobject

            parent.transform.SetParent(cellTransform); // selected piece의 부모는 selected piece가 이동할 cell
            parent.transform.position = cellTransform.position; // 위치 조정
            parent.GetComponent<ChessPiece>().indexX = cellTransform.gameObject.GetComponent<cellController>().indexX;
            parent.GetComponent<ChessPiece>().indexY = cellTransform.gameObject.GetComponent<cellController>().indexY;        
        }
        
        // Get Opponent's piece gameobject and destory it (kill)
        // GameObject attacked_piece = cell.GetChild(0).gameObject;
        // Destroy(attacked_piece);

        // Game_Manager.selected_piece.transform.SetParent(cell.transform);
        // Game_Manager.selected_piece.transform.position = cell.position;
        // Game_Manager.selected_piece.GetComponent<ChessPiece>().indexX = indexX;
        // Game_Manager.selected_piece.GetComponent<ChessPiece>().indexY = indexY;

        // Game_Manager.selected_piece = null;
        
        Game_Manager.destroyAlldots(); 
        Game_Manager.destroyAllIndicators(); 

        if(card) {
            dragDrop temp = card.GetComponent<dragDrop>(); 
            List<GameObject> list = temp.player_data.cards_in_hand;
            int index = -1;
            for (int i=0; i<list.Count; i++) {
                if(card == list[i]){
                    index = i;
                    break;
                }

            }
            temp.player_data.cards_in_hand.RemoveAt(index);
            Destroy(card); 
        }
        
        {
            ChessPiece piece = enemy.GetComponent<ChessPiece>();
            List<GameObject> list = piece.player_data.piecesOnBoard;
            int index = -1;
            for (int i=0; i<list.Count; i++) {
                if(enemy == list[i]){
                    index = i;
                    break;
                }

            }
            list.RemoveAt(index);
            Destroy(enemy); 
        }


        // Switch turn after strike
        endButtonController.switchTurn();
    }

}
