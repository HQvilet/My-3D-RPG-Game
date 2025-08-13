using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


namespace DialogueGraph.Editor
{
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
                if (o.Asset == target)
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

        void OnGUI()
        {
            if (m_asset != null)
                if (EditorUtility.IsDirty(m_asset))
                    this.hasUnsavedChanges = true;
                else
                    this.hasUnsavedChanges = false;   
        }

        private void DrawGraph()
        {
            m_serializedObject = new SerializedObject(m_asset);
            m_graphView = new DialogueGraphView(m_serializedObject, this);
            m_graphView.graphViewChanged += OnChanged;
            rootVisualElement.Add(m_graphView);
        }

        private GraphViewChange OnChanged(GraphViewChange graphViewChange)
        {
            this.hasUnsavedChanges = true;
            EditorUtility.SetDirty(m_asset);
            return graphViewChange;
        }
    }
}