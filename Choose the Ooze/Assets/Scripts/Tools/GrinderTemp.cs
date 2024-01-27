using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrinderTemp : MonoBehaviour
{
    private Camera _camera;
    private MouseFollower _mouseFollower;
    public Ingredient ingredientInProcess = null;
    public Button grindButton;
    void Start()
    {
        _camera = Camera.main;
        _mouseFollower = FindObjectOfType<MouseFollower>();
    }

    public void Grind()
    {
        if(ingredientInProcess == null) return;
        switch (ingredientInProcess.currentLevels.grindLevel)
        {
            case GrindLevel.None:
                ingredientInProcess.currentLevels.grindLevel = GrindLevel.Low;
                break;
            case GrindLevel.Low:
                ingredientInProcess.currentLevels.grindLevel = GrindLevel.Medium;
                break;
            case GrindLevel.Medium:
                ingredientInProcess.currentLevels.grindLevel = GrindLevel.High;
                break;
            case GrindLevel.High:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        _mouseFollower.ingredientInProcess = !(ingredientInProcess == null);
        Mouse mouse = Mouse.current;
        if(ingredientInProcess != null && ingredientInProcess.currentLevels.grindLevel != GrindLevel.High)
        {
            grindButton.interactable = true;
        }
        else
        {
            grindButton.interactable = false;
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
