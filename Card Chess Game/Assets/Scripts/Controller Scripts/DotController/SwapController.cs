using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwapController : DotController, IPointerDownHandler
{
    public GameObject mage;
    public GameObject target;

    public void OnPointerDown(PointerEventData eventData)
    {
        Transform temp = mage.transform.parent;

        mage.transform.SetParent(target.transform.parent, false);
        target.transform.SetParent(temp, false);
        
        GameManager.lastDotClicked(true);
    }
}
