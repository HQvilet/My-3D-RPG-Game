using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    public DialogueContext dialogueContext;
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject.name == "Player")
        {
            // Dialogue.Instance.Activate(dialogueContext);
            Debug.Log("Interact");
        }
    }
}
