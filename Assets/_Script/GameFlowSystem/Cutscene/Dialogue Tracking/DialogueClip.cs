using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueClip : PlayableAsset
{
    public string actorName;
    public string context;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogueBehaviour>.Create(graph);

        DialogueBehaviour dialogueBehaviour = playable.GetBehaviour();
        dialogueBehaviour.context = context;
        dialogueBehaviour.actor = actorName;

        return playable; 
    }
}
