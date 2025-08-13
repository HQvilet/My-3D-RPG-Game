using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlasticPipe.PlasticProtocol.Messages;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


namespace DialogueGraph.Editor
{
    public class DialogueGraphView : GraphView
    {
        DialogueGraphAsset m_asset;
        SerializedObject m_serializedObject;

        Dictionary<string, DialogueGraphEditorNode> myDict_editorNodes;

        public DialogueGraphEditorWindow window;
        DialogueGraphWindowSearchProvider m_searchProvider;

        List<Edge> s_edges = new();

        public DialogueGraphView(SerializedObject obj, DialogueGraphEditorWindow window)
        {
            myDict_editorNodes = new();

            this.m_serializedObject = obj;
            this.m_asset = (DialogueGraphAsset)obj.targetObject;
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
            // this.AddManipulator(new ContextualMenuManipulator(BuildContextualMenu));

            DrawNodes();
            DrawConnections();

            graphViewChanged += OnGraphViewHasChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            base.BuildContextualMenu(evt);
            if (evt.target is Node)
            {
                evt.menu.AppendAction(
                "Set Node as Start Node",
                null,
                DropdownMenuAction.AlwaysEnabled);

                m_asset.startNode = ((DialogueGraphEditorNode)evt.target).GraphNode;
            }

        }


        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            List<Port> allPorts = new();
            List<Port> ports = new();

            foreach (var node in myDict_editorNodes.Values)
            {
                allPorts.AddRange(node.GetAllPorts());
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
                    ClearAllEdges();
                    DrawConnections();
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
                outputNode.GetGraphNodeData<DialogueNode>().p_nextGraphID = string.Empty;
            }
            else if (outputNode.GraphNode.GetNodeType() == DialogueType.EVENT)
            {
                outputNode.GetGraphNodeData<EventNode>().p_nextGraphID = string.Empty;
            }
            else
            {
                outputNode.GetGraphNodeData<OptionalDialogueNode>()._options[((OptionEditorNode)outputNode).GetPortIndex(edge.output)].p_nextGraphID = string.Empty;
            }
            m_serializedObject.Update();
            m_serializedObject.ApplyModifiedProperties();
            
        }

        private void CreateConnection(Edge edge)
        {
            s_edges.Add(edge);
            var outputNode = (DialogueGraphEditorNode)edge.output.node;
            var inputNode = (DialogueGraphEditorNode)edge.input.node;
            outputNode.ConnectTo(inputNode, edge.output);
            // if (outputNode.GraphNode.GetNodeType() == DialogueType.NORMAL)
            // {
            //     NormalEditorNode o_Node = (NormalEditorNode)outputNode;
            //     o_Node.m_nodeGraphData.SetNextGraph(inputNode.GraphNode);
            // }
            // else if (outputNode.GraphNode.GetNodeType() == DialogueType.EVENT)
            // {
            //     NormalEditorNode o_Node = (NormalEditorNode)outputNode;
            //     o_Node.m_nodeGraphData.SetNextGraph(inputNode.GraphNode);
            // }
            // else if (outputNode.GraphNode.GetNodeType() == DialogueType.OPTIONAL)
            // {
            //     OptionEditorNode o_Node = (OptionEditorNode)outputNode;
            //     int connectIndex = o_Node.GetPortIndex(edge.output);
            //     o_Node.m_nodeGraphData._options[connectIndex].p_nextGraphID = inputNode.GraphNode.ID;
            // }
            m_serializedObject.Update();
            m_serializedObject.ApplyModifiedProperties();
            
        }

        private void RemoveNode(DialogueGraphEditorNode nodeToRemove)
        {

            m_asset.nodes.Remove(nodeToRemove.GraphNode);
            myDict_editorNodes.Remove(nodeToRemove.GraphNode.ID);
            m_serializedObject.Update();
            m_serializedObject.ApplyModifiedProperties();
            

        }

        private void ClearAllEdges()
        {
            foreach (Edge edge in s_edges)
            {
                RemoveElement(edge);
            }
            s_edges.Clear();
        }

        private void DrawNodes()
        {
            if (m_asset.nodes == null)
                m_asset.nodes = new();
            foreach (GraphNodeData node in m_asset.nodes)
            {
                AddNodeToGraph(node);
            }
        }

