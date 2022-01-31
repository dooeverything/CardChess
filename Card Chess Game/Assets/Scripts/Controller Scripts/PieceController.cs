using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;


public class PieceController : MonoBehaviour
{
    public bool selected = false;
    public ChessPiece piece = null;
    public int player = 1;
    Game_Manager player_data;

    // Start is called before the first frame update
    void Start()
    {
        if (player == 1)
        {
            player_data = Game_Manager.player1;
        }
        else
        {
            player_data = Game_Manager.player2;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //Set up the new Pointer Event
            var ped = new PointerEventData(null);
            //Set the Pointer Event Position to that of the mouse position
            ped.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(ped, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            // Result is a **dot_move(Clone)**
            foreach (RaycastResult result in results)
            {
                //Debug.Log(result.gameObject.name);
                //If the player clicked the dot_move to move a selected piece
                if (result.gameObject.name == "dot_move(Clone)")
                {
                    //Debug.Log(this.name);
                    if (this.transform.childCount > 0) // if a piece has selected indicator as a child gameobject
                    {
                        Destroy(this.transform.GetChild(0).gameObject); // Destory the selected indicator

                        this.transform.SetParent(result.gameObject.transform.parent); // Set parent to destination cell
                        //result.gameObject.transform.parent.GetComponent<Image>().color = Color.white;
                        this.transform.position = result.gameObject.transform.position; // Set position to destination cell
                        piece.indexX = result.gameObject.transform.parent.GetComponent<cellController>().indexX; // Set indexX as cell X
                        piece.indexY = result.gameObject.transform.parent.GetComponent<cellController>().indexY; // Set indexY as CellY
                        //Debug.Log("Dot list has " + Game_Manager.dots.Count);

                        foreach (GameObject obj in player_data.dots)
                        {
                            Destroy(obj);  // Destory all dots
                        }

                        // Destroy Selected Card
                        foreach (GameObject obj in player_data.cards_in_hand)
                        {
                            if (obj.GetInstanceID() == player_data.selected_card.GetInstanceID())
                            {
                                player_data.cards_in_hand.Remove(obj);
                                Destroy(obj);
                                player_data.selected_card = null;
                                break;
                            }
                        }
                        Debug.Log("DragDrop selectedPiece is " + dragDrop.selectedPiece);
                    }
                }else if(result.gameObject.name == "Attacking(Clone)") {
                    if (this.transform.childCount > 0) {
                        Destroy(this.transform.GetChild(0).gameObject); // Destory the selected indicator
                        
                        Debug.Log("Attacked a piece");
                        int x = result.gameObject.GetComponent<strikeController>().indexX;
                        int y = result.gameObject.GetComponent<strikeController>().indexY;
                        Destroy(cardSave.cells[x, y].transform.GetChild(0).gameObject);
                        
                        Debug.Log("An attacked piece is now destoryed");

                        foreach(GameObject obj in player_data.strike) {
                            Destroy(obj);
                        }

                        foreach (GameObject obj in player_data.dots)
                        {
                            Destroy(obj);  // Destory all dots
                        }

                        //this.transform.SetParent(result.gameObject.transform.parent); // Set parent to destination cell
                        //result.gameObject.transform.parent.GetComponent<Image>().color = Color.white;
                        //this.transform.position = result.gameObject.transform.position; // Set position to destination cell
                        //piece.indexX = result.gameObject.transform.parent.GetComponent<cellController>().indexX; // Set indexX as cell X
                        //piece.indexY = result.gameObject.transform.parent.GetComponent<cellController>().indexY; // Set indexY as CellY

                        // Destroy Selected Card
                        foreach (GameObject obj in player_data.cards_in_hand)
                        {
                            if (obj.GetInstanceID() == player_data.selected_card.GetInstanceID())
                            {
                                player_data.cards_in_hand.Remove(obj);
                                Destroy(obj);
                                player_data.selected_card = null;
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void createIndicator()
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

    public void destroyIndicator()
    {
        if (this.transform.childCount != 0)
        {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    public void createDot(ChessPiece piece, string behaviour)
    {
        //GameObject dot = Instantiate(prefab) as GameObject;
        this.piece = piece;
        if (behaviour == "move")
        {
            piece.createDotMove();
        }
        else
        {
            piece.createDotStrike();
        }
    }
}