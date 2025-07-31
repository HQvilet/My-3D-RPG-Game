using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public interface IInteractAction
{
    void OnInteract();
}

public class CutsceneTriggerInteract : MonoBehaviour, IInteractAction
{
    public PlayableDirector timeline;
    public void OnInteract()
    {
        timeline.Play();
    }
}

// public class DialogueTriggerInteract : MonoBehaviour, IInteractAction
// {

// }

// public class QuestTriggerInteract : MonoBehaviour, IInteractAction
// {
    
// }