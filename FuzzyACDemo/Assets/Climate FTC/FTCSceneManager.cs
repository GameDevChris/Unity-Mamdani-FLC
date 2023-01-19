using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FTCSceneManager : MonoBehaviour
{
    public CrispData crispData;
    public List<FuzzyOutput> outputData;

    public List<CrispController> crispControllers;
    public List<OutputController> outputControllers;

    private void Start()
    {
        for (int i = 0; i < crispControllers.Count; i++)
        {
            crispControllers[i].Setup(crispData.crisps[i]);
        }
        
        for (int i = 0; i < outputControllers.Count; i++)
        {
            outputControllers[i].Setup(outputData[i]);
        }
    }
}
