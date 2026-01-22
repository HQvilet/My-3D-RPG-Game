
using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
    [CreateAssetMenu(menuName = "Item/Weapon Abilities")]
    public class WeaponAbilityItem : ItemData
    {
        public WeaponType weaponType;
        public WeaponAbility ability;
    }
}