using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Dialogue/DialogueContext")]
public class DialogueContext : ScriptableObject
{

    public string CharacterName;
    public string optionScript;
    
    public List<string> contexts;

    public DialogueContext nextDialogue;
}
