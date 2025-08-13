using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueGraph
{
    public enum DialogueType
    {
        NONE,
        NORMAL,
        OPTIONAL,
        EVENT,
    }

    [Serializable]
    public class GraphNodeData
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

        public GraphNodeData()
        {
            m_guid = Guid.NewGuid().ToString();
        }

        // public virtual 
        public virtual DialogueType GetNodeType() => DialogueType.NONE;
        public virtual string GetNextGraphID(int index = 0) => null;
    }
}