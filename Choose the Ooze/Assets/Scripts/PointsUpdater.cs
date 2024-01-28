using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsUpdater : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = "Points: " + GameState.getInstance().currentScore;
    }
}
