using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehaviour))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //setup
        CompositeBehaviour cb = (CompositeBehaviour)target;

        //check for behaviours
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.HelpBox("No behaviors in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviors", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false, GUILayout.MinWidth(60f));
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(cb);
        }

        if (GUILayout.Button("Add Behavior"))
        {
            AddBehaviour(cb);
            EditorUtility.SetDirty(cb);
        }

        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {
            if (GUILayout.Button("Remove Behavior"))
            {
                RemoveBehaviour(cb);
                EditorUtility.SetDirty(cb);
            }
        }
    }

    private void AddBehaviour(CompositeBehaviour cb)
    {
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }

    private void RemoveBehaviour(CompositeBehaviour cb)
    {
        int oldCount = cb.behaviours.Length;
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }

        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }
}
