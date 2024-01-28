using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDialogue
{
    public ClientType client;
    [TextArea(3, 5)]
    public string[] introLines;
    [TextArea(3, 5)]
    public string[] outroLines;
}