using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    private MouseFollower _mouseFollower;
    public List<Ingredient> ingredients = new List<Ingredient>();
    private Camera m_Camera;
    void Awake()
    {
        _mouseFollower = FindObjectOfType<MouseFollower>();
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
                    if(_mouseFollower.ingredientBeingCarried != null)
                    {
                        ingredients.Add(_mouseFollower.ingredientBeingCarried);
                        _mouseFollower.ingredientBeingCarried.gameObject.transform.SetParent(null);
                        _mouseFollower.ingredientBeingCarried.gameObject.SetActive(false);
                        _mouseFollower.ingredientBeingCarried = null;
                    }
                }
            }
        }
    }

    /*public void GenerateExpectedIngredients(ClientRequest requests)
    {
        
    }*/
}
