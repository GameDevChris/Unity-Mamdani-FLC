using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public CrispData data = null;
    public FuzzyOutput output;
    
    public List<Slider> inputSliders;
    public Slider outSlider;

    private void Start()
    {
        if (data != null)
        {
            for (int i = 0; i < data.crisps.Count; i++)
            {
                data.crisps[i].valueSlider = inputSliders[i];
            }

            data.crisps[3].valueSlider = outSlider;
        }
    }

    private void Update()
    {
        if (data != null)
        {
            foreach (var crisp in data.crisps)
            {
                if (crisp.valueSlider != null)
                {
                    crisp.valueSlider.minValue = crisp.minimumValue;
                    crisp.valueSlider.maxValue = crisp.maximumValue;

                    crisp.value = crisp.valueSlider.value;
                }
            }

            data.crisps[3].value = output.centerOfGravity;
            outSlider.minValue = data.crisps[3].minimumValue;
            outSlider.maxValue = data.crisps[3].maximumValue;
            outSlider.value = data.crisps[3].value;
        }
    }
}
