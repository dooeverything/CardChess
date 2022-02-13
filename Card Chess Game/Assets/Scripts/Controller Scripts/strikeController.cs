using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class strikeController : MonoBehaviour, IPointerDownHandler
{
    public int indexX;
    public int indexY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData) {

        Debug.Log("Strike!!");

        Transform cell = cardSave.cells[indexX, indexY].transform;
        
        GameObject attacked_piece = cell.GetChild(0).gameObject;
        Destroy(attacked_piece);

        Game_Manager.selected_piece.transform.SetParent(cell.transform);
        Game_Manager.selected_piece.transform.position = cell.position;
        Game_Manager.selected_piece.GetComponent<ChessPiece>().indexX = indexX;
        Game_Manager.selected_piece.GetComponent<ChessPiece>().indexY = indexY;

        Game_Manager.selected_piece = null;
        
        foreach(GameObject obj in Game_Manager.indicators) {
            Destroy(obj);
        }

    }

}
