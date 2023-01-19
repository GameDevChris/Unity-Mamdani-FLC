using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FuzzyCurve", menuName = "Fuzzy/CurveSO")]
public class FuzzyCurve : ScriptableObject
{
    public enum CurveType
    {
        trimf,
        gaussian,
        trapmf
    }

    [Header("Base Settings")]
    public string curveName = "Untitled";
    public CurveType type;
    public int keyCount = 100;
    public float rangeMin = 0;
    public float rangeMax = 1;

    //Trip
    [Space]
    [Header("TripMF Settings")]
    public float startValue = -0.5f;
    public float midValue = 0f;
    public float endValue = 0.5f;
    
    //Gaussian
    [Space]
    [Header("Gaussian Settings")]
    public float mean = 0.5f;
    public float deviation = 2;
    
    //Trap
    [Space]
    [Header("TrapMF Settings")]
    public float startFoot = 0;
    public float startShoulder = 0.25f;
    public float endShoulder = 0.75f;
    public float endFoot = 1f;
    
    [Space]
    [Header("Final Curve Output")]
    public AnimationCurve Curve;

    public void GenerateCurves()
    {
        switch (type)
        {
            case CurveType.trimf:
                trimfCurve();
                break;
            
            case CurveType.gaussian:
                gaussianCurve();
                break;
            
            case CurveType.trapmf:
                trapmfCurve();
                break;
        }
    }

    private void trimfCurve()
    {
        float startI = Mathf.Max
        (
            Mathf.Min
            ((0 - startValue) / (midValue - startValue)
                , (endValue - 0) / (endValue - midValue))
            , 0);

        float endI = Mathf.Max
        (
            Mathf.Min
            ((1 - startValue) / (midValue - startValue)
                , (endValue - 1) / (endValue - midValue))
            , 0);

        Curve = new AnimationCurve(new Keyframe(0, startI), new Keyframe(1, endI));

        for (float i = 0; i < rangeMax; i += (rangeMax / keyCount))
        {
            float iVal =
                Mathf.Max
                (
                    Mathf.Min
                    ((i - startValue) / (midValue - startValue)
                        , (endValue - i) / (endValue - midValue))
                    , 0);

            Curve.AddKey(i, iVal);

        }
    }

    private void gaussianCurve()
    {
        float startI = Mathf.Exp(-0.5f* Mathf.Pow((0 - mean)/deviation, 2));
        float endI = Mathf.Exp(-0.5f * Mathf.Pow((1 - mean)/deviation, 2));

        Curve = new AnimationCurve(new Keyframe(0, startI), new Keyframe(1, endI));

        for (float i = 0; i < rangeMax; i += (rangeMax / keyCount))
        {
            float iVal = Mathf.Exp(-0.5f * Mathf.Pow((i - mean)/deviation, 2));
            Curve.AddKey(i, iVal);
        }
    }

    private void trapmfCurve()
    {
        float startI = Mathf.Max
        (
            Mathf.Min
            ((0 - startFoot) / (startShoulder - startFoot)
                , 1
                ,(endFoot - 0) / (endFoot - endShoulder)
                )
            ,
            0);
        
        float endI = Mathf.Max
        (
            Mathf.Min
            ((1 - startFoot) / (startShoulder - startFoot)
                , 1
                ,(endFoot - 1) / (endFoot - endShoulder)
            )
            ,
            0);

        Curve = new AnimationCurve(new Keyframe(0, startI), new Keyframe(1, endI));

        for (float i = 0; i < rangeMax; i += (rangeMax / keyCount))
        {
            float iVal =
                Mathf.Max
                (
                    Mathf.Min
                    ((i - startFoot) / (startShoulder - startFoot)
                        , 1
                        ,(endFoot - i) / (endFoot - endShoulder)
                    )
                    ,
                    0);

            Curve.AddKey(i, iVal);

        }
    }
    
}
