using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DialogueTriggerInteract : MonoBehaviour, IInteractAction
{
    [SerializeField] DialogueContext context;
    public void OnInteract()
    {
        Dialogue.Instance.Activate(context);
    }
}