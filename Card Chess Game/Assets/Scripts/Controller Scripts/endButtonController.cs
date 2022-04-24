using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;
public class endButtonController : MonoBehaviour
{

    // GameObject hands;
    GameObject hands1;
    GameObject hands2;
    // Start is called before the first frame update
    void Start()
    {
        hands1 = GameObject.Find("Hand_P1");
        hands2 = GameObject.Find("Hand_P2");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject hands; 
        if(GameManager.turn == 1) {
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
        GameManager player_data;

        if(GameManager.turn == 1) {
            player_data = GameManager.player1;
        }else {
            player_data = GameManager.player2;
        }
        Card card = player_data.drawCard();
        
        GameObject card_object = Config.Helper.cardToGameObject(card);

        card_object.transform.SetParent(hands.transform, true);
        card_object.GetComponent<dragDrop>().player = GameManager.turn;
        card_object.GetComponent<dragDrop>().init(GameManager.turn, player_data.hand.Count-1, card);
    }

    public static void switchTurn() {
        GameManager.turn = GameManager.turn == 1 ? 2 : 1; 
    }
}
