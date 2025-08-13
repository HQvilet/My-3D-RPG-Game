using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueGraph;
using UnityEngine.Playables;


public class DialogueTriggerInteract : MonoBehaviour, IInteractAction
{
    // [SerializeField] DialogueContext context;
    public StaticDialogueBox box;
    [SerializeField] DialogueGraphAsset dialogueAsset;
    // GraphNodeData currentNode;
    public void Start()
    {
        // dialogueAsset.OnStartDialogueCallback += () => { Debug.Log("Start dialogue"); };
        // dialogueAsset.OnEndDialogueCallback += () => { Debug.Log("End dialogue"); };
    }
    public void OnInteract()
    {
        box.Activate(dialogueAsset);
        // Dialogue.Instance.Activate(context);
        // Debug.Log(dialogueAsset.name);
        // currentNode = dialogueAsset.MoveToNextNode(currentNode);
        // Debug.Log(((NormalDialogueNode)currentNode)._content);

    }

    public void SetDialogueAsset(DialogueGraphAsset asset) => this.dialogueAsset = asset;

}