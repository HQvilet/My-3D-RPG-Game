using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueType
{
    NONE,
    NORMAL,
    OPTIONAL
}

[Serializable]
public class DialogueGraphNode
{
    [SerializeField] string m_guid;
    public string ID => m_guid;
    [SerializeField] string typeName;
    public string TypeName
    {
        get => typeName;
        set => typeName = value;
    }

    [SerializeField] Rect rect;
    public Rect Rect
    {
        get => rect;
        set => rect = value;
    }

    public DialogueGraphNode()
    {
        m_guid = Guid.NewGuid().ToString();
    }

    public virtual DialogueType GetNodeType() => DialogueType.NONE;
}
