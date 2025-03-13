using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class DialogueBehaviour : PlayableBehaviour
{
    public string context;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        DynamicDialogueBox dialogueBox = playerData as DynamicDialogueBox;

        dialogueBox.SetText(context);

    }
}

public class DialogueTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        DynamicDialogueBox dialogueBox = playerData as DynamicDialogueBox;

        float currentWeight = 0f;

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if(inputWeight > 0)
            {
                ScriptPlayable<DialogueBehaviour> inputPlayable = (ScriptPlayable<DialogueBehaviour>)playable.GetInput(i);
                // inputPlayable.GetDuration();
                DialogueBehaviour input = inputPlayable.GetBehaviour();

                currentWeight = inputWeight;
                dialogueBox.SetText(input.context);
            }
        }
        dialogueBox.SetWeight(currentWeight);
    }
}