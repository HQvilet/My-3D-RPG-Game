using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

[System.Serializable]
public class ArmourAsset : IInventorySlot
{
    public ArmourItem ItemData => armourRef.GetItemData();
    public ArmourReference armourRef = null;
    public int Amount
    {
        get => (IsFull()) ? 1 : 0; 
        set {}
    }

    public ItemData GetItemData() => ItemData;
    // public void SetItemData(ArmourItem item) => ItemData = item;

    public void SetArmourRef(ArmourReference @ref)
    {
        armourRef = @ref;
    }

    // public bool IsEmpty() => Amount <= 0 || ItemData == null;
    public bool IsEmpty() => armourRef == null;
    // public bool IsFull() => Amount != 0 && ItemData != null;
    public bool IsFull() => armourRef != null;

    public void Clear() => armourRef = null;

    public void GetDescription(){}

    public SlotType GetSlotType() => SlotType.ARMOUR_ITEM_SLOT;

    public ArmourAsset Clone(){return new ArmourAsset();}

    public void Copy(ArmourAsset other)
    {
        // ItemData = other.ItemData;
    }
}
