using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueGraphEditorWindow : EditorWindow
{
    [SerializeField] DialogueGraphAsset m_asset;
    public DialogueGraphAsset Asset => m_asset;

    [SerializeField] SerializedObject m_serializedObject;
    [SerializeField] DialogueGraphView m_graphView;

    public static void Open(DialogueGraphAsset target)
    {
        DialogueGraphEditorWindow[] windows = Resources.FindObjectsOfTypeAll<DialogueGraphEditorWindow>();
        foreach (DialogueGraphEditorWindow o in windows)
        {
            if (o.m_asset == target)
            {
                o.Focus();
                return;
            }
        }
        DialogueGraphEditorWindow window = CreateWindow<DialogueGraphEditorWindow>(typeof(DialogueGraphEditorWindow), typeof(SceneView));
        window.titleContent = new GUIContent(target.name);//, EditorGUIUtility.ObjectContent(null, typeof(DialogueGraphAsset)).image);
        window.Load(target);
    }

    public void Load(DialogueGraphAsset asset)
    {
        m_asset = asset;
        DrawGraph();
    }

    private void DrawGraph()
    {
        m_serializedObject = new SerializedObject(m_asset);
        m_graphView = new DialogueGraphView(m_serializedObject, this);
        rootVisualElement.Add(m_graphView);
    }
}
