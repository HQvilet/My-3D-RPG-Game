using System;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;

public interface IInventorySlot
{
    // public ItemData ItemData{get; set;}
    public int Amount{get; set;}

    public bool IsFull();
    public bool IsEmpty();

    public ItemData GetItemData();
    public SlotType GetSlotType();

}