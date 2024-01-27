using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GrindLevel
{
    None, Low, Medium, High
}
public enum TemperatureLevel
{
    None, Low, Medium, High
}

public class ProcessingLevels
{
    public GrindLevel grindLevel;
    public TemperatureLevel temperatureLevel;
    public float cuttingLevel;

    public ProcessingLevels(bool random)
    {
        if(random)
        {
            grindLevel = (GrindLevel) Random.Range(0, 5);
            temperatureLevel = (TemperatureLevel) Random.Range(0, 5);
            cuttingLevel = (float)Random.Range(2, 11)/10;
        }
        else
        {
            grindLevel = GrindLevel.None;
            temperatureLevel = TemperatureLevel.None;
            cuttingLevel = 1;
        }
    }
    public ProcessingLevels(GrindLevel gLevel, TemperatureLevel tLevel, float cLevel)
    {
        grindLevel = gLevel;
        temperatureLevel = tLevel;
        cuttingLevel = cLevel;
    }
}