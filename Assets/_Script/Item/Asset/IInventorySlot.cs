using System;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;


// Inventory interface for a slot in inventory
public interface IInventorySlot
{
    public int Amount { get; set; }

    public bool IsFull();
    public bool IsEmpty();

    public ItemData GetItemData();
    public SlotType GetSlotType();

}