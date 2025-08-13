using System.Collections;
using System.Collections.Generic;
using NPCSystem;
using QuestSystem;
using TMPro;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    private IInteractAction interact;

    public TextMeshProUGUI nameTag;
    public NPCData Data;
    public bool HasInteracted = false;

    void Start()
    {
        nameTag.text = Data.characterName;
        interact = GetComponent<IInteractAction>();
        NPCManager.Instance.AddWorldDataNPC(this);
    }

    public void Interact()
    {
        // Dialogue.Instance.Activate(NPCContexts[0]);
        // HasInteracted = true;
        // // QuestDataTracking.Instance.NPCInteraction?.Invoke(Data.ID);

        // if (quest != null)
        // {
        //     QuestSystemManager.Instance.baseQuest.questSegments.Add(quest);
        // }
        interact.OnInteract();
    }

    public IInteractAction GetInteractAction() => interact;
}
