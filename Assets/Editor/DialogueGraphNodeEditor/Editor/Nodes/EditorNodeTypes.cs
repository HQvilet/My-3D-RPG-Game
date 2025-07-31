using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class NormalEditorNode : DialogueGraphEditorNode
{
    public NormalDialogueNode m_nodeGraphData;
    TextField _nameField;
    TextField _contentField;
    public Port _outputPort;

    public NormalEditorNode(DialogueGraphNode node) : base(node)
    {
        m_nodeGraphData = (NormalDialogueNode)node;
        LoadNodeData();
    }

    protected override void CreateNodeVisual()
    {
        base.CreateNodeVisual();
        this.Q("OptionContentContainer").style.display = DisplayStyle.None;
        this.Q("OutputPort").Add(_outputPort = CreateFlowOutputPort("output"));

        _nameField = this.Q<TextField>("Name");
        _contentField = this.Q<TextField>("Content");
    }

    protected override void AddEventListener()
    {
        _nameField.RegisterValueChangedCallback(evt => { m_nodeGraphData._name = evt.newValue; });
        _contentField.RegisterValueChangedCallback(evt => { m_nodeGraphData._content = evt.newValue; });
    }

    protected override void LoadNodeData()
    {
        _nameField.value = m_nodeGraphData._name;
        _contentField.value = m_nodeGraphData._content;
    }

    public override Port GetOutputPort(int index = 0) => _outputPort;
}

public class OptionNode : VisualElement
{
    protected VisualTreeAsset visualOptionNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/OptionNodeVisual.uxml");
    DialogueOption data;
    public OptionNode(DialogueOption data, Port outputPort)
    {
        this.data = data;
        Add(visualOptionNodeTreeAsset.Instantiate());
        var a = this.Q<TextField>("OptionContentTextField");
        a.value = data._optionContent;
        a.RegisterValueChangedCallback(evt => { data._optionContent = evt.newValue; });
        this.Q("OutputPortContainer").Add(outputPort);

    }
}

public class OptionEditorNode : DialogueGraphEditorNode
{
    public OptionalDialogueNode m_nodeGraphData;
    public List<Port> _outputPorts = new();
    TextField _nameField;
    TextField _contentField;
    IntegerField _optionNumberField;

    public OptionEditorNode(DialogueGraphNode node) : base(node)
    {
        m_nodeGraphData = (OptionalDialogueNode)node;
        LoadNodeData();
    }

    protected override void CreateNodeVisual()
    {
        base.CreateNodeVisual();
        
        opt = this.Q("OptionContainer");

        _nameField = this.Q<TextField>("Name");
        _contentField = this.Q<TextField>("Content");
        _optionNumberField = m_contentContainer.Q<IntegerField>("OptionNumberField");//.RegisterValueChangedCallback(OnOptionDataChange);
    }

    protected override void AddEventListener()
    {
        _nameField.RegisterValueChangedCallback(evt => { m_nodeGraphData._name = evt.newValue; });
        _contentField.RegisterValueChangedCallback(evt => { m_nodeGraphData._content = evt.newValue; });
        _optionNumberField.RegisterValueChangedCallback(OnOptionNumberChange);
    }

    protected override void LoadNodeData()
    {
        _nameField.value = m_nodeGraphData._name;
        _contentField.value = m_nodeGraphData._content;
        _optionNumberField.value = m_nodeGraphData._optionCount;
        LoadOptionData(m_nodeGraphData._optionCount);

    }

    private void LoadOptionData(int value)
    {
        // opt.Clear();
        // _outputPorts.Clear();
        for (int i = 0; i < value; ++i)
        {
            // VisualElement n_optionNode = visualOptionNodeTreeAsset.Instantiate();
            // // n_optionNode.Q<TextField>("OptionContent").value = ;
            // Port outputPort = CreateFlowOutputPort("output");
            // _outputPorts.Add(outputPort);
            // n_optionNode.Q("OutputPortContainer").Add(outputPort);

            // opt.Add(n_optionNode);
            // VisualElement n_optionNode = visualOptionNodeTreeAsset.Instantiate();
            Port outputPort = CreateFlowOutputPort("output");
            OptionNode n_optionNode = new OptionNode(m_nodeGraphData._options[i], outputPort);
            
            // n_optionNode.Q("OutputPortContainer").Add(outputPort);
            opt.Add(n_optionNode);
            _outputPorts.Add(outputPort);
        }
    }

    VisualElement opt;
    private void OnOptionNumberChange(ChangeEvent<int> evt)
    {
        if (evt.newValue > 10 && evt.newValue >= 0) return;

        if (evt.newValue > m_nodeGraphData._optionCount)
        {

            // {
            //     VisualElement n_optionNode = visualOptionNodeTreeAsset.Instantiate();
            //     Port outputPort = CreateFlowOutputPort("output");
            //     n_optionNode.Q("OutputPortContainer").Add(outputPort);
            //     opt.Add(n_optionNode);
            //     _outputPorts.Add(outputPort);
            //     m_nodeGraphData._options.Add(new DialogueOption());
            // }
            for (int i = m_nodeGraphData._optionCount; i < evt.newValue; ++i)
                m_nodeGraphData._options.Add(new DialogueOption());
            LoadOptionData(evt.newValue - m_nodeGraphData._optionCount);

        }
        else
        {
            for (int i = m_nodeGraphData._optionCount - 1; i >= evt.newValue; --i)
            {
                Debug.Log(i);
                opt.RemoveAt(i);
                _outputPorts.RemoveAt(i);
                m_nodeGraphData._options.RemoveAt(i);
            }
        }

        m_nodeGraphData._optionCount = evt.newValue;
        for (int i = _ports.Count - 1; i >= 1; --i)
        {
            _ports.RemoveAt(i);
        }
    }

    public override Port GetOutputPort(int index = 0) => _outputPorts[index];
    public int GetPortIndex(Port port) => _outputPorts.IndexOf(port);
}
