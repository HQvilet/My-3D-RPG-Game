using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using UnityEngine;


[CreateAssetMenu(menuName = "Quest/New Quest Segment")]
public class QuestSegment : ScriptableObject
{
    public string Title;
    public string Description;

    // public QuestRequirement Tasks;
    [Header("Collecting Item Quest")]
    public List<ItemCollectingQuest> ItemsRequirement;

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
        return ObjectToCollect.All(o => o.MeetRequirement()) || ItemsRequirement.Count() == 0;
    }

    public void OnStartQuestSegment()
    {
        QuestDataTracking.Instance.ObjectCollectEvent += AddItem;
    }

    public void Update(){}

    public void OnFinishQuestSegment()
    {
        QuestDataTracking.Instance.ObjectCollectEvent -= AddItem;
    }

}
