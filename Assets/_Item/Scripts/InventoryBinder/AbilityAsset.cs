using ItemSystem.ItemConfiguration;
using UnityEngine;

[System.Serializable]
public class AbilityAsset : IInventorySlot
{
    public AbilityAsset(WeaponAbilityItem weaponAbilityItem = null)
    {
        ItemData = weaponAbilityItem;
    }

    public WeaponAbilityItem ItemData;

    public int Amount
    {
        get => IsFull() ? 1 : 0;
        set {}
    }

    public ItemData GetItemData() => ItemData;

    public SlotType GetSlotType() => SlotType.ABILITY_SLOT;

    public bool IsEmpty() => ItemData == null;

    public bool IsFull() => ItemData != null;

    public void Clear() => ItemData = null;
}