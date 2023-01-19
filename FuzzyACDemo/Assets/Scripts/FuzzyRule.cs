using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FuzzyRule", menuName = "Fuzzy/FuzzyRuleSO")]
public class FuzzyRule : ScriptableObject
{
    public enum Display
    {
        setup,
        saved
    }

    [HideInInspector] public Display currentEditor = Display.setup;
    
    public enum ConnectionType
    {
        OR,
        AND
    }

    [HideInInspector] public List<RuleConnection> inputConnections = new List<RuleConnection>();
    [HideInInspector] public List<RuleConnection> outputConnections = new List<RuleConnection>();
    [HideInInspector] public List<float> inputFuzzyValues = new List<float>();
    [HideInInspector] public List<RuleOutput> outputFuzzyValues = new List<RuleOutput>();
    
    public List<MF> inputs = new List<MF>();
    public List<MF> outputs = new List<MF>();
    public ConnectionType connection = ConnectionType.AND;

    
    public void EvaluateData(CrispData data)
    {
        inputFuzzyValues.Clear();
        foreach (var input in inputConnections)
        {
            foreach (var crisp in data.crisps)
            {
                if (crisp.membershipFunction == input.membership)
                {
                    inputFuzzyValues.Add(crisp.fuzzyfiedCrisps[input.index].value);
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        outputFuzzyValues.Clear();

        foreach (var output in outputConnections)
        {
            if (connection == ConnectionType.AND)
            {
                RuleOutput newEntry = new RuleOutput(Mathf.Min(inputFuzzyValues.ToArray()), output.membership, output.index);
                outputFuzzyValues.Add(newEntry);
                output.membership.fuzzifiedValues.Add(newEntry);
            }
            else
            {
                RuleOutput newEntry = new RuleOutput(Mathf.Max(inputFuzzyValues.ToArray()), output.membership, output.index);
                outputFuzzyValues.Add(newEntry);
                output.membership.fuzzifiedValues.Add(newEntry);
            }
        }
    }
    
    public void SaveData()
    {
        inputConnections.Clear();
        foreach (var input in inputs)
        {
            RuleConnection newConnection = new RuleConnection(input, input.mfIndex);
            inputConnections.Add(newConnection);
        }
        
        outputConnections.Clear();
        foreach (var output in outputs)
        {
            RuleConnection newConnection = new RuleConnection(output, output.mfIndex);
            outputConnections.Add(newConnection);
        }
    }
}
