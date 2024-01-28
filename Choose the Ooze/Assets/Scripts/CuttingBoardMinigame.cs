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
    private Ingredient ingredient;
    public MouseFollower mouseFollower;
    public GameObject knife;
    public SpriteMask mask;
    public TMP_Text weightSign;
    public GameObject cutSign;
    public GameObject pickOneSign;
    public GameObject locationReferenceIngredient;

    private bool allowedToCut;
    private bool finished;
    private bool allowedToRun;
    private Ingredient cutIngredient;


    public void EnterMiniGame() {
        knife.transform.SetParent(mouseFollower.transform);
        allowedToCut = true;
        finished = false;
        cutSign.SetActive(true);
        pickOneSign.SetActive(false);
        knife.SetActive(true);
        mask.gameObject.SetActive(false);
        if (mouseFollower.ingredientBeingCarried != null) {
            ingredient = mouseFollower.ingredientBeingCarried;
            ingredient.gameObject.transform.SetParent(this.transform);
            ingredient.gameObject.transform.position = locationReferenceIngredient.transform.position;
            ingredient.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 0);

            // ingredient.gameObject.transform.rotation *= Quaternion.Euler(0, 0, -90);
            allowedToRun = true;
        }
    }
    public void ExitMiniGame()
    {
        allowedToRun = false;
        allowedToCut = false;
        finished = true;
        cutSign.SetActive(false);
        pickOneSign.SetActive(true);
        knife.SetActive(false);
    }

    void Awake() {
        ExitMiniGame();
        mainCamera = Camera.main; 
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (finished || !allowedToRun) return;

        String mat = ingredient.material.ingredientName;
        float weight = ingredient.material.weight;
        weightSign.text = String.Format("{0}: {1}g per Item", mat, weight);

        if (!allowedToCut) {
            Ingredient result = this.userPick();

            if (result != null) {
                mouseFollower.SetIngredient(result);
            }
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
                     
        }

        if (hit.collider.gameObject == ingredient.gameObject)
        {
            allowedToCut = false;
            knife.SetActive(false);
            cutSign.SetActive(false);
            pickOneSign.SetActive(true);
            float xSlicePos = mainCamera.ScreenToWorldPoint(mousePosition).x;

            cutIngredient = slicer.Slice(xSlicePos, ingredient, mask);
            float randomNum = UnityEngine.Random.Range(0, 100);
            if (randomNum > 0.66) {
                AudioManager.instance.Play("chop_best");
            }
            else if (randomNum > 0.33) {
                AudioManager.instance.Play("chop_okay");
            }
            else {
                AudioManager.instance.Play("chop_bad");
            }
        }
    }

    private Ingredient userPick() {
        Mouse mouse = Mouse.current;
        if (!mouse.leftButton.wasPressedThisFrame) {
            return null;
        }

        Vector3 mousePos = mouse.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return null;
        }

        //left piece
        if (hit.collider.gameObject == ingredient.gameObject)
        {
            cutIngredient.gameObject.SetActive(false);
            Destroy(cutIngredient);
            finished = true;
            ingredient.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            return ingredient;
        }
        //right piece
        else if (hit.collider.gameObject == cutIngredient.gameObject) {
            ingredient.gameObject.SetActive(false);
            Destroy(ingredient);
            finished = true;
            cutIngredient.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            return cutIngredient;
        }

        return null;
    }
}
