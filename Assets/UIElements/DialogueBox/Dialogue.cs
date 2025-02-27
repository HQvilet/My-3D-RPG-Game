using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : Singleton<Dialogue>
{

    public DialogueBox dialogue;
    public void Activate(DialogueContext context)
    {
        dialogue.SetContext(context);
        dialogue.Activate();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)) dialogue.Activate();
            

    }
}
