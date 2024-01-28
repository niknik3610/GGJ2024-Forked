using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeAudio : MonoBehaviour
{
    bool previouslyHovered = false;

    void Update()
    {
        bool beingHovered = MouseHelper.isBeingHoveredThisFrame(this.gameObject);
        if (previouslyHovered && beingHovered) return;
        if (beingHovered)
        {
            if (Random.Range(0, 1) > 0.33)
            {
                AudioManager.instance.Play("knife_sharpen1");
            } else if (Random.Range(0,1) > 0.5)
            {
                AudioManager.instance.Play("knife_sharpen2");
            } else
            {
                AudioManager.instance.Play("knife_sharpen3");
            }

        }
        previouslyHovered = beingHovered;
    }
}
