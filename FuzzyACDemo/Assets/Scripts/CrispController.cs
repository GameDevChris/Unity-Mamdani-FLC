using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrispController : MonoBehaviour
{
    public Slider controlSlider;
    public TextMeshProUGUI controlText;
    public CrispValue controlledCrisp;
    
    public void Setup(CrispValue crisp)
    {
        controlledCrisp = crisp;
        controlSlider.maxValue = crisp.maximumValue;
        controlSlider.minValue = crisp.minimumValue;
        controlSlider.value = crisp.value;
        
        crisp.valueSlider = controlSlider;
    }

    public void Update()
    {
        controlledCrisp.value = controlSlider.value;
        controlText.text = controlledCrisp.valName + ": " + controlledCrisp.value + controlledCrisp.valueUnit;
    }
}
