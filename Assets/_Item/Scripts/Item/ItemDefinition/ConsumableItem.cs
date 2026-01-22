using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
    [CreateAssetMenu(menuName = "Item/Consumable Item")]
    public class ConsumableItem : ItemData
    {
        public ConsumEffect util;
    }
}