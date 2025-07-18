using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ItemSystem.ItemConfiguration;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ItemCollectingQuest
{

    public ItemData Item;
    public int RequiredAmount;
    public int CurrentAmount;

    public bool MeetRequirement() => CurrentAmount >= RequiredAmount;
}

[System.Serializable]
public class ObjectCollectingQuest
{

    public string Obj;
    public int RequiredAmount;
    public int CurrentAmount;

    public bool MeetRequirement() => CurrentAmount >= RequiredAmount;
}

[System.Serializable]
public class MoveToPositionQuest
{
    public Vector3 TargetPosition;
}


// [CreateAssetMenu(menuName = "Quest/New Quest Task")]
// public class QuestRequirement : ScriptableObject
// {

//     [Header("Collecting Item Quest")]
//     public List<ItemCollectingQuest> ItemsRequirement;

//     // Move to specific position
//     [Header("Move To Quest")]
//     public List<MoveToPositionQuest> TargetQuestPositions;

//     [Header("Collecting Item Quest")]
//     public List<ObjectCollectingQuest> ObjectToCollect;

//     // Interact with NPC

//     //... other event trigger

//     // public void AddItem(int ItemID ,int amount)
//     // {
//     //     ItemCollectingQuest item = ItemsRequirement.Find(o => o.Item.ID == ItemID);
//     //     item.CurrentAmount += amount;
//     // }

//     public bool IsFinished()
//     {
//         return ItemsRequirement.All(o => o.MeetRequirement()) || ItemsRequirement.Count() == 0;
//     }

// }
