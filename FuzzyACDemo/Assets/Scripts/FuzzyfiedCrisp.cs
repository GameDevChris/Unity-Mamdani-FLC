using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FuzzyfiedCrisp
{
    private FuzzyCurve curve;
    public string curveName;
    public float value;

    public FuzzyfiedCrisp(FuzzyCurve curve, float value)
    {
        this.curve = curve;
        this.value = value;
        this.curveName = curve.curveName;
    }
}
