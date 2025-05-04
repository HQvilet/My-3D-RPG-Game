using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem
{

    public struct NPCDataConfig
    {
        public NPCData Info;
        public GameObject WorldInfo;

    }
    public class NPCWorldData
    {
        Dictionary<int ,NPCDataConfig> AllNPCData = new Dictionary<int, NPCDataConfig>();
        
        
        void LoadAllNPC()
        {
            
        }
    }
}