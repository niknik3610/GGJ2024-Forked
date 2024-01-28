using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transitioner : MonoBehaviour
{
    public GameState.State associatedScreen;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHits = Physics.RaycastAll(ray);
            foreach (var rhit in raycastHits)
            {
                if (rhit.collider.gameObject == gameObject)
                {
                    Transition();
                }
            }
        }
    }

    public void Transition()
    {
        GameState.getInstance().attemptTransition(associatedScreen);
    }
}
