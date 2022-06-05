using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;

public class Warrior : MonoBehaviour 
{
    public GameObject createStrike(GameObject cell, GameObject enemy, GameObject card, GameObject parent) 
    {
        GameObject attacking = Helper.prefabNameToGameObject(Prefab.Attacking.ToString());
        attacking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        attacking.transform.position = cell.transform.position;
        attacking.GetComponent<StrikeController>().enemy = enemy;
        attacking.GetComponent<StrikeController>().card = card;
        attacking.GetComponent<StrikeController>().parent = parent;

        return attacking;
    }
}
