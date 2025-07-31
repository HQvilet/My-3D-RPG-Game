using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace QuestSystem
{
    public interface IBaseQuest
    {
        public void OnStartQuest();
        public void OnFinishedQuest();
    }

    public enum QuestState
    {
        NOT_MEET_REQUIREMENT,
        FINISHED,

    }

    [CreateAssetMenu(menuName = "Quest/Quest")]
    public class BaseQuest : ScriptableObject, IBaseQuest
    {

        public int ID;
        public string Title;

        public List<QuestSegment> questSegments;

        public QuestState CurrentState;
        private QuestSegment CurrentQuest;
        public int CurrentQuestIndex = -1;

        public void MoveToNextQuest()
        {

            CurrentQuestIndex++;
            if (CurrentQuestIndex < questSegments.Count())
            {
                // if(CurrentQuest.OnFinishedCallback != null)
                //     CurrentQuest.OnFinishedCallback -= OnStartQuest;
                CurrentQuest = questSegments[CurrentQuestIndex];
                CurrentQuest.OnFinishedCallback += OnStartQuest;
                CurrentQuest.OnStartQuestSegment();
            }
            else
            {
                OnFinishedQuest();
            }
        }

        public virtual void OnStartQuest()
        {
            Debug.Log("You have just started quest : " + Title);
            MoveToNextQuest();
        }

        public void OnInteruptWhileDoingQuest()
        {

        }
        
        public virtual void OnFinishedQuest()
        {
            Debug.Log("You finshed all the tasks. :) ");
        }

        // void OnValidate()
        // {
        //     ID = Title.GetHashCode();
        // }
    }

}