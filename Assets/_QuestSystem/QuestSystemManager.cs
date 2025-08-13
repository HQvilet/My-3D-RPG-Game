using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestSystem
{
    public class QuestSystemManager : Singleton<QuestSystemManager>
    {
        public List<BaseQuest> quests;
        public QuestSystemUI questUI;

        public BaseQuest currentQuest;

        public void AddQuest(BaseQuest quest)
        {
            quests.Add(quest);
            // questUI.AddQuest();
        }

        public void AddPriorityQuest(BaseQuest quest)
        {

        }

    }

}

