

using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
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
}