using System;
using System.Collections.Generic;
using UnityEngine;

namespace NPCSystem
{

    public struct RefModelPack
    {
        public Sprite DialogueSprite;
        // public MeshData
    }


    [CreateAssetMenu(menuName = "NPC/NPC Data")]
    public class NPCData : ScriptableObject
    {
        public int ID;
        public string characterName;

        public RefModelPack model;
    }
}