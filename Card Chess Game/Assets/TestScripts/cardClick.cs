using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class cardClick : MonoBehaviour
{
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                if(hit.point.x < transform.position.x-0.5) {
                    transform.position = new Vector3(transform.position.x - 1,transform.position.y, 0);
                }else if(hit.point.x > transform.position.x+0.5){
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, 0);
                }else if(hit.point.y > transform.position.y+0.5) {
                    transform.position = new Vector3(transform.position.x, transform.position.y+1, 0);
                }else if(hit.point.y < transform.position.y-0.5) {
                    transform.position = new Vector3(transform.position.x, transform.position.y-1,0);
                }
            }
        }
    }
}
