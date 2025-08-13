using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace DialogueGraph.Editor
{
    [CustomEditor(typeof(DialogueGraphAsset))]
    public class DialogueGraphEditor : UnityEditor.Editor
    {
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int index)
        {
            Object asset = EditorUtility.InstanceIDToObject(instanceID);
            if (asset == null)
                return false;
            if (asset.GetType() == typeof(DialogueGraphAsset))
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
}