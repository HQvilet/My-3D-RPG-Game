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

public class DialogueGraphEditorNode : Node
{
    protected VisualTreeAsset visualNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/NodeVisual.uxml");
    protected VisualTreeAsset visualOptionNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/OptionNodeVisual.uxml");

    protected VisualElement m_contentContainer;

    protected DialogueGraphNode m_graphNode;
    public DialogueGraphNode GraphNode => m_graphNode;

    public List<Port> _ports = new();
    public Port _inputPort;


    public DialogueGraphEditorNode(DialogueGraphNode graphNode)
    {
        this.AddToClassList("dialogue-graph-node");

        this.m_graphNode = graphNode;

        Type type = graphNode.GetType();
        NodeInfoAttribute info = type.GetCustomAttribute<NodeInfoAttribute>();
        title = info.Title;

        CreateNodeVisual();
        AddEventListener();
    }

    public T GetGraphNodeData<T>() where T : DialogueGraphNode => m_graphNode as T;   
    

    protected virtual void CreateNodeVisual()
    {
        // create option content container
        m_contentContainer = visualNodeTreeAsset.Instantiate();
        this.Add(m_contentContainer);

        this.Q("node-border").Remove(this.Q("contents"));
        this.Q("InputPort").Add(CreateFlowInputPort("input"));

    }

    protected virtual void LoadNodeData() { }
    protected virtual void AddEventListener() { }

    protected Port CreateFlowOutputPort(string name, string tooltip = "")
    {
        Port _outputPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(PortType.FlowPort));
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


    public void SavePosition()
    {
        m_graphNode.Rect = GetPosition();
    }

    public void SaveNodeData()
    {

    }
    

}
