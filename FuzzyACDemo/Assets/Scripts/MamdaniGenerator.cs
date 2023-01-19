using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mamdani", menuName = "Fuzzy/MamdaniGeneratorSO")]
public class MamdaniGenerator : ScriptableObject
{
    public List<FuzzyRule> fuzzyRules;
    public CrispData data;
    public List<FuzzyOutput> fuzzyOutputs;
    public List<MF> memberships;

    public void Run()
    {
        ClearOutputs();
        Fuzzification();
        Inference();
        Aggregation();
        Defuzzification();
    }
    
    public void Fuzzification()
    {
        foreach (var value in data.crisps)
        {
            value.fuzzyfiedCrisps = new List<FuzzyfiedCrisp>();
            foreach (var curve in value.membershipFunction.curves)
            {
                value.fuzzyfiedCrisps.Add(new FuzzyfiedCrisp(curve, curve.Curve.Evaluate(value.value)));
            }
        }
    }

    public void Inference()
    {
        foreach (var rule in fuzzyRules)
        {
            rule.EvaluateData(data);
        }
    }

    public void Aggregation()
    {
        foreach (var output in fuzzyOutputs)
        {
            output.CreateOutputCurves();
        }
    }

    public void Defuzzification()
    {
        foreach (var output in fuzzyOutputs)
        {
            output.GetOutputValue();
        }
    }

    public void ClearOutputs()
    {
        foreach (var mf in memberships)
        {
            mf.fuzzifiedValues.Clear();
        }
    }
}
