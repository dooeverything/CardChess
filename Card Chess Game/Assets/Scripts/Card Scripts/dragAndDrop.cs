using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class dragAndDrop : MonoBehaviour
{
    public int handPos; // 손에서 몇번째에 있는지 
    public int cardType; // 무슨 카드인지
    public bool canMove = true; // 드래그할수 있는지
    private float firstPosX;
    private float firstPosY;
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private GameObject trashCan;
    void Start() {

        cardSave.cardList[handPos] = cardType;
        firstPosX = this.transform.localPosition.x;
        firstPosY = this.transform.localPosition.y;
        trashCan = GameObject.Find("trashcan_closed");

    }
    void Update()
    {
        if(isBeingHeld == true) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }else {
            if(trashCan.GetComponent<BoxCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
                Debug.Log("Change Card");
                Destroy(gameObject);
                createNewCard();
            }
        }
        //onMouseUp();
    }

    private void createNewCard() {
        int index = Random.Range(0, 7); // 랜덤으로 새로 만든 카드
        Debug.Log("Create new Card " + index);
        Object prefab = AssetDatabase.LoadAssetAtPath(cardSave.pathMulligan[index], typeof(GameObject));
        GameObject card = Instantiate(prefab) as GameObject;
        dragAndDrop component = card.GetComponent<dragAndDrop>();
        card.transform.position = new Vector3( firstPosX, firstPosY, 0);
        component.canMove = false;
        component.cardType = index; 
        component.handPos = handPos; 
    }

    private void OnMouseDown() {
        if(!canMove) return; 
        if(Input.GetMouseButtonDown(0)) {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos); // convert screen mouse to world(game)

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
    }

    private void OnMouseUp() {
        
        isBeingHeld = false;
        gameObject.transform.localPosition = new Vector3(firstPosX, firstPosY, 0);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        
    }
}