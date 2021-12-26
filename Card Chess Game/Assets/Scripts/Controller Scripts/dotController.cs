using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dotController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //indexX = this.transform.parent.GetComponent<>
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void destoryDot() {
        Destroy(this.gameObject);
    }
}
