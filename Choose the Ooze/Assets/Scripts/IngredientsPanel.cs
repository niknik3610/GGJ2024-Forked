using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientsPanel : MonoBehaviour
{
    public Cauldron cauldron;
    public GameObject ingredientListEntry;

    public List<GameObject> instantiatedEntries = new();

    private int yPositionOfNextEntry = -25;

    public void UpdateInstructionList(List<IngredientInstruction> instructions)
    {
        TemperatureLevel lastLevel = TemperatureLevel.TooCold;
        string text;
        for(int i = 0; i < instructions.Count; i++)
        {
            char c = instructions[i].material.ingredientName.ToCharArray()[0];
            bool isVowel = "aeioAEIO".IndexOf(c) >= 0;
            if(isVowel)
            {
                text = "Take an ";
            }
            else text = "Take a ";
            text += instructions[i].material.ingredientName + ":";
            
            AddNewEntry(text);
            if(Math.Abs(instructions[i].requiredLevels.cuttingLevel-1) > 0.001f)
            {
                float cutoff = 1-instructions[i].requiredLevels.cuttingLevel;
                text = "Cut off " + cutoff.ToString("0.0");
                AddNewEntry(text);
            }
            if(instructions[i].requiredLevels.grindLevel != GrindLevel.None)
            {
                text = "Grind it to ";
                switch (instructions[i].requiredLevels.grindLevel)
                {
                    case GrindLevel.None:
                        text += "none(?)";
                        break;
                    case GrindLevel.Low:
                        text += "low";
                        break;
                    case GrindLevel.Medium:
                        text += "medium";
                        break;
                    case GrindLevel.High:
                        text += "high";
                        break;
                    default:
                        Debug.Log("Why are we here?");
                        Debug.Log(instructions[i].requiredLevels.grindLevel);
                        break;
                }
                AddNewEntry(text);
            }
            if(instructions[i].requiredLevels.temperatureLevel != lastLevel)
            {
                text = "Set temperature to ";
                switch (instructions[i].requiredLevels.temperatureLevel)
                {
                    case TemperatureLevel.TooCold:
                        text += "cold";
                        break;
                    case TemperatureLevel.Low:
                        text += "low";
                        break;
                    case TemperatureLevel.Medium:
                        text += "medium";
                        break;
                    case TemperatureLevel.High:
                        text += "high";
                        break;
                    case TemperatureLevel.TooHot:
                        text += "too hot(?)";
                        break;
                }
                AddNewEntry(text);
                lastLevel = instructions[i].requiredLevels.temperatureLevel;
            }
            AddNewEntry("Put in cauldron");
        }
        AddNewEntry("Whisper happy thoughts!");
    }


    public void clearEntries()
    {
        foreach(var entry in instantiatedEntries)
        {
            Destroy(entry);
        }
        instantiatedEntries = new();
        yPositionOfNextEntry = -25;
    }

    public void AddNewEntry(string text)
    {
        GameObject newEntry = Instantiate(ingredientListEntry, new Vector3(0, yPositionOfNextEntry, 0), Quaternion.identity, this.transform);
        newEntry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPositionOfNextEntry);
        newEntry.GetComponent<TextMeshProUGUI>().text = text;
        yPositionOfNextEntry -= 20;
        instantiatedEntries.Add(newEntry);
    }
}
