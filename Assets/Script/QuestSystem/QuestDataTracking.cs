using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;



namespace QuestSystem
{
    public class QuestDataTracking : Singleton<QuestDataTracking>
    {

        public Action<string ,int> ObjectCollectEvent;
        public Action<int ,int> ItemCollectEvent;
        public Action<string> QuestEvent;


        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                QuestDataTracking.Instance.ObjectCollectEvent?.Invoke("ObjectName1" ,3);
        }

    }
}