        private void DrawConnections()
        {
            foreach (GraphNodeData node in m_asset.nodes)
            {
                if (node.GetNodeType() == DialogueType.NORMAL)
                {
                    string nextGraphNodeID = ((DialogueNode)node).p_nextGraphID;
                    if (string.IsNullOrEmpty(nextGraphNodeID))
                        continue;
                    if (!myDict_editorNodes.ContainsKey(nextGraphNodeID))
                    {
                        myDict_editorNodes[node.ID].GetGraphNodeData<DialogueNode>().p_nextGraphID = string.Empty;
                        continue;
                    }

                    DrawConnectionLine(myDict_editorNodes[node.ID].GetOutputPort(), myDict_editorNodes[nextGraphNodeID].GetInputPort());
                }
                else if (node.GetNodeType() == DialogueType.EVENT)
                {
                    string nextGraphNodeID = ((EventNode)node).p_nextGraphID;
                    if (string.IsNullOrEmpty(nextGraphNodeID))
                        continue;
                    if (!myDict_editorNodes.ContainsKey(nextGraphNodeID))
                    {
                        myDict_editorNodes[node.ID].GetGraphNodeData<EventNode>().p_nextGraphID = string.Empty;
                        continue;
                    }

                    DrawConnectionLine(myDict_editorNodes[node.ID].GetOutputPort(), myDict_editorNodes[nextGraphNodeID].GetInputPort());
                }
                else if (node.GetNodeType() == DialogueType.OPTIONAL)
                {
                    OptionEditorNode optionEditorNode = (OptionEditorNode)myDict_editorNodes[node.ID];
                    OptionalDialogueNode optionNode = optionEditorNode.GetGraphNodeData<OptionalDialogueNode>();
                    // int i = 0;
                    // foreach (DialogueOption option in optionNode._options)
                    // {
                    //     if (string.IsNullOrEmpty(option.p_nextGraphID))
                    //         continue;
                    //     if (!myDict_editorNodes.ContainsKey(option.p_nextGraphID))
                    //         continue;
                    //     DrawConnectionLine(optionEditorNode.GetOutputPort(i), myDict_editorNodes[option.p_nextGraphID].GetInputPort());
                    //     ++i;
                    // }
                    for (int i = 0; i < optionNode._optionCount; ++i)
                    {
                        var option = optionNode._options[i];
                        if (string.IsNullOrEmpty(option.p_nextGraphID))
                            continue;
                        if (!myDict_editorNodes.ContainsKey(option.p_nextGraphID))
                        {
                            option.p_nextGraphID = string.Empty;
                            continue;
                        }

                        DrawConnectionLine(optionEditorNode.GetOutputPort(i), myDict_editorNodes[option.p_nextGraphID].GetInputPort());
                    }
                }
            }
            m_serializedObject.Update();
            m_serializedObject.ApplyModifiedProperties();
            
        }

        private void DrawConnectionLine(Port port_1, Port port_2)
        {
            Edge edgeToDraw = port_1.ConnectTo(port_2);
            s_edges.Add(edgeToDraw);
            AddElement(edgeToDraw);
        }

        private void ShowSearchWindow(NodeCreationContext obj)
        {
            m_searchProvider.target = (VisualElement)focusController.focusedElement;
            SearchWindow.Open(new SearchWindowContext(obj.screenMousePosition), m_searchProvider);
        }

        public void Add(GraphNodeData node)
        {
            Undo.RecordObject(m_serializedObject.targetObject, "Added Node");
            m_serializedObject.Update();
            m_asset.nodes.Add(node);
            m_serializedObject.ApplyModifiedProperties();
            AddNodeToGraph(node);
        }

        public void AddNodeToGraph(GraphNodeData node)
        {
            node.TypeName = node.GetType().AssemblyQualifiedName;

            DialogueGraphEditorNode editorNode;// = new DialogueGraphEditorNode(node);
            if (node.GetNodeType() == DialogueType.OPTIONAL)
            {
                editorNode = new OptionEditorNode(node);
            }
            else if (node.GetNodeType() == DialogueType.NORMAL)
            {
                editorNode = new NormalEditorNode(node);
            }
            else if (node.GetNodeType() == DialogueType.EVENT)
            {
                editorNode = new EventEditorNode(node);
            }
            else
            {
                return;
            }
            // Debug.Log(Activator.CreateInstance(node.TypeName))
            editorNode.SetPosition(node.Rect);

            myDict_editorNodes.Add(node.ID, editorNode);

            AddElement(editorNode);
        }
    }
}