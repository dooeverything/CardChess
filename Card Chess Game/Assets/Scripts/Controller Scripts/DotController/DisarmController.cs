using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Config;
public class DisarmController : DotController, IPointerDownHandler
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        target.GetComponent<ChessPiece>().offense_power = 1;
        target.GetComponent<ChessPiece>().defense_power = 1;

        if(target.GetComponent<ChessPiece>().piece_type == Piece.Archer) {

            target.GetComponent<Archer>().attack_range = 4;
        }
        

        GameManager.lastDotClicked(true);
    }
}
