using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Singleton<Dialogue>
{

    public StaticDialogueBox s_dialogue;
    public DynamicDialogueBox d_dialogue;
    public void Activate(DialogueContext context)
    {
        s_dialogue.SetContext(context);
        s_dialogue.Activate();
    }

}
