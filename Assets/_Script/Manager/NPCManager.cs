using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem
{
    public class NPCManager : MonoBehaviour
    {
        Dictionary<string, InteractableEventEmitter> myDict_NPCs = new();

        public void AddWorldDataNPC(InteractableEventEmitter npc)
        {
            // myDict_NPCs.TryAdd(npc.Data.name, npc);
        }

        public InteractableEventEmitter GetNPCWithName(string name)
        {
            return myDict_NPCs.GetValueOrDefault(name);
        }


    }
}
