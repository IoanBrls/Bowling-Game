using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonselected;

   

    void Update ()
    {
        Cursor.visible=true;
        Cursor.lockState = CursorLockMode.None;
        
        if (Input.GetAxisRaw("Vertical") != 0 && buttonselected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonselected = true;
        }
	}

    private void OnDisable()
    {
        buttonselected = false;
    }
}
