using System;
using System.Collections;
using System.Collections.Generic;
using IngredientDetails;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cauldron : MonoBehaviour
{
    private List<IngredientDetails.Material> _materials = new();
    private MouseFollower _mouseFollower;
    public float perfectWeakPotionValue;
    public float strongPotionMultiplier;
    public float incorrectGrindStagePenalty;
    public float incorrectHeatStagePenalty;
    public float incorrectCutMultiplier;
    public float cutMarginOfError;
    public float incorrectSeverityMultiplier;
    public List<IngredientInstruction> ingredientInstructions = new ();
    public List<Ingredient> receivedIngredients = new();
    private Camera m_Camera;
    void Awake()
    {
        _mouseFollower = FindObjectOfType<MouseFollower>();
        m_Camera = Camera.main;
        UnityEngine.Object[] scripObjects = Resources.LoadAll("", typeof(IngredientDetails.Material));
        for(int i = 0; i < scripObjects.Length; i++)
        {
            _materials.Add((IngredientDetails.Material)scripObjects[i]);
        }
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
                        receivedIngredients.Add(_mouseFollower.ingredientBeingCarried);
                        _mouseFollower.ingredientBeingCarried.gameObject.transform.SetParent(null);
                        _mouseFollower.ingredientBeingCarried.gameObject.SetActive(false);
                        _mouseFollower.ingredientBeingCarried = null;
                    }
                }
            }
        }
    }

    public void GenerateExpectedIngredients(ClientRequest requests)
    {
        for(int i = 0; i < requests.Request_required_ingredients.Count; i++)
        {
            var (Emotion, Severity) = requests.Request_required_ingredients[i];
            for(int j = 0; j < _materials.Count; j++)
            {
                if(_materials[i].emotion == Emotion && _materials[i].severity == Severity)
                {
                    ingredientInstructions.Add(new IngredientInstruction(_materials[i]));
                    break;
                }
            }
        }
    }

    public float EvaluateIngredient(IngredientInstruction expected, Ingredient received)
    {

        float result = perfectWeakPotionValue;
        // If the emotions don't match, no reward
        if(expected.material.emotion != received.material.emotion) return 0;
        result -= Math.Abs(
            (int)expected.requiredLevels.grindLevel - (int)received.currentLevels.grindLevel
        ) * incorrectGrindStagePenalty;

        result -= Math.Abs(
            (int)expected.requiredLevels.temperatureLevel - (int)received.currentLevels.temperatureLevel
        ) * incorrectHeatStagePenalty;

        if(Math.Abs(expected.requiredLevels.cuttingLevel - received.currentLevels.cuttingLevel) > cutMarginOfError)
        {
            result -= Math.Abs(expected.requiredLevels.cuttingLevel - received.currentLevels.cuttingLevel) * incorrectCutMultiplier;
        }
        if(expected.material.severity != received.material.severity)
        {
            result *= incorrectSeverityMultiplier;
        }
        else if(expected.material.severity == Severity.High)
        {
            result *= strongPotionMultiplier;
        }
        return result;
    }
}
