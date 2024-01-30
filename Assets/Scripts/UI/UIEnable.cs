using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEnable : MonoBehaviour, IPointerClickHandler
{
    public GameObject targetUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        targetUI.SetActive(true);
    }
}
