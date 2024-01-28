using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Potion : MonoBehaviour
{
    public float value;
    public ClientRequest potionContents;
    public void SetPotion(Potion p)
    {
        value = p.value;
        potionContents = p.potionContents;
    }
    void Awake()
    {
        potionContents = new ClientRequest();
        value = 0;
    }
    void Update()
    {
        Mouse mouse = Mouse.current;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == gameObject)
                {
                }
            }
        }
    }
}
