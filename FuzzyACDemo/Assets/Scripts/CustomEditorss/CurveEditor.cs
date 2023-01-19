using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FuzzyCurve))]

public class CurveEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FuzzyCurve fuzzyCurve = (FuzzyCurve)target;
        if (GUILayout.Button("Generate Curves"))
        {
            fuzzyCurve.GenerateCurves();
        }
    }
}
