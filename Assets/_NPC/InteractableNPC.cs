using System.Collections;
using System.Collections.Generic;
using NPCSystem;
using QuestSystem;
using TMPro;
using UnityEngine;

public class InteractableNPC : MonoBehaviour ,IInteractable
{

    public TextMeshProUGUI nameTag;
    public NPCData Data;
    public bool HasInteracted = false;
    [SerializeField] private List<DialogueContext> NPCContexts = new List<DialogueContext>();

    void Awake()
    {
        nameTag.text = Data.characterName;
    }

    public void Interact()
    {
        Dialogue.Instance.Activate(NPCContexts[0]);
        HasInteracted = true;
        QuestDataTracking.Instance.NPCInteraction?.Invoke(Data.ID);
    }
}
