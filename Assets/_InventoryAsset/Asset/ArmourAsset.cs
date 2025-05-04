using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

[System.Serializable]
public class ArmourAsset : IInventorySlot
{
    public ArmourItem ItemData;

    public int Amount
    { 
        get => (ItemData != null) ? 1 : 0; 
        set {}
    }

    [SerializeField] private UpgradeAsset upgradeInfo;

    public ItemData GetItemData() => ItemData;
    public void SetItemData(ArmourItem item) => ItemData = item;

    public bool IsEmpty() => Amount <= 0 || ItemData == null;
    public bool IsFull() => Amount != 0 && ItemData != null;

    public void Clear() => SetItemData(null);

    public void GetDescription(){}

    public SlotType GetSlotType() => SlotType.ARMOUR;

    public ArmourAsset Clone(){return new ArmourAsset();}

    public void Copy(ArmourAsset other)
    {
        ItemData = other.ItemData;

    }
}
