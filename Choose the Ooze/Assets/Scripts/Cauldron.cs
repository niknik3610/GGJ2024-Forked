using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();
    public MouseFollower mouseFollower;
    private Camera m_Camera;
    void Awake()
    {
        m_Camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;
        
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = m_Camera.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == gameObject)
                {
                    Debug.Log("Clicked");
                    if(mouseFollower.ingredientBeingCarried != null)
                    {
                        ingredients.Add(mouseFollower.ingredientBeingCarried);
                        mouseFollower.ingredientBeingCarried.gameObject.transform.SetParent(null);
                        mouseFollower.ingredientBeingCarried.gameObject.SetActive(false);
                        mouseFollower.ingredientBeingCarried = null;
                    }
                }
            }
        }
    }
}
