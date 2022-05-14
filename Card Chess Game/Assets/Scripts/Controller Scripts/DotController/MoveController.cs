using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveController :  DotController, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        moveParent();
        GameManager.destroyAlldots();
        GameManager.destroyAllIndicators();
        deleteCard();
        GameManager.endTurn();
    }
}
