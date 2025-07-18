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


    public class BaseQuest : MonoBehaviour, IBaseQuest
    {

        public int ID;
        public string Title;

        public List<QuestSegment> questSegments;

        public QuestState CurrentState;
        public QuestSegment CurrentQuest;
        public int CurrentQuestIndex = -1;

        public void Start()
        {
            OnStartQuest();
        }

        public void MoveToNextQuest()
        {
            
            CurrentQuestIndex++;
            if(CurrentQuestIndex < questSegments.Count())
            {
                CurrentQuest = questSegments[CurrentQuestIndex];
                CurrentQuest.OnStartQuestSegment();
            }
            else
            {
                OnFinishedQuest();
            }
        }

        public virtual void OnStartQuest()
        {
            MoveToNextQuest();
        }

        public void OnInteruptWhileDoingQuest()
        {

        }
        
        public virtual void OnFinishedQuest()
        {
            
        }

        void OnValidate()
        {
            ID = Title.GetHashCode();
        }
    }

}