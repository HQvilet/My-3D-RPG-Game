using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AYellowpaper.SerializedCollections;
using QuestSystem;
using UnityEngine;

// Framework for basic quest segment
// [CreateAssetMenu(menuName = "Quest/New Quest Segment")]
[Serializable]
public class QuestSegment
{
    public string Title;
    public string Description;

    // public QuestRequirement Tasks;
    [Header("Collecting Item Quest")]
    public List<ItemCollectingQuest> ItemsToCollect;

    [Header("Collecting Object Quest")]
    public List<ObjectCollectingQuest> ObjectToCollect;

    public SerializedDictionary<string, bool> Tasks;

    public Action OnFinishedCallback;
    public void AddItem(string obj, int amount)
    {
        ObjectCollectingQuest item = ObjectToCollect.Find(o => o.Obj == obj);
        item.CurrentAmount += amount;
    }


    public bool HasFinished()
    {
        return (ObjectToCollect.All(o => o.MeetRequirement()) || ObjectToCollect.Count() == 0)
            && (ItemsToCollect.All(o => o.MeetRequirement()) || ItemsToCollect.Count() == 0)
            && (Tasks.All(o => o.Value) || Tasks.Count() == 0);
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
        Debug.Log("Quest Segment completed");
        OnFinishedCallback?.Invoke();
        Bus<OnCollectItemEvent>.RemoveRegister(OnCollectedItem);
    }

}
