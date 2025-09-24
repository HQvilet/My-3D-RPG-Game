using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
    [CreateAssetMenu(menuName = "Item/ItemData")]
    public class ItemData : ScriptableObject
    {
        
        [ReadOnly] [SerializeField] private string _id;
        public string ID => _id;
        public string Name;

        public Sprite Sprite;

        [TextArea(15,7)]
        public string Description;
        public bool IsStackable;

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(_id))
            {
                _id = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(this);
            }
#endif
        }
    }

    [CreateAssetMenu(menuName = "Item/Consumable Item")]
    public class ConsumableItem : ItemData
    {
        public ConsumEffect util;
    }

    [CreateAssetMenu(menuName = "Item/Weapon Item")]
    public class WeaponItem : ItemData
    {
        public WeaponRef weaponRef;
        public BasicStatsConfig stats;
        
        protected override void OnValidate() 
        {
            base.OnValidate();
            IsStackable = false;
        }
    }



    public enum Armour
    {
        // ARMOUR NAME FOR SPECIFIC ONES
    }

    [CreateAssetMenu(menuName = "Item/ArmourItem")]
    public class ArmourItem : ItemData
    {
        public ArmourType armourType;

        protected override void OnValidate() 
        {
            base.OnValidate();
            IsStackable = false;
        }
    }
}
