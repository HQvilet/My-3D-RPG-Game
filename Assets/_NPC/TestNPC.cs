using System.Collections;
using System.Collections.Generic;
using NPCSystem;
using UnityEngine;
using UnityEngine.AI;

public class TestNPC : MonoBehaviour ,IInteractable
{
    //Interactable NPC
    public NPCData Data;
    [SerializeField] private List<DialogueContext> NPCContext = new List<DialogueContext>();

    public void Interact(){ }
}
