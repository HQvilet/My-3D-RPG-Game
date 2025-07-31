using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Graph View/New Dialogue Asset")]
public class DialogueGraphAsset : ScriptableObject
{
    [SerializeReference] public List<DialogueGraphNode> nodes;
}