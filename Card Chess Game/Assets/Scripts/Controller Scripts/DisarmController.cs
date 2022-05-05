using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Config;
public class DisarmController : MonoBehaviour, IPointerDownHandler
{
    public GameObject target;
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        target.GetComponent<ChessPiece>().offensePower = 1;
        target.GetComponent<ChessPiece>().defensePower = 1;

        if(target.GetComponent<ChessPiece>().chessPieceType == Piece.Archer) {

            target.GetComponent<Archer>().attackRange = 4;
        }
        

        GameManager.lastDotClicked(true);
    }
}
