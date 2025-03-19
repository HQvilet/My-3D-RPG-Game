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

    [CreateAssetMenu(menuName = "Item/WeaponItem")]
    public class WeaponItem : ItemData
    {
        public WeaponRef weaponRef;
    }


    public enum ArmourType
    {
        HEAD_ARMOUR ,
        ARM_ARMOUR ,
        LEG_ARMOUR ,
        ARTIFACT
    }

    [CreateAssetMenu(menuName = "Item/ArmourItem")]
    public class ArmourItem : ItemData
    {
        public ArmourType armourType;
        // public Stats
    }


}
