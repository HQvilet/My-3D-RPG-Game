using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;

public interface IInventoryUtilities
{
    // public void AddItem(int itemID ,int amount);
    // public void RemoveItemInSlot(int itemSlotIndex ,int amount);
    // public void RemoveAllItemInSlot(int itemSlotIndex);
    // public void ExchangeItem(int itemSlotIndex_1 ,int itemSlotIndex_2);
}

public class InventoryManager : Singleton<InventoryManager> ,IInventoryUtilities
{
    public InventoryData inventoryData;
    public InventoryUI inventoryUI;

    public void AddItem(int itemID ,int amount)
    {
        // for(int i = 0; i < InventoryData.MAX_SLOT;i++)
        // {
        foreach(ItemSlotUnit itemSlot in inventoryData.ItemSlots)
        {
            ItemStack itemStack = itemSlot.itemSlotData;
            
            if(itemStack.IsEmpty())
                continue;
                
            if(itemStack.ItemData.ID == itemID)
            {
                if(itemStack.IsFull())
                    continue;
                else
                {
                    itemStack.Add(amount ,out int left);
                    itemSlot.OnAmountChange();
                    if(left > 0)
                        AddItem(itemStack.ItemData.ID ,left);
                    return;
                }
            }
        }

        foreach(ItemSlotUnit itemSlot in inventoryData.ItemSlots)
        {
            ItemStack itemStack = itemSlot.itemSlotData;
            if(itemStack.IsEmpty())
            {
                itemStack.SetItemData(itemID ,amount);
                return;
            }
        }
    }

    public void AddItem(ItemData item ,int amount)
    {
        AddItem(item.ID ,amount);
    }

    public void AddItem(ArmourItem item)
    {
        foreach(ArmourSlotUnit itemSlot in inventoryData.ArmourSlots)
        {    
            if(itemSlot.armourAsset.IsEmpty())
            {
                itemSlot.armourAsset.SetItemData(item);
                break;
            }
        }
    }

    public bool TryAddItem(ItemSlotUnit itemSlot ,ItemData item ,int amount)
    {
        // if(itemSlot.itemSlotData.IsEmpty())
        // {
        //     itemSlot.itemSlotData.SetItemData(item ,amount);
        //     return true;
        // }
        // if(itemSlot.itemSlotData.ItemData.ID == item.ID && !itemSlot.itemSlotData.IsFull())
        // {
        //     itemSlot.itemSlotData.Add(amount ,out int left);
        //     return true;
        // }
        return false;   
    }

    public void AddItemByCategories(ItemData item ,int amount)
    {
        ArmourItem armourItem = item as ArmourItem;

        if(armourItem != null)
        {
            AddItem(armourItem);
        }
        else
            AddItem(item ,amount);
    }

    // public void RemoveItemInSlot(int itemSlotIndex ,int amount)
    // {
    //     inventoryData.itemStacks[itemSlotIndex].Remove(amount);
    // }

    // public void ExchangeItem(int itemSlotIndex_1 ,int itemSlotIndex_2)
    // {
    //     ItemStack i_1 = inventoryData.itemStacks[itemSlotIndex_1];
    //     ItemStack i_2 = inventoryData.itemStacks[itemSlotIndex_2];

    //     ItemStack temp = i_1.Clone();
    //     i_1.Copy(i_2);
    //     i_2.Copy(temp);
    // }

    public void ExchangeItem(ItemSlotUnit slot_1 ,ItemSlotUnit slot_2)
    {
        ItemStack temp = slot_1.itemSlotData;
        slot_1.itemSlotData = slot_2.itemSlotData;
        slot_2.itemSlotData = temp;
    }

    // public void ExchangeItem(ArmourSlotEquipment slot_1 ,ItemSlotUnit slot_2)
    // {
    //     ItemStack i_1 = new ItemStack();
    //     i_1.SetItemData(slot_1.armourItem,1);
    //     ItemStack i_2 = slot_2.itemSlotData;

    //     ItemStack temp = i_1.Clone();
    //     i_1.Copy(i_2);
    //     i_2.Copy(temp);
    // }

    public void ExchangeItem(ArmourSlotUnit slot_1 ,ArmourSlotUnit slot_2)
    {
        ArmourAsset temp = slot_1.armourAsset;
        slot_1.armourAsset = slot_2.armourAsset;
        slot_2.armourAsset = temp;
    }

    public void EquipItem(ArmourSlotUnit slotUnit ,ArmourSlotEquipment equipmentUnit)
    {
        if(equipmentUnit.TryEquipArmour(slotUnit.armourAsset))
        {
            slotUnit.ClearSlot();
        }

    }

    public void RemoveAllItemInSlot(int itemSlotIndex)    
    {
        inventoryData.ItemSlots[itemSlotIndex].itemSlotData.Amount = 0;
    }
    
    // public bool HasItem(ItemData _item ,out ItemStack itemStack)
    // {
    //     itemStack = inventoryData.itemStacks.Find(item => item.ItemData == _item);
    //     return itemStack != null;
    // }
}
