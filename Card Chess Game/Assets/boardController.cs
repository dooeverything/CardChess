using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class boardController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject pieces = GameObject.Find("Chess Piece");
        Object prefabArcher = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.Piece.Archer], typeof(GameObject));
        GameObject archer_1 = Instantiate(prefabArcher) as GameObject;
        archer_1.transform.position = new Vector2(cardSave.positionBoard[0], cardSave.positionBoard[1]);
        archer_1.transform.SetParent(pieces.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
