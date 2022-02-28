using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dotController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void destoryDot() {
        Destroy(this.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData) {

        Transform parent = gameObject.transform.parent; // parent is a Cell gameobject

        Game_Manager.selected_piece.transform.SetParent(parent); // selected piece의 부모는 selected piece가 이동할 cell
        Game_Manager.selected_piece.transform.position = parent.position; // 위치 조정
        Game_Manager.selected_piece.GetComponent<ChessPiece>().indexX = parent.GetComponent<cellController>().indexX;
        Game_Manager.selected_piece.GetComponent<ChessPiece>().indexY = parent.GetComponent<cellController>().indexY;

        Game_Manager.selected_piece = null;
        

        foreach(GameObject obj in Game_Manager.indicators) {
            Destroy(obj);
        }

        // Switch turn after the piece move
        endButtonController.switchTurn();
    }
}
