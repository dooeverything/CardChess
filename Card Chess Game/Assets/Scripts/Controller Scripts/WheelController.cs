using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class WheelController : MonoBehaviour, IPointerDownHandler
{
    float rotSpeed;
    bool clicked = false;
    void Start() {

    }
    void Update()
    {
        transform.Rotate(0, 0, this.rotSpeed);
        this.rotSpeed *= Random.Range(0.97f, 0.98f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!clicked)
            rotSpeed = Random.Range(30.0f, 50.0f);
        clicked = true;
    }

    public int result() {
        if(transform.eulerAngles.z <= 180) {
            return 0;
        }else {
            return 1;
        }
    }

    public bool finish() {
        if(rotSpeed <= 0.07f && clicked) {
            rotSpeed = 0;
            return true;
        }
        return false;
    }
}
