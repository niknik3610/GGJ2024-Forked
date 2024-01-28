using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
public enum Severity
{
    Standard,
    High,
}

public class ClientRequest
{
    public List<(IngredientDetails.Emotion, Severity)> Request_required_ingredients;
    public ClientRequest()
    {
        Request_required_ingredients = new();
    }
}
