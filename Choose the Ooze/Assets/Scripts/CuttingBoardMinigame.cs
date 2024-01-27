using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CuttingBoardMinigame : MonoBehaviour
{
    Camera mainCamera; 
    public Slicer slicer;
    public Ingredient ingredient;
    public MouseFollower mouseFollower;
    public SpriteMask mask;
    public TMP_Text weightSign;
    public GameObject cutSign;
    public GameObject pickOneSign;

    private bool allowedToCut;


    public void Reset() {
        allowedToCut = true;
        cutSign.SetActive(true);
        pickOneSign.SetActive(false);
    }

    void Awake() {
        Reset();
        ingredient.gameObject.transform.SetParent(this.transform);
        ingredient.gameObject.transform.position = new Vector3(-5, -2f);
        mainCamera = Camera.main; 
    }
    // Start is called before the first frame update
    void Start()
    {
        mask.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        String mat = ingredient.material.ingredientName;
        float weight = ingredient.material.weight;
        weightSign.text = String.Format("{0}: {1}g per Item", mat, weight);

        if (!allowedToCut) {
            return;
        }

        Mouse mouse = Mouse.current;
        if (!mouse.leftButton.wasPressedThisFrame) {
            return;
        }

        Vector3 mousePosition = mouse.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }

        if (hit.collider.gameObject == ingredient.gameObject)
        {
            allowedToCut = false;
            cutSign.SetActive(false);
            pickOneSign.SetActive(true);
            float xSlicePos = mainCamera.ScreenToWorldPoint(mousePosition).x;
            slicer.Slice(xSlicePos, ingredient, mask);
        }
    }
}
