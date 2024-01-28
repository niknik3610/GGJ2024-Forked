using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleWipe : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void Wipe()
    {
        transition.SetTrigger("StartTransition");
    }
}
