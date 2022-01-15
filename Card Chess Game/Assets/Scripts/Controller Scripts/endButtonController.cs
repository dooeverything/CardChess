using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class endButtonController : MonoBehaviour
{
    GameObject hands;

    // Start is called before the first frame update
    void Start()
    {
        hands = GameObject.Find("Hands");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            // Result is a **dot_move(Clone)**
            foreach (RaycastResult result in results) {
                //Debug.Log(result.gameObject.name);
                if (result.gameObject.name == "End") {
                    result.gameObject.transform.GetChild(0).GetComponent<timerController>().timer = 30;
                    int randomIndex = Random.Range(0, Game_Manager.myDeckCount);
                    int randomCard = Game_Manager.deck[randomIndex];
                    Game_Manager.deck.RemoveAt(randomIndex);
                    Game_Manager.myDeckCount--;
                    Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.test[randomCard], typeof(GameObject));
                    GameObject card = Instantiate(prefab) as GameObject;
                    card.transform.SetParent(hands.transform, true); 
                    card.GetComponent<dragDrop>().pieceType = cardSave.test2[randomCard, 0];
                    card.GetComponent<dragDrop>().behaviour = cardSave.test2[randomCard, 1];
                    Game_Manager.cards_in_hand.Add(card);
                }
                
            }
        }
    }
}
