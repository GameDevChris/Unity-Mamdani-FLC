using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CrispValue
{
    public string valName;
    public string valueUnit;
    public float value;
    public float minimumValue;
    public float maximumValue;

    public Slider valueSlider;
    
    public List<FuzzyfiedCrisp> fuzzyfiedCrisps;
    
    public MF membershipFunction;

    public CrispValue(string valName, float value, float minValue, float maxValue)
    {
        this.valName = valName;
        this.value = value;
        this.minimumValue = minValue;
        this.value = maxValue;
        this.valueUnit = "Undefined";
    }

    public CrispValue() {}
}
