using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OutputController : MonoBehaviour
{
    public float defaultOutput = 0f;
    public string startText;
    public TextMeshProUGUI outputText;
    public FuzzyOutput controlledOutput;
    public float outputValue;
    
    private void Start()
    {
        startText = outputText.text;
    }

    public void Setup(FuzzyOutput output)
    {
        controlledOutput = output;
    }

    public void Update()
    {
        outputValue = float.IsNaN(controlledOutput.centerOfGravity) ? defaultOutput : controlledOutput.centerOfGravity;
        outputText.text = startText + outputValue.ToString("F1") + "%";
    }
}
