using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class strikeController : MonoBehaviour, IPointerDownHandler
{
    public GameObject enemy;
    public GameObject card;
    public bool moveWhenAttack = true;
    public GameObject parent; 
    public static int numAttack = 1;
    // Start is called before the first frame update
    //public static int count = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        int numEnemy = 0;
        deleteCard();

        {
            ChessPiece piece = enemy.GetComponent<ChessPiece>();
            int hp_enemy = piece.defensePower;
            int st_self = parent.GetComponent<ChessPiece>().offensePower;
            piece.defensePower = hp_enemy - st_self;

            if(piece.defensePower <= 0) {
                List<GameObject> list = piece.player_data.piecesOnBoard;
                int index = -1;
                for (int i=0; i<list.Count; i++) {
                    if(enemy == list[i]){
                        index = i;
                        break;
                    }
                }
                list.RemoveAt(index);
                numAttack--;
                Debug.Log(numAttack);
                Destroy(enemy); 
                if(parent.GetComponent<ChessPiece>().chessPieceType != cardSave.Piece.Archer) {
                    moveParent();
                }else {
                    parent.GetComponent<Archer>().attackRange = 4; // Set archer's attack range as 4 (basic attack range) 
                    numEnemy = parent.GetComponent<Archer>().numEnemy;
                }
            }
        }

        if(parent.GetComponent<ChessPiece>().offensePower > 1) {
            parent.GetComponent<ChessPiece>().offensePower--;
        }

        if(numAttack == 0 || numEnemy == 1) {
            Game_Manager.destroyAllIndicators();
            Game_Manager.destroyAlldots();
            // Switch turn after strike
            endButtonController.switchTurn();
            numAttack = 1;
        }
    }

    public void deleteCard()
    {
        Debug.Log("Delete card!");
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
    }

    public void moveParent(){
            Transform cellTransform = gameObject.transform.parent; // parent is a Cell gameobject

            parent.transform.SetParent(cellTransform); // selected piece의 부모는 selected piece가 이동할 cell
            parent.transform.position = cellTransform.position; // 위치 조정
            parent.GetComponent<ChessPiece>().indexX = cellTransform.gameObject.GetComponent<cellController>().indexX;
            parent.GetComponent<ChessPiece>().indexY = cellTransform.gameObject.GetComponent<cellController>().indexY;      
    }
}
