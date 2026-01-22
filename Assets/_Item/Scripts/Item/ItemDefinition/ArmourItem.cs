using UnityEngine;

namespace ItemSystem.ItemConfiguration
{
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