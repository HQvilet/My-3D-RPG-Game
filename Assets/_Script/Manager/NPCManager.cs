using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections.Editor.Search;
using UnityEngine;

namespace NPCSystem
{
    public class NPCManager : Singleton<NPCManager>
    {
        Dictionary<string, InteractableNPC> myDict_NPCs = new();

        public void AddWorldDataNPC(InteractableNPC npc)
        {
            myDict_NPCs.TryAdd(npc.Data.name, npc);
        }

        public InteractableNPC GetNPCWithName(string name)
        {
            return myDict_NPCs.GetValueOrDefault(name);
        }


    }
}
