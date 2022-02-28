using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ChessPiece : MonoBehaviour, IPointerDownHandler
{
    //GameObject parent; // Cell GameObject
    public int indexX;
    public int indexY;
    public cardSave.Piece chessPieceType;
    protected GameObject indicator;
    protected GameObject moveIndicator;
    public int player;
    protected Game_Manager player_data;
    public List<GameObject> indicators = null;
    public bool select = false; // if the card selects the piece, it gives true, otherwise false.
    void Start()
    {
        if(player == 1) {
            player_data = Game_Manager.player1;
        }else {
            player_data = Game_Manager.player2;
        }
    }

    public bool clicked = false;
    public void OnPointerDown(PointerEventData eventData) {
        
        // First need to check player's turn
        if(player == Game_Manager.turn && select == false){
            // If the piece is clicked again //
            if(gameObject == eventData.lastPress ) {
                if(clicked) {
                    Debug.Log("You clicked twice!");
                    if(Game_Manager.indicators != null) {
                        foreach(GameObject obj in Game_Manager.indicators) {
                            Destroy(obj);
                        }
                        Debug.Log("All indicators/dots is now removed!" + Game_Manager.indicators.Count);
                    }
                    clicked = false;
                    return;
                }
            }else {
                // If the another piece is click??
                Debug.Log("Another Piece is now clicked!");
                clicked = false;
                if(Game_Manager.indicators != null) {
                    foreach(GameObject obj in Game_Manager.indicators) {
                        Destroy(obj);
                    }
                    Debug.Log("All indicators/dots is now removed!" + Game_Manager.indicators.Count);
                }
            }

            clicked = true;

            Debug.Log( this.gameObject.name + " is now selected and ready to move or attack");

            switch (chessPieceType)
            {
                case cardSave.Piece.Archer:
                    indicators = GetComponent<Archer>().createIndicator();
                    break;

                case cardSave.Piece.Warrior:
                    indicators = GetComponent<Warrior>().createIndicator();
                    break;

                case cardSave.Piece.Mage:
                    indicators = GetComponent<Mage>().createIndicator();
                    break;

                case cardSave.Piece.King:
                    indicators = GetComponent<King>().createIndicator();
                    break;

                default:
                    break;
            }

            indicators.Add(selected()); // Create a selected indicator on piece and add it to the list of all indicators//

            Game_Manager.selected_piece = gameObject;
            Game_Manager.indicators = indicators;
        }else if(select == true) {
            
            
            Destroy(player_data.selected_card);
            
            endButtonController.switchTurn();

        }
    }

    public GameObject selected() {
        Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject selected_indicator = Instantiate(prefab) as GameObject;
        //player_data.indicators.Add(indicator);
        //Debug.Log("indicator is: " + indicator.transform.position.x); 
        selected_indicator.transform.SetParent(this.gameObject.transform);
        selected_indicator.transform.position = transform.position;
        return selected_indicator;
    } 

    public void destroyIndicator()
    {
        if (this.transform.childCount != 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    public void createIndicatorForCard()
    {
        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject indicator = Instantiate(selected) as GameObject;
        player_data.indicator.Add(indicator);
        //Debug.Log("indicator is: " + indicator.transform.position.x); 
        indicator.transform.SetParent(this.gameObject.transform);
        indicator.transform.position = transform.position;
        // Destroy(indicator); 
        //Debug.Log("id is: " + indicator.GetInstanceID()); 
    }
}
