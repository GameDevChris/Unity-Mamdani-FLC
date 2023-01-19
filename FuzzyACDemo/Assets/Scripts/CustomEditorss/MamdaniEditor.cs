using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MamdaniGenerator))]
public class MamdaniEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MamdaniGenerator generator = (MamdaniGenerator)target;
        
        if (GUILayout.Button("Clear Outputs"))
        {
            generator.ClearOutputs();
        }
        
        if (GUILayout.Button("Run Mamdani System"))
        {
            generator.Run();
        }
        
        if (GUILayout.Button("Fuzzification"))
        {
            generator.Fuzzification();
        }

        if (GUILayout.Button("Inference"))
        {
            generator.Inference();
        }
        
        if (GUILayout.Button("Aggregate"))
        {
            generator.Aggregation();
        }
        
        if (GUILayout.Button("Defuzzify"))
        {
            generator.Defuzzification();
        }
    }

}
