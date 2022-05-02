using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using Config;

public class endButtonController : MonoBehaviour
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.endTurn();
    }
}
