using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dotController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public GameObject parent; 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData) {
        Transform cellTransform = gameObject.transform.parent; // parent is a Cell gameobject

        parent.transform.SetParent(cellTransform); // selected piece의 부모는 selected piece가 이동할 cell
        parent.transform.position = cellTransform.position; // 위치 조정
        parent.GetComponent<ChessPiece>().indexX = cellTransform.gameObject.GetComponent<cellController>().indexX;
        parent.GetComponent<ChessPiece>().indexY = cellTransform.gameObject.GetComponent<cellController>().indexY;        

        // Switch turn after the piece move
        endButtonController.switchTurn();

        Game_Manager.destroyAlldots(); 
        Game_Manager.destroyAllIndicators(); 

    }
}
