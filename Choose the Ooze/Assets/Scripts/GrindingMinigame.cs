using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrindingMinigame : MonoBehaviour
{
    Camera grindingCamera;
    public Ingredient ingredient;
    public Pestle pestle;

    void Awake()
    {
        grindingCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        /*         Mouse mouse = Mouse.current;
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    Vector3 mousePosition = mouse.position.ReadValue();
                    Ray ray = grindingCamera.ScreenPointToRay(mousePosition);
                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if (hit.collider.gameObject == pestle.gameObject) { }
                    }
                }
          */
    }
}
