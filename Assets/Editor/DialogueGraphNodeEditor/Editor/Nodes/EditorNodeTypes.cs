using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueGraph.Editor
{

    public class NormalEditorNode : DialogueGraphEditorNode
    {
        public DialogueNode m_nodeGraphData;
        TextField _nameField;
        TextField _contentField;
        public Port _outputPort;

        public NormalEditorNode(GraphNodeData node) : base(node)
        {
            m_nodeGraphData = (DialogueNode)node;
            LoadNodeData();
        }

        protected override void CreateNodeVisual()
        {
            
            m_contentContainer = visualNodeTreeAsset.Instantiate();
            this.Add(m_contentContainer);

            m_contentContainer.Q("OptionContentContainer").style.display = DisplayStyle.None;
            m_contentContainer.Q("OutputPort").Add(_outputPort = CreateFlowOutputPort("output"));

            _nameField = m_contentContainer.Q<TextField>("Name");
            _contentField = m_contentContainer.Q<TextField>("Content");
            base.CreateNodeVisual();
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
        public override List<Port> GetAllPorts()
        {
            return new List<Port>() { _inputPort, _outputPort };
        }
        public override string GetNextGraphID(int index = 0) => m_nodeGraphData.p_nextGraphID;
    }

    public class OptionNode : VisualElement
    {
        protected static VisualTreeAsset visualOptionNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/OptionNodeVisual.uxml");
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

        public OptionEditorNode(GraphNodeData node) : base(node)
        {
            m_nodeGraphData = (OptionalDialogueNode)node;
            LoadNodeData();
        }

        protected override void CreateNodeVisual()
        {
            
            m_contentContainer = visualNodeTreeAsset.Instantiate();
            this.Add(m_contentContainer);

            opt = this.Q("OptionContainer");

            _nameField = this.Q<TextField>("Name");
            _contentField = this.Q<TextField>("Content");
            _optionNumberField = m_contentContainer.Q<IntegerField>("OptionNumberField");//.RegisterValueChangedCallback(OnOptionDataChange);
            base.CreateNodeVisual();
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
        public override List<Port> GetAllPorts()
        {
            var t = new List<Port>(_outputPorts);
            t.Add(_inputPort);
            return t;
        }
        public override string GetNextGraphID(int index = 0) => m_nodeGraphData._options[index].p_nextGraphID;
    }

    public class EventEditorNode : DialogueGraphEditorNode
    {
        protected static VisualTreeAsset visualEventNodeTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/DialogueGraphNodeEditor/EventNodeVisual.uxml");
        EventNode m_nodeGraphData;
        TextField packageNameField;
        Port _outputPort;
        public EventEditorNode(GraphNodeData graphNode) : base(graphNode)
        {
            m_nodeGraphData = (EventNode)graphNode;
            LoadNodeData();
        }

        protected override void CreateNodeVisual()
        {
            
            m_contentContainer = visualEventNodeTreeAsset.Instantiate();
            this.Add(m_contentContainer);

            m_contentContainer.Q("OutputPortContainer").Add(_outputPort = CreateFlowOutputPort("output"));
            packageNameField = m_contentContainer.Q<TextField>("PackageNameField");
            base.CreateNodeVisual();

        }

        protected override void LoadNodeData()
        {
            packageNameField.value = m_nodeGraphData.assetToLoad;
        }

        protected override void AddEventListener()
        {
            packageNameField.RegisterValueChangedCallback(evt => { m_nodeGraphData.assetToLoad = evt.newValue; });
        }

        public override Port GetOutputPort(int index = 0) => _outputPort;
        public override List<Port> GetAllPorts()
        {
            return new List<Port>() { _inputPort, _outputPort };
        }
        public override string GetNextGraphID(int index = 0) => m_nodeGraphData.p_nextGraphID;
    }
}