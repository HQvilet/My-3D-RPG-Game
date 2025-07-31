using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(DialogueGraphAsset))]
public class DialogueGraphEditor : Editor
{
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int index)
    {
        Object asset = EditorUtility.InstanceIDToObject(instanceID);
        if (asset.GetType() == typeof(DialogueGraphEditor))
        {
            DialogueGraphEditorWindow.Open((DialogueGraphAsset)asset);
            return true;
        }
        return false;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Open"))
            DialogueGraphEditorWindow.Open((DialogueGraphAsset)target);

    }
}
