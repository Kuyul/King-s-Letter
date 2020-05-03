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
            var selectedStampName = hit.transform.gameObject.name;
            var touchMessage = "You touched " + selectedStampName;

            GameObject levelControl = GameObject.Find("LevelControl");
            LevelControl levelControlScript = levelControl.GetComponent<LevelControl>();
            var levelStamp = levelControlScript.getLevelStamp();

            if (levelStamp.name.Equals(selectedStampName))
            {
                GameControl.instance.showText.text = "CORRECT! " + touchMessage;
            } else
            {
                GameControl.instance.showText.text = "WRONG! " + touchMessage;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }
}
