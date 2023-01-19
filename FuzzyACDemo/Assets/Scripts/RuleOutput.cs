using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RuleOutput
{
    public MF mf;
    public int curveIndex;
    public float outputValue;

    public RuleOutput(float outputValue, MF mf, int curveIndex)
    {
        this.outputValue = outputValue;
        this.mf = mf;
        this.curveIndex = curveIndex;
    }

}
