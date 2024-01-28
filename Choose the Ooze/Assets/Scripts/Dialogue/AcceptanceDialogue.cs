using System.Collections;
using System.Collections.Generic;
using IngredientDetails;
using UnityEngine;

public enum Happiness
{
    Terrible, Poor, Normal, Great, Perfect
}
[System.Serializable]
public class AcceptanceDialogue
{
    public Happiness happinessLevel;
    [TextArea(3, 5)]
    public string[] dialogueLines;
}