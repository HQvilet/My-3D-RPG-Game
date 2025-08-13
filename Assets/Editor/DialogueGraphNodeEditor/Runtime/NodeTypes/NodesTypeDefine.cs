using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueGraph
{
    [Serializable]
    public class DialogueOption
    {
        public string _optionContent;
        public string p_nextGraphID;
    }

    public abstract class NormalDialogueNode : GraphNodeData
    {
        public string _name;
        public string _content;
    }

    [NodeInfo("Normal Dialogue Node", "Process/Normal Dialogue Node")]
    public class DialogueNode : NormalDialogueNode
    {
        public string p_nextGraphID;
        public override DialogueType GetNodeType() => DialogueType.NORMAL;
        // public void SetNextGraph(GraphNodeData node)
        // {
        //     p_nextGraphID = node.ID;
        // }
        public override string GetNextGraphID(int index = 0) => p_nextGraphID;
    }

    [NodeInfo("Option Dialogue Node", "Process/Optional Dialogue Node")]
    public class OptionalDialogueNode : NormalDialogueNode
    {
        public OptionalDialogueNode()
        {
            _options = new(_optionCount);
        }
        // public string _name;
        // public string _content;
        public int _optionCount;
        public List<DialogueOption> _options;
        public override DialogueType GetNodeType() => DialogueType.OPTIONAL;
        public override string GetNextGraphID(int index = 0) => _options[index].p_nextGraphID;
    }

    [NodeInfo("Load Package Event Node", "Event/Load Package Node")]
    public class EventNode : GraphNodeData
    {
        public string assetToLoad;
        public string p_nextGraphID;
        public override DialogueType GetNodeType() => DialogueType.EVENT;
        public override string GetNextGraphID(int index = 0) => p_nextGraphID;
    }
}