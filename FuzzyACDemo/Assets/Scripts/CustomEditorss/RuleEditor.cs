using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FuzzyRule))]
public class RuleEditor : Editor
{
   
    public override void OnInspectorGUI()
    {
        FuzzyRule fuzzyRule = (FuzzyRule)target;

        if (fuzzyRule.currentEditor == FuzzyRule.Display.setup)
        {
            EditorGUILayout.LabelField("Setup rule:", EditorStyles.largeLabel);
            
            DrawDefaultInspector();
            
            GUILayout.Space(20);
            EditorGUILayout.LabelField("IF",  EditorStyles.largeLabel);
            for (int i = 0; i < fuzzyRule.inputs.Count; i++)
            {
                if (fuzzyRule.inputs[i] != null)
                {
                    Rect r = EditorGUILayout.BeginHorizontal("Box");
                    EditorGUILayout.LabelField(fuzzyRule.inputs[i].mfName + " is:", EditorStyles.largeLabel);
                    fuzzyRule.inputs[i].mfIndex = EditorGUILayout.Popup(fuzzyRule.inputs[i].mfIndex,
                        fuzzyRule.inputs[i].curveNames.ToArray());
                    EditorGUILayout.EndHorizontal();
                    if (i + 1 != fuzzyRule.inputs.Count)
                    {
                        EditorGUILayout.LabelField(fuzzyRule.connection.ToString(), EditorStyles.largeLabel);
                    }
                    else
                    {
                        EditorGUILayout.LabelField("THEN", EditorStyles.largeLabel);
                    }
                }
            }
            
            for (int i = 0; i < fuzzyRule.outputs.Count; i++)
            {
                if (fuzzyRule.outputs[i] != null)
                {
                    Rect r = EditorGUILayout.BeginHorizontal("Box");
                    EditorGUILayout.LabelField(fuzzyRule.outputs[i].mfName + " is:", EditorStyles.largeLabel);
                    fuzzyRule.outputs[i].mfIndex = EditorGUILayout.Popup(fuzzyRule.outputs[i].mfIndex,
                        fuzzyRule.outputs[i].curveNames.ToArray());
                    EditorGUILayout.EndHorizontal();
                    if (i + 1 != fuzzyRule.outputs.Count)
                    {
                        EditorGUILayout.LabelField("AND", EditorStyles.largeLabel);
                    }
                }
            }

            GUILayout.Space(20);
        
            if (GUILayout.Button("Save Rule"))
            {
                fuzzyRule.SaveData();
                fuzzyRule.currentEditor = FuzzyRule.Display.saved;
            }
        }

        else
        {
            EditorGUILayout.LabelField("Rule Data:", EditorStyles.largeLabel);
            GUILayout.Space(20);
            EditorGUILayout.LabelField("IF",  EditorStyles.largeLabel);
            for (int i = 0; i < fuzzyRule.inputConnections.Count; i++)
            {
                RuleConnection input = fuzzyRule.inputConnections[i];
            
                Rect r = EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.LabelField(input.membership.mfName + " is " + input.membership.curveNames[input.index], EditorStyles.largeLabel);
                EditorGUILayout.EndHorizontal();

                if (i + 1 != fuzzyRule.inputConnections.Count)
                {
                    EditorGUILayout.LabelField(fuzzyRule.connection.ToString(), EditorStyles.largeLabel);
                }
            }

            EditorGUILayout.LabelField("THEN", EditorStyles.largeLabel);

            for (int i = 0; i < fuzzyRule.outputConnections.Count; i++)
            {
                RuleConnection output = fuzzyRule.outputConnections[i];
                Rect r = EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.LabelField(output.membership.mfName + " is " + output.membership.curveNames[output.index], EditorStyles.largeLabel);
                EditorGUILayout.EndHorizontal();

                if (i + 1 != fuzzyRule.outputConnections.Count)
                {
                    EditorGUILayout.LabelField("AND", EditorStyles.largeLabel);
                }
            }
            GUILayout.Space(20);
            if (GUILayout.Button("Change Rule"))
            {
                fuzzyRule.currentEditor = FuzzyRule.Display.setup;
            }
            
        }
        
        
    }
}
