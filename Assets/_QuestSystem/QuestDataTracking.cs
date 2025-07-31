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
        public Action<int> NPCInteraction;


        protected override void Awake()
        {
            base.Awake();

            ObjectCollectEvent += TestCollectEvent;
            ItemCollectEvent += TestItemCollectEvent;
            QuestEvent += TestQuestEvent;
            NPCInteraction += TestNPCInteractionEvent;

            Bus<OnCollectItemEvent>.AddRegister(CollectedItem);
        }

        private void CollectedItem(OnCollectItemEvent @event)
        {
            
        }

        private void TestNPCInteractionEvent(int obj)
        {
            Debug.Log("Player has interacted with " + obj);
        }

        private void TestCollectEvent(string arg1, int arg2)
        {
            Debug.Log("Player has collected " + arg1 + " x" + arg2);
        }

        private void TestQuestEvent(string obj)
        {
            Debug.Log("Player has finished " + obj);
        }

        private void TestItemCollectEvent(int arg1, int arg2)
        {
            Debug.Log("Player has collected " + arg1 + " x" + arg2);
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
                QuestDataTracking.Instance.ObjectCollectEvent?.Invoke("ObjectName1" ,3);
        }
    }
}