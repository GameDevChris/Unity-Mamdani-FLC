using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MF))]
public class MFEditor : Editor
{
    int curveToolbarInt = 0;
    string[] curveToolbarStrings = {"TriMF Curves", "TrapMF Curves", "GaussianMF Curves"};
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MF membershipFunction = (MF)target;

        GUILayout.Space(20);
        if (GUILayout.Button("Update Curves"))
        {
            membershipFunction.UpdateCurveDisplay();
        }
        GUILayout.Space(20);
        
        if (GUILayout.Button("Save Curves"))
        {
            membershipFunction.SaveCurves();
        }
        GUILayout.Space(20);
        
        EditorGUILayout.LabelField("Generate Curves:", EditorStyles.largeLabel);
        curveToolbarInt = GUILayout.Toolbar(curveToolbarInt, curveToolbarStrings);
        
        if (GUILayout.Button("Generate"))
        {
            switch (curveToolbarInt)
            {
                case 0:
                    membershipFunction.GenerateTrimfCurves();
                    break;
                
                case 1:
                    membershipFunction.GenerateTrapmfCurves();
                    break;
                
                case 2:
                    membershipFunction.GenerateGaussianCurves();
                    break;
            }
        }
    }
}
