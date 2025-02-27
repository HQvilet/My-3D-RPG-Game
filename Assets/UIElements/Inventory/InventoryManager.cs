using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;

public interface IInventoryUtilities
{
    public void AddItem(int itemID ,int amount);
    public void RemoveItemInSlot(int itemSlotIndex ,int amount);
    public void RemoveAllItemInSlot(int itemSlotIndex);
    public void ExchangeItem(int itemSlotIndex_1 ,int itemSlotIndex_2);
}

public class InventoryManager : Singleton<InventoryManager> ,IInventoryUtilities
{
    public InventoryData inventoryData;
    public InventoryUI inventoryUI;

    public void AddItem(int itemID ,int amount)
    {
        for(int i = 0; i < InventoryData.MAX_SLOT;i++)
        {
            ItemStack itemStack = inventoryData.Items[i];                
            
            if(itemStack.IsEmpty())
                    continue;
            if(itemStack.ItemData.ID == itemID)
            {


                if(itemStack.IsFull())
                    continue;
                else
                {
                    itemStack.Add(amount ,out int left);
                    inventoryUI.Slots[i].OnAmountChange();
                    if(left > 0)
                        AddItem(itemStack.ItemData.ID ,left);
                    return;
                }
            }
        }

        for(int i = 0; i < InventoryData.MAX_SLOT;i++)
        {
            ItemStack itemStack = inventoryData.Items[i];
            if(itemStack.IsEmpty())
            {
                itemStack.SetData(itemID ,amount);
                return;
            }
                
        }


    }

    public void AddItem(ItemData item ,int amount)
    {
        AddItem(item.ID ,amount);
    }

    public void RemoveItemInSlot(int itemSlotIndex ,int amount)
    {
        inventoryData.Items[itemSlotIndex].Remove(amount);
    }

    public void ExchangeItem(int itemSlotIndex_1 ,int itemSlotIndex_2)
    {
        ItemStack i_1 = inventoryData.Items[itemSlotIndex_1];
        ItemStack i_2 = inventoryData.Items[itemSlotIndex_2];

        ItemStack temp = i_1.Clone();
        i_1.Copy(i_2);
        i_2.Copy(temp);

    }

    public void RemoveAllItemInSlot(int itemSlotIndex)    
    {
        inventoryData.Items[itemSlotIndex].Amount = 0;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            RemoveAllItemInSlot(2);
    }

}
