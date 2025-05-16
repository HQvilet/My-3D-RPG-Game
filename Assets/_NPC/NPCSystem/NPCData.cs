using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem
{

    [CreateAssetMenu(menuName = "NPC/NPC Data")]
    public class NPCData : ScriptableObject
    {
        public int ID;
        public string characterName;

    }
}