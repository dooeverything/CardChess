using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Config;
using UnityEngine.UI;

public class DemolitionController : DotController, MonoBehaviour
{
    public GameObject target;

    public void OnPointerDown(PointerEventData eventData)
    {
        Piece type = target.GetComponent<ChessPiece>().piece_type;
        
        switch(type){
            case Piece.Mage:
                // Change Mage to Archer
                Debug.Log("Change from Mage to Archer");
                Sprite archer = Helper.generateSprite(Path.Prefab.archer.ToString());
                target.GetComponent<Image>().sprite = archer;
                Destroy(target.GetComponent<Mage>());
                target.AddComponent<Archer>();
                break;
            case Piece.Archer:
                Debug.Log("Change from Archer to Warrior");
                Sprite warrior = Helper.generateSprite(Path.Prefab.warrior.ToString());
                target.GetComponent<Image>().sprite = warrior;
                Destroy(target.GetComponent<Archer>());
                target.AddComponent<Warrior>();
                break;
            default:
                break;
        }
    }
}
