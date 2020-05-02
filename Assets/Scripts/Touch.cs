using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Declare public variables
    public void Start()
    {
        
    }

    public void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {   //Shoot ray, and store the original position where the finger started
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Stamp")))
        {
            var message = "Touched " + hit.transform.gameObject.name;
            Debug.Log(message);
            GameControl.instance.showText.text = message;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
