using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using UnityEngine;

// Framework for basic quest segment
[CreateAssetMenu(menuName = "Quest/New Quest Segment")]
public class QuestSegment : ScriptableObject
{
    public string Title;
    public string Description;

    // public QuestRequirement Tasks;
    [Header("Collecting Item Quest")]
    public List<ItemCollectingQuest> ItemsToCollect;

    // Move to specific position
    [Header("Move To Quest")]
    public List<MoveToPositionQuest> TargetQuestPositions;

    [Header("Collecting Item Quest")]
    public List<ObjectCollectingQuest> ObjectToCollect;

    public void AddItem(string obj ,int amount)
    {
        ObjectCollectingQuest item = ObjectToCollect.Find(o => o.Obj == obj);
        item.CurrentAmount += amount;
        Debug.Log(item.CurrentAmount);
    }

    public bool HasFinished()
    {
        return (ObjectToCollect.All(o => o.MeetRequirement()) || ObjectToCollect.Count() == 0)
            && (ItemsToCollect.All(o => o.MeetRequirement()) || ItemsToCollect.Count() == 0);
    }

    public void OnStartQuestSegment()
    {
        Bus<OnCollectItemEvent>.AddRegister(OnCollectedItem);
    }

    private void OnCollectedItem(OnCollectItemEvent @event)
    {
        foreach (ItemCollectingQuest itemQuest in ItemsToCollect)
        {
            if (itemQuest.Item.ID == @event.itemID)
            {
                itemQuest.CurrentAmount += @event.amount;
                break;
            }
        }
        CheckFinished();
    }

    private void CheckFinished()
    {
        if (HasFinished())
        {
            OnFinishQuestSegment();
        }
    }

    public void OnFinishQuestSegment()
    {
        Debug.Log("Quest complete");
        Bus<OnCollectItemEvent>.RemoveRegister(OnCollectedItem);
    }

}
