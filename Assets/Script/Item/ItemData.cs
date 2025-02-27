using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
    [CreateAssetMenu(menuName = "Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int ID;
        public string Name;

        public Sprite Sprite;

        [TextArea(15,10)]
        public string Description;
        public bool IsStackable;

        void OnValidate()
        {
            ID = Name.GetHashCode();
        }
    }

}
