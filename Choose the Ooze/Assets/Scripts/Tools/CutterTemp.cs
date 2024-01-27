using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CutterTemp : MonoBehaviour
{
    private Camera _camera;
    private MouseFollower _mouseFollower;
    public Ingredient ingredientInProcess = null;
    public Button cutButton;
    void Start()
    {
        _camera = Camera.main;
        _mouseFollower = FindObjectOfType<MouseFollower>();
    }

    public void Cut()
    {
        if(ingredientInProcess == null) return;
        ingredientInProcess.currentLevels.cuttingLevel -= 0.2f;
    }


    // Update is called once per frame
    void Update()
    {
        _mouseFollower.ingredientInProcess = !(ingredientInProcess == null);
        Mouse mouse = Mouse.current;
        if(ingredientInProcess != null && ingredientInProcess.currentLevels.cuttingLevel > 0.2)
        {
            cutButton.interactable = true;
        }
        else
        {
            cutButton.interactable = false;
        }
        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = mouse.position.ReadValue();
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach(var rhit in raycastHits)
            {
                if(rhit.collider.gameObject == gameObject)
                {
                    if(_mouseFollower.ingredientBeingCarried != null && ingredientInProcess == null)
                    {
                        ingredientInProcess = _mouseFollower.ingredientBeingCarried;
                        _mouseFollower.ingredientBeingCarried.transform.SetParent(gameObject.transform, false);
                        _mouseFollower.ingredientInProcess = true;
                        _mouseFollower.ingredientBeingCarried = null;
                    }
                    else if(_mouseFollower.ingredientBeingCarried == null && ingredientInProcess != null)
                    {
                        _mouseFollower.ingredientBeingCarried = ingredientInProcess;
                        ingredientInProcess.transform.SetParent(_mouseFollower.transform, false);
                        _mouseFollower.ingredientInProcess = false;
                        ingredientInProcess = null;
                    }
                }
            }
        }

    }
}
