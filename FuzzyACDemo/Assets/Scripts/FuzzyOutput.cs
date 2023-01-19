using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "FuzzyOutput", menuName = "Fuzzy/FuzzyOutputSO")]
public class FuzzyOutput : ScriptableObject
{
    public MF outputMf;

    public List<AnimationCurve> outCurves = new List<AnimationCurve>();

    public AnimationCurve outputCurve = new AnimationCurve();

    public float centerOfGravity;
    public void CreateOutputCurves()
    {
        outCurves.Clear();
        foreach (var val in outputMf.fuzzifiedValues)
        {
            FuzzyCurve newCurve = outputMf.curves[val.curveIndex];

            AnimationCurve newOutCurve = new AnimationCurve(
                new Keyframe(0, GetKeyFromOutput(newCurve, 0, val.outputValue)),
                          new Keyframe(1, GetKeyFromOutput(newCurve, 1, val.outputValue)));
            
            for(int i=0; i<newCurve.Curve.keys.Length; i++)
            {
                Keyframe key = new Keyframe(i, GetKeyFromOutput(newCurve, i, val.outputValue));
                newOutCurve.AddKey(key);
            }
            outCurves.Add(newOutCurve);
        }

        outputCurve = new AnimationCurve(
            new Keyframe(0, GetHighestAtIndex(0)),
            new Keyframe(1, GetHighestAtIndex(1)));
        
        for (int i = (int)outputMf.rangeMin; i < (int)outputMf.rangeMax; i++)
        {
            outputCurve.AddKey(new Keyframe(i,GetHighestAtIndex(i)));
        }
    }
    
    public void GetOutputValue()
    {
        float dividend = 0;
        float divisor = 0;
        
        foreach (var key in outputCurve.keys)
        {
            dividend += key.time * key.value;
            divisor += key.value;
        }

        centerOfGravity = dividend / divisor;
    }

    float GetKeyFromOutput(FuzzyCurve curve, int timeIndex, float maxY)
    {
        float output;
        if (curve.Curve.keys[timeIndex].time < curve.rangeMin)
        {
            output = 0;
        }
                
        if (curve.Curve.keys[timeIndex].time > curve.rangeMax)
        {
            output = 0;
        }
                
        if (curve.Curve.keys[timeIndex].value > maxY)
        {
            output = maxY;
        }

        else
        {
            output = curve.Curve.keys[timeIndex].value;
        }

        return output;
    }

    float GetHighestAtIndex(int timeIndex)
    {
        List<float> indices = new List<float>();
        foreach (var curve in outCurves)
        {
            indices.Add(curve[timeIndex].value);
        }

        return Mathf.Max(indices.ToArray());
    }
}
