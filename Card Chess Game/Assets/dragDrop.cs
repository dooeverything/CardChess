using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class dragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    GameObject canvas;
    GameObject hands;
    public static bool beingHeld;
    public GameObject horsePiece1;
    public GameObject horsePiece2;
    GameObject indicator;
    void Start() {
        canvas = GameObject.Find("Canvas");
        hands = GameObject.Find("Hands");
        indicator = GameObject.Find("selectedIndicator");
        horsePiece1 = GameObject.Find("test piece");
        horsePiece2 = GameObject.Find("test piece_2");
    }
    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        transform.SetParent(canvas.transform);
        beingHeld = true;
    }

    public void OnDrag(PointerEventData eventData) {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        if(horsePiece1.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
            //when card is dropped to horse piece
            Debug.Log("Card will be destroyed and horsepiece1 will move");
            indicator.transform.position = horsePiece1.transform.position;
            indicator.transform.SetParent(horsePiece1.transform);
            horsePiece1.transform.position = new Vector2(horsePiece1.transform.position.x, horsePiece1.transform.position.y+160);
            Destroy(gameObject);
        }else if(horsePiece2.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {
            Debug.Log("Card will be destroyed and horsepiece2 will move");
            indicator.transform.position = horsePiece2.transform.position;
            indicator.transform.SetParent(horsePiece2.transform);
            Destroy(gameObject);
        }

        transform.SetParent(hands.transform);
        beingHeld = false;
    }
}

// create selected indicator -->
            // selected = Resources.Load<Sprite>("Assets/Image/selected picture.png");
            // selectedObject.AddComponent(typeof(Image));
            // selectedObject.GetComponent<Image>().sprite = selected;
            // selectedObject.transform.position = gameObject.transform.position;
            // selectedObject.transform.localScale = new Vector2(0.3f, 0.3f);            
