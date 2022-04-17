using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class thunderBoltStrikeController : MonoBehaviour, IPointerDownHandler
{
    public GameObject enemy;
    public GameObject card;
    public GameObject parent; 
    public int enemy_player;

    // Start is called before the first frame update
    void Start()
    {
        enemy = gameObject.transform.parent.GetChild(0).gameObject;
        enemy_player = enemy.GetComponent<ChessPiece>().player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) {

        deleteCard();

        {
            for(int i=0; i<3; i++) {
                List<GameObject> next_targets = new List<GameObject>(); 
                for(int j=0; j<8; j++) {
                    int newX_strike = enemy.GetComponent<ChessPiece>().indexX + (CardEffect.move_list[j, 0]);
                    int newY_strike = enemy.GetComponent<ChessPiece>().indexY + (CardEffect.move_list[j, 1]);

                    // Check the location is out of bound 
                    if(newX_strike > 4 || newX_strike < 0) {
                        continue;
                    }
                    if(newY_strike > 7 || newY_strike < 0 ) {
                        continue;
                    }
                    
                    GameObject newCell_Strike = CardSave.cells[newX_strike, newY_strike ];
                    if(newCell_Strike.transform.childCount > 0) {
                        if(newCell_Strike.transform.GetChild(0) == null) continue; 
                        GameObject enemyPiece = newCell_Strike.transform.GetChild(0).gameObject;
                        if(enemyPiece.GetComponent<ChessPiece>() && enemyPiece.GetComponent<ChessPiece>().player == enemy_player) {
                            next_targets.Add(enemyPiece); 
                        }
                    }
                }

                ChessPiece piece = enemy.GetComponent<ChessPiece>();
                int hp_enemy = piece.defensePower;
                int st_self = parent.GetComponent<ChessPiece>().offensePower;
                piece.defensePower = hp_enemy - st_self;
                if(piece.defensePower <= 0) {
                    List<GameObject> list = piece.player_data.piecesOnBoard;
                    int index = -1;
                    for (int j=0; j<list.Count; j++) {
                        if(enemy == list[j]){
                            index = j;
                            break;
                        }
                    }
                    list.RemoveAt(index);
                    DestroyImmediate(enemy); 
                }
                if(next_targets.Count == 0) break; 

                int randomIndex = Random.Range(0, next_targets.Count);
                enemy = next_targets[randomIndex];
                

            }


        }


        GameManager.destroyAllIndicators(); 
        GameManager.destroyAlldots(); 
        // Switch turn after strike
        endButtonController.switchTurn();
    }

    public void deleteCard()
    {
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

}
