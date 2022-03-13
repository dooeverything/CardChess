using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class Archer : MonoBehaviour {
    
    public GameObject createStrike(GameObject cell, GameObject enemy, GameObject card) {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/Attacking.prefab", typeof(GameObject)); // Create Prefab
        GameObject striking = GameObject.Instantiate(prefab) as GameObject; // Instantiate on Canvas
        striking.transform.SetParent(cell.transform, false); // Parent is Cell GameObject
        striking.transform.position = cell.transform.position;
        striking.GetComponent<strikeController>().enemy = enemy;
        striking.GetComponent<strikeController>().card = card;
        striking.GetComponent<strikeController>().moveWhenAttack = false;

        return striking;
    }
}
