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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
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
                if (result.gameObject.name == "dot_move(Clone)")
                {
                    Debug.Log(this.name);
                    if(this.transform.childCount > 0) {
                        Destroy(this.transform.GetChild(0).gameObject);
                        
                        this.transform.SetParent(result.gameObject.transform.parent);
                        //result.gameObject.transform.parent.GetComponent<Image>().color = Color.white;
                        this.transform.position = result.gameObject.transform.position;
                        piece.indexX = result.gameObject.transform.parent.GetComponent<cellController>().indexX;
                        piece.indexY = result.gameObject.transform.parent.GetComponent<cellController>().indexY;
                        Debug.Log("Dot list has " + Game_Manager.dots.Count);

                        foreach (GameObject obj in Game_Manager.dots)
                        {
                            Destroy(obj); 
                        }
                        // for(int i=0; i<Game_Manager.dots.Count; i++) {
                        //     Game_Manager.dots[i].GetComponent<dotController>().destoryDot();
                        //     Debug.Log((i+1) + " th dot is removed!");
                        // }
                        // Game_Manager.dots.Clear();
                        //Destroy(result.gameObject);
                        foreach (GameObject obj in Game_Manager.cards_in_hand)
                        {
                            if(obj.GetInstanceID() == Game_Manager.selected_card.GetInstanceID()) {
                                Game_Manager.cards_in_hand.Remove(obj);
                                Destroy(obj); 
                                Game_Manager.selected_card = null; 
                                break; 
                            }
                        }                        
                    }
                    //  = result.gameObject.transform.position;

                    // foreach (Transform child in moveIndicator.transform)
                    // {
                    //     GameObject.Destroy(child.gameObject);
                    // }
                }
            }            
        }
    }

    public void createIndicator() {
        Object selected = AssetDatabase.LoadAssetAtPath("Assets/Prefab/selectedIndicator.prefab", typeof(GameObject));
        GameObject indicator = Instantiate(selected) as GameObject;
        Game_Manager.indicator.Add(indicator); 
        Debug.Log("indicator is: " + indicator.transform.position.x); 
        indicator.transform.SetParent(this.gameObject.transform);
        indicator.transform.position = transform.position;
        // Destroy(indicator); 
        Debug.Log("id is: " + indicator.GetInstanceID()); 
    }

    public void destroyIndicator() {
        if(this.transform.childCount != 0) {
            Destroy(this.transform.GetChild(0).gameObject);
        }
    }

    public void createDot(ChessPiece piece, string behaviour) {
        //GameObject dot = Instantiate(prefab) as GameObject;
        this.piece = piece;
        if(behaviour == "move") {
            piece.createDotMove();
        } else {
            piece.createDotStrike();
        }
    }
}
