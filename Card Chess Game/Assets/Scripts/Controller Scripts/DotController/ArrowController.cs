using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowController : DotController, IPointerDownHandler
{
    public GameObject enemy;
    public bool moveWhenAttack = true;
    public static int numAttack = default;

    public void OnPointerDown(PointerEventData eventData) {
        deleteCard();

        {
            ChessPiece piece = enemy.GetComponent<ChessPiece>();
            int hp_enemy = piece.defense_power;
            int st_self = parent.GetComponent<ChessPiece>().offense_power;
            piece.defense_power = hp_enemy - st_self;

            if(piece.defense_power <= 0) {
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
                Destroy(gameObject);
                Destroy(enemy); 
            }
        }

        if(parent.GetComponent<ChessPiece>().offense_power > 1) {
            parent.GetComponent<ChessPiece>().offense_power--;
        }

        if(numAttack == 0) {
            GameManager.destroyAllIndicators();
            GameManager.destroyAlldots();
            // Switch turn after strike
            GameManager.endTurn();
        }
        
    }

}
