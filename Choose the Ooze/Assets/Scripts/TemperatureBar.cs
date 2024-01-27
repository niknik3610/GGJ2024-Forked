using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureBar : MonoBehaviour
{
    private Slider slider;
    public Gradient gradient;  
    public Image fill; 

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void setSliderValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(value);
    }
}
