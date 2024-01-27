using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeatButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
 
    public bool buttonPressed;

    public float changeRateIncreaseOnPress;
    public float changeRateDecreaseOffPress;
    public Cauldron cauldron;

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("Pressed Button");
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData){
        Debug.Log("Released Button");
        buttonPressed = false;
    }

    void Update()
    {
        if(buttonPressed)
        {
            cauldron.currentChangeRate += changeRateIncreaseOnPress * Time.deltaTime;
        }
        else
        {
            if(cauldron.currentChangeRate < 0 - cauldron.defaultTemperatureDecreasePerSecond)
            {
                cauldron.currentChangeRate = 0 - cauldron.defaultTemperatureDecreasePerSecond;
            }
            else if(cauldron.currentChangeRate > 0 - cauldron.defaultTemperatureDecreasePerSecond)
            {
                cauldron.currentChangeRate -= changeRateDecreaseOffPress * Time.deltaTime;
            }
        }
    }

    /*public IEnumerator disableButton()
    {
        Button button = GetComponent<Button>();
    }*/
}