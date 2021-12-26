using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool beingHeld;
    private GameObject pieces;
    public static string pieceName;
    public static string cardName;

    public string pieceType;
    public string behaviour;
    Transform hand;

    private List<GameObject> temp = null;
    private List<ChessPiece> temp2 = null;
    void Start()
    {
        cardName = this.gameObject.name;
        hand = this.transform.parent;

        // Get information about a card
        switch (pieceType)
        {
            case "archer":
                if (behaviour == "move")
                {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.archerOnBoard_player1;
                    temp2 = Game_Manager.archerConstructors_player1;
                }
                break;
            case "warrior":
                if (behaviour == "move")
                {
                    temp = Game_Manager.warriorOnBoard_player1;
                    temp2 = Game_Manager.warriorConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.warriorOnBoard_player1;
                    temp2 = Game_Manager.warriorConstructors_player1;
                }
                break;
            case "mage":
                if (behaviour == "move")
                {
                    temp = Game_Manager.mageOnBoard_player1;
                    temp2 = Game_Manager.mageConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.mageOnBoard_player1;
                    temp2 = Game_Manager.mageConstructors_player1;
                }
                break;
            case "king":
                if (behaviour == "move")
                {
                    temp = Game_Manager.kingOnBoard_player1;
                    temp2 = Game_Manager.kingConstructors_player1;
                }
                else
                {
                    temp = Game_Manager.kingOnBoard_player1;
                    temp2 = Game_Manager.kingConstructors_player1;
                }
                break;
            default: return;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Game_Manager.selected_card = null; 
        for (int i = 0; i < temp.Count; i++)
        {
            temp[i].GetComponent<PieceController>().selected = false; 
        }
        foreach (GameObject obj in Game_Manager.cards_in_hand)
        {
            Color color = obj.GetComponent<Image>().color;
            color.a = 1;
            obj.GetComponent<Image>().color = color;
        }

        foreach (GameObject obj in Game_Manager.dots)
        {
            Destroy(obj); 
        }

        Debug.Log("count before: " + Game_Manager.indicator.Count);

        foreach (GameObject obj in Game_Manager.indicator)
        {
            // Debug.Log("obj is: " + obj);
            Debug.Log("id is: " + obj.GetInstanceID()); 
            Destroy(obj); 
        }

        Debug.Log("count after: " + Game_Manager.indicator.Count);

        Debug.Log("OnBeginDrag");
        transform.SetParent(this.transform.root);
        beingHeld = true;
        switch (pieceType)
        {
            case "archer":
                if (behaviour == "move")
                {
                    List<GameObject> temp = Game_Manager.archerOnBoard_player1;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        temp[i].GetComponent<PieceController>().createIndicator();
                        
                    }
                }
                else
                {
                    List<GameObject> temp = Game_Manager.archerOnBoard_player1;
                    for (int i = 0; i < temp.Count; i++)
                    {
                        temp[i].GetComponent<PieceController>().createIndicator();
                    }
                }
                break;
            default: return;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
    public static bool selected = false;
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        // List<GameObject> temp = null;
        // List<ChessPiece> temp2 = null;

        // // Get information about a card
        // switch (pieceType)
        // {
        //     case "archer":
        //         if (behaviour == "move")
        //         {
        //             temp = Game_Manager.archerOnBoard_player1;
        //             temp2 = Game_Manager.archerConstructors_player1;
        //         }
        //         else
        //         {
        //             temp = Game_Manager.archerOnBoard_player1;
        //             temp2 = Game_Manager.archerConstructors_player1;
        //         }
        //         break;
        //     case "warrior":
        //         if (behaviour == "move")
        //         {
        //             temp = Game_Manager.warriorOnBoard_player1;
        //             temp2 = Game_Manager.warriorConstructors_player1;
        //         }
        //         else
        //         {
        //             temp = Game_Manager.warriorOnBoard_player1;
        //             temp2 = Game_Manager.warriorConstructors_player1;
        //         }
        //         break;
        //     case "mage":
        //         if (behaviour == "move")
        //         {
        //             temp = Game_Manager.mageOnBoard_player1;
        //             temp2 = Game_Manager.mageConstructors_player1;
        //         }
        //         else
        //         {
        //             temp = Game_Manager.mageOnBoard_player1;
        //             temp2 = Game_Manager.mageConstructors_player1;
        //         }
        //         break;
        //     case "king":
        //         if (behaviour == "move")
        //         {
        //             temp = Game_Manager.kingOnBoard_player1;
        //             temp2 = Game_Manager.kingConstructors_player1;
        //         }
        //         else
        //         {
        //             temp = Game_Manager.kingOnBoard_player1;
        //             temp2 = Game_Manager.kingConstructors_player1;
        //         }
        //         break;
        //     default: return;
        // }

        // Create a dot
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].GetComponent<CircleCollider2D>().IsTouching(this.gameObject.GetComponent<BoxCollider2D>()))
            {
                temp[i].GetComponent<PieceController>().selected = true;
                temp[i].GetComponent<PieceController>().createDot(temp2[i], behaviour);
                Color color = gameObject.GetComponent<Image>().color;
                color.a = 0.5f;
                gameObject.GetComponent<Image>().color = color;
                Game_Manager.selected_card = gameObject; 
            }
        }

        // Destroy Indicator
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i].GetComponent<PieceController>().selected == true)
            {
                continue;
            }
            temp[i].GetComponent<PieceController>().destroyIndicator();
        }
        transform.SetParent(hand);
        beingHeld = false;

        /*foreach(Transform child in pieces.transform) {
        if(child.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log(child.gameObject.name + " will move or attack");
                pieceName = child.gameObject.name;
                selected = true;
                obj_id = child.GetInstanceID();
                Destroy(gameObject);
            }
        }*/
    }
}