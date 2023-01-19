using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiatorController : MonoBehaviour
{
    public OutputController radiatorOutput;
    public Gradient powerGradient;
    public float currentPowerPercentage = 0;
    public Color currentColour;
    public Transform ModelToColour;
    public float lerpDuration = 2; 
    bool lerping = false;
    private Coroutine lerpCorouting;
    void Start()
    {
        currentColour = ModelToColour.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(radiatorOutput.outputValue - currentPowerPercentage) > 0.1)
        {
            if (lerping)
            {
                StopCoroutine(lerpCorouting);
                lerping = false;
            }
            currentPowerPercentage = radiatorOutput.outputValue;
            Color newColor = powerGradient.Evaluate(currentPowerPercentage / 100);
            Color originalColor = currentColour;
            lerpCorouting = StartCoroutine(Lerp(originalColor, newColor));
            currentColour = newColor;
        }
    }
    
    IEnumerator Lerp(Color oldColor, Color newColor)
    {
        lerping = true;
        float timeElapsed = 0;
        while (timeElapsed < lerpDuration)
        {
            ModelToColour.GetComponent<MeshRenderer>().material.color = Color.Lerp(oldColor, newColor, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lerping = false;
    }
}
