using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DialogueGraph
{
    [CreateAssetMenu(menuName = "Graph View/New Dialogue Asset")]
    public class DialogueGraphAsset : ScriptableObject
    {
        [SerializeReference] public List<GraphNodeData> nodes;
        Dictionary<string, GraphNodeData> myDict_nodes = new();
        [SerializeReference] public GraphNodeData startNode;
        // public DialogueGraphNode currentNode;

        public Action OnStartDialogueCallback;
        public Action OnEndDialogueCallback;

        void OnEnable()
        {
            if (nodes == null)
                return;
            foreach (var node in nodes)
            {
                myDict_nodes.Add(node.ID, node);
            }
                
        }

        public GraphNodeData MoveToNextNode(GraphNodeData currentNode, int index = 0)
        {

            if (currentNode == null)
            {
                OnStartDialogueCallback?.Invoke();
                // currentNode = startNode;
                return startNode;
            }
            string next = currentNode.GetNextGraphID(index);
            if (string.IsNullOrEmpty(next) || !myDict_nodes.ContainsKey(next))
            {
                return null;
            }
            return myDict_nodes[next];

        }
    }
}