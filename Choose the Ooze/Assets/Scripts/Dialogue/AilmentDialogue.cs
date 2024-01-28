using System.Collections;
using System.Collections.Generic;
using IngredientDetails;
using UnityEngine;


[System.Serializable]
public class AilmentDialogue
{
    public Emotion emotion;
    public Severity severity;
    [TextArea(3, 5)]
    public string[] dialogueLines;
}
