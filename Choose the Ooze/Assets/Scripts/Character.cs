using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public ClientType type {get; private set;} = ClientType.None;
    

    public void SetClientType(ClientType Type)
    {
        type = Type;
        switch (type)
        {
            case ClientType.None:
                break;
            case ClientType.Fish:
                Debug.Log("Attempting to load fisherman sprite...");
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/SadFisherman");
                break;
            case ClientType.Wizard:
                break;
            case ClientType.Viking:
                break;
            case ClientType.Cultist:
                break;
            case ClientType.Scientist:
                break;
            case ClientType.Princess:
                break;
            case ClientType.Milkman:
                break;
            case ClientType.Child:
                break;
        }
    }

}
