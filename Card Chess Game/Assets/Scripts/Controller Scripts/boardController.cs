using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

// Create every pieces on the chess board
public class boardController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // Get Path of each piece
        GameObject pieces = GameObject.Find("Chess Piece");
        Object prefabArcher = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.Piece.Archer], typeof(GameObject));
        Object prefabMage = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.Piece.Mage], typeof(GameObject));
        Object prefabWarrior = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.Piece.Warrior], typeof(GameObject));
        Object prefabKing = AssetDatabase.LoadAssetAtPath(cardSave.piece[(int) cardSave.Piece.King], typeof(GameObject));
        

        // Create 2 Archers
        GameObject archer_1 = Instantiate(prefabArcher) as GameObject;
        archer_1.transform.position = new Vector2(cardSave.positionBoard[0], cardSave.positionBoard[1]);
        archer_1.transform.SetParent(pieces.transform, false);
        GameObject archer_2 = Instantiate(prefabArcher) as GameObject;
        archer_2.transform.position = new Vector2(cardSave.positionBoard[0] + cardSave.positionBoard[2] * 4, cardSave.positionBoard[1]);
        archer_2.transform.SetParent(pieces.transform, false);

        // Create 2 Mages
        GameObject mage_1 = Instantiate(prefabMage) as GameObject;
        mage_1.transform.position = new Vector2(cardSave.positionBoard[0] + cardSave.positionBoard[2], cardSave.positionBoard[1]);
        mage_1.transform.SetParent(pieces.transform, false);
        GameObject mage_2 = Instantiate(prefabMage) as GameObject;
        mage_2.transform.position = new Vector2(cardSave.positionBoard[0] + cardSave.positionBoard[2] * 3, cardSave.positionBoard[1]);
        mage_2.transform.SetParent(pieces.transform, false);

        // Create 5 warrior pieces
        for(int i=0; i<5; i++) {
            GameObject warrior = Instantiate(prefabWarrior) as GameObject;
            warrior.transform.position = new Vector2(cardSave.positionBoard[0] + cardSave.positionBoard[2] * i, cardSave.positionBoard[1] + cardSave.positionBoard[2]);
            warrior.transform.SetParent(pieces.transform, false);
        }


        // Create a king piece
        GameObject king = Instantiate(prefabKing) as GameObject;
        king.transform.position = new Vector2(cardSave.positionBoard[0] + cardSave.positionBoard[2] * 2, cardSave.positionBoard[1]);
        king.transform.SetParent(pieces.transform, false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
