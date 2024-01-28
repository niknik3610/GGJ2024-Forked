using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public float value;
    public ClientRequest potionContents;
    public void SetPotion(Potion p)
    {
        value = p.value;
        potionContents = p.potionContents;
    }
}
