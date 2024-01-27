using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Severity
{
    Standard,
    High,
}

public class ClientRequest
{
    List<(IngredientDetails.Emotion, Severity)> Request_required_ingredients;
}
