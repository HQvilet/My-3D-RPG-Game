using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    DialogueGraphAsset m_asset;
    SerializedObject m_serializedObject;
    
    Dictionary<string, DialogueGraphEditorNode> myDict_editorNodes;

    public DialogueGraphEditorWindow window;
    DialogueGraphWindowSearchProvider m_searchProvider;

    public DialogueGraphView(SerializedObject o, DialogueGraphEditorWindow window)
    {
        myDict_editorNodes = new();

        this.m_serializedObject = o;
        this.m_asset = (DialogueGraphAsset)m_serializedObject.targetObject;
        this.window = window;

        this.m_searchProvider = ScriptableObject.CreateInstance<DialogueGraphWindowSearchProvider>();
        this.m_searchProvider.graphView = this;
        this.nodeCreationRequest += ShowSearchWindow;

        StyleSheet style = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/DialogueGraphNodeEditor/Editor/BackGround.uss");
        styleSheets.Add(style);

        GridBackground bg = new GridBackground();
        bg.name = "Grid";
        this.Add(bg);
        bg.SendToBack();

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new ClickSelector());
        this.AddManipulator(new ContentZoomer());

        DrawNodes();
        DrawConnections();

        graphViewChanged += OnGraphViewHasChange;
    }



    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> allPorts = new();
        List<Port> ports = new();

        foreach (var node in myDict_editorNodes.Values)
        {
            allPorts.AddRange(node._ports);
        }

        foreach (Port p in allPorts)
        {
            if (p == startPort) continue;
            if (p.node == startPort.node) continue;
            if (p.direction == startPort.direction) continue;
            ports.Add(p);
        }
        return ports;
    }

    private GraphViewChange OnGraphViewHasChange(GraphViewChange graphViewChange)
    {
        if (graphViewChange.movedElements != null)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Moved Nodes");
            foreach (DialogueGraphEditorNode element in graphViewChange.movedElements.OfType<DialogueGraphEditorNode>())
            {
                element.SavePosition();
            }
        }
        if (graphViewChange.elementsToRemove != null)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Removed Nodes");
            foreach (DialogueGraphEditorNode element in graphViewChange.elementsToRemove.OfType<DialogueGraphEditorNode>())
            {
                RemoveNode(element);
            }
            foreach (Edge element in graphViewChange.elementsToRemove.OfType<Edge>())
            {
                RemoveEdge(element);
            }
        }
        if (graphViewChange.edgesToCreate != null)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Added Connection");
            foreach (Edge edge in graphViewChange.edgesToCreate)
            {
                CreateConnection(edge);
            }
        }
        // graphViewChange.elementsToRemove.OfType<Edge>();
        return graphViewChange;
    }

    private void RemoveEdge(Edge edge)
    {
        var outputNode = (DialogueGraphEditorNode)edge.output.node;
        if (outputNode.GraphNode.GetNodeType() == DialogueType.NORMAL)
        {
            outputNode.GetGraphNodeData<NormalDialogueNode>().p_nexGraphID = string.Empty;
        }
        else
        {
            outputNode.GetGraphNodeData<OptionalDialogueNode>()._options[((OptionEditorNode)outputNode).GetPortIndex(edge.output)].p_nextGraphID = string.Empty;
        }
    }

    private void CreateConnection(Edge edge)
    {
        var outputNode = (DialogueGraphEditorNode)edge.output.node;
        var inputNode = (DialogueGraphEditorNode)edge.input.node;
        if (outputNode.GraphNode.GetNodeType() == DialogueType.NORMAL)
        {
            NormalEditorNode o_Node = (NormalEditorNode)outputNode;
            o_Node.m_nodeGraphData.SetNextGraph(inputNode.GraphNode);
        }
        else if (outputNode.GraphNode.GetNodeType() == DialogueType.OPTIONAL)
        {
            OptionEditorNode o_Node = (OptionEditorNode)outputNode;
            int connectIndex = o_Node._outputPorts.IndexOf(edge.output);
            o_Node.m_nodeGraphData._options[connectIndex].p_nextGraphID = inputNode.GraphNode.ID;
        }
    }

    private void RemoveNode(DialogueGraphEditorNode nodeToRemove)
    {
        
        m_asset.nodes.Remove(nodeToRemove.GraphNode);
        myDict_editorNodes.Remove(nodeToRemove.GraphNode.ID);
        m_serializedObject.Update();
    }

    private void DrawNodes()
    {
        foreach (DialogueGraphNode node in m_asset.nodes)
        {
            AddNodeToGraph(node);
        }
    }

    private void DrawConnections()
    {
        foreach (DialogueGraphNode node in m_asset.nodes)
        {
            if (node.GetNodeType() == DialogueType.NORMAL)
            {
                string nextGraphNode = ((NormalDialogueNode)node).p_nexGraphID;
                if (string.IsNullOrEmpty(nextGraphNode))
                    continue;
                DrawConnectionLine(((NormalEditorNode)myDict_editorNodes[node.ID])._outputPort, myDict_editorNodes[((NormalDialogueNode)node).p_nexGraphID]._inputPort);
            }
            else if (node.GetNodeType() == DialogueType.OPTIONAL)
            {
                OptionEditorNode optionEditorNode = (OptionEditorNode)myDict_editorNodes[node.ID];
                OptionalDialogueNode optionNode = optionEditorNode.m_nodeGraphData;
                int i = 0;
                foreach (DialogueOption option in optionNode._options)
                {
                    if (string.IsNullOrEmpty(option.p_nextGraphID))
                        continue;
                    DrawConnectionLine(optionEditorNode._outputPorts[i], myDict_editorNodes[option.p_nextGraphID]._inputPort);
                    ++i;
                }
            }
        }
    }

    private void DrawConnectionLine(Port port_1, Port port_2)
    {
        Edge edgeToDraw = port_1.ConnectTo(port_2);
        AddElement(edgeToDraw);
    }

    private void ShowSearchWindow(NodeCreationContext obj)
    {
        m_searchProvider.target = (VisualElement)focusController.focusedElement;
        SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), m_searchProvider);
    }

    public void Add(DialogueGraphNode node)
    {
        Undo.RecordObject(m_serializedObject.targetObject, "Added Node");
        m_asset.nodes.Add(node);
        m_serializedObject.Update();

        AddNodeToGraph(node);
    }

    public void AddNodeToGraph(DialogueGraphNode node)
    {
        node.TypeName = node.GetType().AssemblyQualifiedName;

        DialogueGraphEditorNode editorNode;// = new DialogueGraphEditorNode(node);
        if (node.GetNodeType() == DialogueType.OPTIONAL)
        {
            editorNode = new OptionEditorNode(node);
        }
        else
        {
            editorNode = new NormalEditorNode(node);
        }
        editorNode.SetPosition(node.Rect);

        myDict_editorNodes.Add(node.ID, editorNode);

        AddElement(editorNode);
    }
}
