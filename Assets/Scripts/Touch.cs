﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {   //Shoot ray, and store the original position where the finger started
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Stamp")))
        {
            var selectedStampName = hit.transform.gameObject.name;
            var touchMessage = "You touched " + selectedStampName;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
