using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// [NodeInfo("Start", "Process/Start")]
// public class DialogueNode : DialogueGraphNode
// {
//     public string name;
//     public string context;
// }

// [NodeInfo("Debug", "Process/Debug")]
// public class DebugNode : DialogueGraphNode
// {
//     public string debug;
// }

// [NodeInfo("Test", "Test/Test")]
// public class TestNode : DialogueGraphNode
// {
//     public string say;
// }

[Serializable]
public class DialogueOption
{
    public string _optionContent;
    public string p_nextGraphID;
}

public abstract class BaseDialogueNode : DialogueGraphNode
{
    public string _name;
    public string _content;
}

[NodeInfo("Normal Dialogue Node", "Process/Normal Dialogue Node")]
public class NormalDialogueNode : DialogueGraphNode
{
    public string _name;
    public string _content;
    public string p_nexGraphID;
    public override DialogueType GetNodeType() => DialogueType.NORMAL;
    public void SetNextGraph(DialogueGraphNode node)
    {
        p_nexGraphID = node.ID;
    }
}

[NodeInfo("Option Dialogue Node", "Process/Optional Dialogue Node")]
public class OptionalDialogueNode : DialogueGraphNode
{
    public OptionalDialogueNode()
    {
        _options = new(_optionCount);
    }
    public string _name;
    public string _content;
    public int _optionCount;
    public List<DialogueOption> _options;
    public override DialogueType GetNodeType() => DialogueType.OPTIONAL;

}