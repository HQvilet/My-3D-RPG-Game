using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueGraph.Editor
{
    public class DialogueGraphEditorNode : Node
    {
        protected static VisualTreeAsset visualNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/NodeVisual.uxml");
        // protected VisualTreeAsset visualOptionNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/OptionNodeVisual.uxml");

        protected VisualElement m_contentContainer;

        protected GraphNodeData m_graphNode;
        public GraphNodeData GraphNode => m_graphNode;

        public List<Port> _ports = new();
        public Port _inputPort;


        public DialogueGraphEditorNode(GraphNodeData graphNode)
        {
            this.AddToClassList("dialogue-graph-node");

            this.m_graphNode = graphNode;

            Type type = graphNode.GetType();
            NodeInfoAttribute info = type.GetCustomAttribute<NodeInfoAttribute>();
            title = info.Title;

            CreateNodeVisual();
            AddEventListener();
        }

        public T GetGraphNodeData<T>() where T : GraphNodeData => m_graphNode as T;


        protected virtual void CreateNodeVisual()
        {
            // create option content container
            this.Q("node-border").Remove(this.Q("contents"));
            this.Q("InputPort").Add(CreateFlowInputPort("input"));

        }

        protected virtual void LoadNodeData() { }
        protected virtual void AddEventListener() { }

        protected Port CreateFlowOutputPort(string name, string tooltip = "")
        {
            Port _outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(PortType.FlowPort));
            _outputPort.portName = name;
            _outputPort.tooltip = tooltip;
            _ports.Add(_outputPort);
            return _outputPort;
        }

        protected Port CreateFlowInputPort(string name, string tooltip = "")
        {
            _inputPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(PortType.FlowPort));
            _inputPort.portName = name;
            _inputPort.tooltip = tooltip;
            _ports.Add(_inputPort);
            return _inputPort;
        }

        public virtual Port GetInputPort() => _inputPort;
        public virtual Port GetOutputPort(int index = 0) => null;
        public virtual List<Port> GetAllPorts() => null;
        public virtual string GetNextGraphID(int index = 0) => null;

        public void ConnectTo(DialogueGraphEditorNode editorNode, Port port)
        {
            if (m_graphNode.GetNodeType() == DialogueType.NORMAL)
            {
                // NormalEditorNode o_Node = (NormalEditorNode)outputNode;
                // o_Node.m_nodeGraphData.SetNextGraph(inputNode.GraphNode);
                this.GetGraphNodeData<DialogueNode>().p_nextGraphID = editorNode.GraphNode.ID;
            }
            else if (m_graphNode.GetNodeType() == DialogueType.EVENT)
            {
                // NormalEditorNode o_Node = (NormalEditorNode)outputNode;
                // o_Node.m_nodeGraphData.SetNextGraph(inputNode.GraphNode);
                this.GetGraphNodeData<EventNode>().p_nextGraphID = editorNode.GraphNode.ID;
            }
            else if (m_graphNode.GetNodeType() == DialogueType.OPTIONAL)
            {
                // OptionEditorNode o_Node = (OptionEditorNode)outputNode;
                int connectIndex = ((OptionEditorNode)this).GetPortIndex(port);
                // o_Node.m_nodeGraphData._options[connectIndex].p_nextGraphID = inputNode.GraphNode.ID;
                this.GetGraphNodeData<OptionalDialogueNode>()._options[connectIndex].p_nextGraphID = editorNode.GraphNode.ID;
            }
        }


        public void SavePosition()
        {
            m_graphNode.Rect = GetPosition();
        }

    }
}

