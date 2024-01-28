using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHelper
{
    public static bool wasClickedThisFrame(GameObject toCheck)
    {
        Mouse mouse = Mouse.current;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == toCheck)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public static bool isBeingHoveredThisFrame(GameObject toCheck)
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePosition = mouse.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        var raycastHits = Physics.RaycastAll(ray);
        foreach(var rhit in raycastHits)
        {
            if(rhit.collider.gameObject == toCheck)
            {
                return true;
            }
        }
        return false;
    }
}
