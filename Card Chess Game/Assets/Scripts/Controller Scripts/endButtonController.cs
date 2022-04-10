using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class endButtonController : MonoBehaviour
{

    // GameObject hands;
    GameObject hands1;
    GameObject hands2;
    // Start is called before the first frame update
    void Start()
    {
        hands1 = GameObject.Find("Hands");
        hands2 = GameObject.Find("Hands_Opponent");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hands; 
        if(Game_Manager.turn == 1) {
            hands = hands1;
        }else {
            hands = hands2; 
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            // Result is a **End**
            foreach (RaycastResult result in results) {
                if (result.gameObject.name == "End") { // result --> timer
                    result.gameObject.transform.GetChild(0).GetComponent<timerController>().timer = 30;
                    
                    // Draw a card before switch turn
                    drawCard(hands);
                    
                    // After a player draw one's card, switch turn
                    switchTurn();
                }
                
            }
        }
    }

    public static void drawCard(GameObject hands) {
        Game_Manager player_data;

        if(Game_Manager.turn == 1) {
            player_data = Game_Manager.player1;
        }else {
            player_data = Game_Manager.player2;
        }
        int randomIndex = Random.Range(0, player_data.myDeckCount);
        int randomCard = player_data.deck[randomIndex];
        player_data.deck.RemoveAt(randomIndex);
        player_data.myDeckCount--;
        Object prefab = AssetDatabase.LoadAssetAtPath(CardSave.test[randomCard], typeof(GameObject));
        GameObject card = Instantiate(prefab) as GameObject;
        card.transform.SetParent(hands.transform, true);
        card.GetComponent<dragDrop>().player = Game_Manager.turn;
        player_data.cards_in_hand.Add(card);
    }

    public static void switchTurn() {
        Game_Manager.turn = Game_Manager.turn == 1 ? 2 : 1; 
    }
}
