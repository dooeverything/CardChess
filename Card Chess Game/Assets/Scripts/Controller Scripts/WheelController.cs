using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WheelController : MonoBehaviour
{
    float rotSpeed;
    int num = 0;
    bool clicked = false;
    void Start() {

    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && num == 0) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null) {
                Debug.Log(hit.collider.gameObject.name);
                if(hit.transform.name == "wheel") {;
                    rotSpeed = Random.Range(30.0f, 50.0f);
                    Debug.Log(rotSpeed);
                    num++;
                    clicked = true;
                }
            }
        }
        transform.Rotate(0, 0, this.rotSpeed);
        this.rotSpeed *= Random.Range(0.9f, 0.97f);
    }

    public int result() {
        Debug.Log(transform.eulerAngles.z);
        if(transform.eulerAngles.z >= 0 && transform.eulerAngles.z <= 180) {
            Debug.Log("Head");
            return 0;
        }else {
            Debug.Log("Tail");
            return 1;
        }
    }

    public bool finish() {
        if(rotSpeed <= 0.07f && clicked) {
            Debug.Log("Finished!");
            rotSpeed = 0;
            return true;
        }
        return false;
    }
}
