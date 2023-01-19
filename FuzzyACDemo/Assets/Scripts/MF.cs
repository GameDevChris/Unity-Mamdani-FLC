using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "FuzzyMF", menuName = "Fuzzy/MembershipFunctionSO")]
public class MF : ScriptableObject
{
    public enum MFType
    {
        input,
        output
    }

    public List<FuzzyCurve> curves = new List<FuzzyCurve>();
    public List<AnimationCurve> visibleCurves = new List<AnimationCurve>();
    
    public string mfName = "Untitled";
    public List<string> curveNames = new List<string>();
    public MFType type = MFType.input;
    public int mfIndex = 0;
    public int curveCount = 3;
    public int curveResolution = 100;
    public float rangeMin = 0;
    public float rangeMax = 1;
    public float triWidth = 1f;
    public float trapTopWidth = 0.05f;
    public float trapBottomWidth = 0.9f;
    public float gaussianWidth = 2f;

    public List<RuleOutput> fuzzifiedValues;

    public void UpdateCurveDisplay()
    {
        visibleCurves.Clear();
        foreach (var cur in curves)
        {
            visibleCurves.Add(cur.Curve);
        }
        
        curveNames.Clear();
        foreach (var cur in curves)
        {
            curveNames.Add(cur.curveName);
        }
    }
    
    public void GenerateTrimfCurves()
    {
        curves.Clear();
        for (int i = 0; i < curveCount; i++)
        {
            FuzzyCurve curve = ScriptableObject.CreateInstance<FuzzyCurve>();
            curve.type = FuzzyCurve.CurveType.trimf;

            curve.keyCount = curveResolution;
            curve.rangeMin = rangeMin;
            curve.rangeMax = rangeMax;
            
            curve.midValue = 0 + ((rangeMax / ((float)curveCount-1)) * i);
            curve.startValue = curve.midValue - triWidth / 2;
            curve.endValue = curve.midValue + triWidth / 2;
            
            curve.curveName = curveNames[i]; 
            
            curve.GenerateCurves();
            curves.Add(curve);
        }
        SaveCurves();
        UpdateCurveDisplay();
    }
    
    public void GenerateTrapmfCurves()
    {
        curves.Clear();
        for (int i = 0; i < curveCount; i++)
        {
            FuzzyCurve curve = ScriptableObject.CreateInstance<FuzzyCurve>();
            curve.type = FuzzyCurve.CurveType.trapmf;
            
            curve.keyCount = curveResolution;
            curve.rangeMin = rangeMin;
            curve.rangeMax = rangeMax;

            curve.startFoot = 0 + ((rangeMax / ((float)curveCount - 1)) * i) - (trapBottomWidth / 2);
            curve.startShoulder = 0 + ((rangeMax / ((float)curveCount - 1)) * i) - (trapTopWidth/ 2);
            curve.endShoulder = 0 + ((rangeMax / ((float)curveCount - 1)) * i) + (trapTopWidth/ 2);
            curve.endFoot = 0 + ((rangeMax / ((float)curveCount - 1)) * i) + (trapBottomWidth / 2);

            curve.curveName = curveNames[i]; 
            
            curve.GenerateCurves();
            curves.Add(curve);
        }
        SaveCurves();
        UpdateCurveDisplay();
    }
    
    public void GenerateGaussianCurves()
    {
        curves.Clear();
        for (int i = 0; i < curveCount; i++)
        {
            FuzzyCurve curve = ScriptableObject.CreateInstance<FuzzyCurve>();
            curve.type = FuzzyCurve.CurveType.gaussian;
            
            curve.keyCount = curveResolution;
            curve.rangeMin = rangeMin;
            curve.rangeMax = rangeMax;
            
            curve.mean = 0 + ((rangeMax / ((float)curveCount-1)) * i);
            curve.deviation = gaussianWidth;

            curve.curveName = curveNames[i];            
            
            curve.GenerateCurves();
            curves.Add(curve);
        }
        SaveCurves();
        UpdateCurveDisplay();
    }

    public void SaveCurves()
    {
        if (!Directory.Exists("Assets/Prefabs"))
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
        }
        
        if (!Directory.Exists("Assets/Prefabs/" + mfName))
        {
            AssetDatabase.CreateFolder("Assets/Prefabs", mfName);
        }
        
        foreach (FuzzyCurve curve in curves)
        {
            string localPath = "Assets/Prefabs/" + mfName + "/" + curve.curveName + ".asset";
            
            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }
            
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            AssetDatabase.CreateAsset(curve, localPath);
        }
    }
}
