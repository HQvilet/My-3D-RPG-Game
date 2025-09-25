using System.Collections;
using System.Collections.Generic;
using DialogueGraph;
using NPCSystem;
using QuestSystem;
using UnityEngine;


public interface IContentPackage
{
    void AddPackageToScene();
}

[CreateAssetMenu(menuName = "PackageContent/QuestPackage")]
public class QuestPackage : ScriptableObject, IContentPackage
{
    [SerializeField] BaseQuest priorityQuest;
    [SerializeField] BaseQuest quest;

    public void AddPackageToScene()
    {
        QuestSystemManager.Instance.AddQuest(quest);
    }
}


[System.Serializable]
public class DialogueFormat
{
    string name;
    DialogueGraphAsset asset;
}

// public struct
[CreateAssetMenu(menuName = "PackageContent/DialoguePackage")]
public class DialoguePackage : ScriptableObject, IContentPackage
{
    [SerializeField] string targetName;
    [SerializeField] DialogueGraphAsset asset;
    public void AddPackageToScene()
    {
        // MetaData.FindNPCWithName<T>("")
        var t = NPCManager.Instance.GetNPCWithName(targetName).GetInteractAction() as DialogueTriggerInteract;
        if (t == null)
            return;
        t.SetDialogueAsset(asset);
    }
}