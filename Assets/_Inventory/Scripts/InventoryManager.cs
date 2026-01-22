using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

// Inventory controller 
// Manage interfaces working with inventory
public class InventoryManager : Singleton<InventoryManager>
{
    public InventoryData inventoryData;
    public InventoryUI inventoryUI;
    

    #region Adding new item
    public bool TryAddItem(string itemID, int amount)
    {
        foreach (ItemSlotUnit itemSlot in inventoryData._inventoryItemSlots)
        {
            ItemStack itemStack = itemSlot.itemSlotData;

            if (itemStack.IsEmpty())
                continue;

            if (itemStack.ItemData.ID == itemID)
            {
                if (itemStack.IsFull())
                    continue;
                else
                {
                    itemStack.Add(amount, out int remaining);
                    itemSlot.OnAmountChange();
                    if (remaining > 0)
                        TryAddItem(itemStack.ItemData.ID, remaining);
                    return true;
                }
            }
        }

        foreach (ItemSlotUnit itemSlot in inventoryData._inventoryItemSlots)
        {
            ItemStack itemStack = itemSlot.itemSlotData;
            if (itemStack.IsEmpty())
            {
                itemStack.SetItemData(itemID, amount);
                return true;
            }
        }

        return false;
    }

    public bool TryAddItem(ItemData item, int amount)
    {
        return TryAddItem(item.ID, amount);
    }
    
    public bool TryAddWeaponAbility(WeaponAbilityItem abilityItem)
    {
        foreach (WeaponAbilitySlot itemSlot in inventoryData._weaponAbilitySlots)
        {
            if (itemSlot.IsEmpty())
            {
                itemSlot.abilityAsset.ItemData = abilityItem;
                // inventoryData.acquiredAbilities.Add(abilityItem);
                return true;
            }
        }
        return false;
    }

    public bool TryAddItem(ItemSlotUnit itemSlot, ItemData item, int amount)
    {
        return false;
    }

    public bool TryAddItemByCategories(ItemData item, int amount)
    {
        if(item is WeaponAbilityItem weaponAbilityItem)
            return TryAddWeaponAbility(weaponAbilityItem);
        else
            return TryAddItem(item, amount);
    }

    public void AddArmourItem(ArmourReference armour)
    {
        if (armour == null)
            return;

        ArmourReference _obj = ScriptableObject.CreateInstance(armour.GetType()) as ArmourReference;
        if (_obj == null)
            return;
        _obj.Set(armour);
        
        // foreach (ArmourSlotUnit itemSlot in inventoryData.ArmourSlots)
        // {
        //     if (itemSlot.armourAsset.IsEmpty())
        //     {
        //         itemSlot.armourAsset.SetArmourRef(_obj);
        //         break;
        //     }
        // }
    }
    #endregion

    #region Manipulate item
    public void ExchangeItem(ItemSlotUnit slot_1, ItemSlotUnit slot_2)
    {
        ItemStack temp = slot_1.itemSlotData;
        slot_1.itemSlotData = slot_2.itemSlotData;
        slot_2.itemSlotData = temp;
    }

    // public void ExchangeItem(ArmourSlotUnit slot_1, ArmourSlotUnit slot_2)
    // {
    //     ArmourAsset temp = slot_1.armourAsset;
    //     slot_1.armourAsset = slot_2.armourAsset;
    //     slot_2.armourAsset = temp;
    // }

    // public void EquipArmourItem(ArmourSlotUnit slotUnit, ArmourSlotEquipment equipmentUnit)
    // {
    //     if (equipmentUnit.TryEquipArmour(slotUnit.armourAsset))
    //     {
    //         ArmourAsset temp = slotUnit.armourAsset;
    //         slotUnit.armourAsset = equipmentUnit.armourAsset;
    //         equipmentUnit.armourAsset = temp;
    //     }
    // }

    public void AttachWeaponAbility(WeaponAbilitySlot abilitySlot, WeaponAbilitySlotEquipment attachSlot)
    {
        if(GameUIManager.Instance.weaponAbilityUIHandler.AddOrReplaceAbilityToWeapon(attachSlot.index, abilitySlot.abilityAsset, out AbilityAsset replacedAbilityAsset))
        {
            abilitySlot.abilityAsset = replacedAbilityAsset; 
        }
    }

    public void DettachWeaponAbility(WeaponAbilitySlot abilitySlot, WeaponAbilitySlotEquipment attachSlot)
    {
        if(abilitySlot.IsFull())
            return;
        if(GameUIManager.Instance.weaponAbilityUIHandler.RemoveAbilityFromWeapon(attachSlot.index, abilitySlot.abilityAsset, out AbilityAsset removedAbilityAsset))
        {
            abilitySlot.abilityAsset = removedAbilityAsset; 
        }
    }

    public void ExchangeWeaponAbility(WeaponAbilitySlot slot1, WeaponAbilitySlot slot2)
    {
        (slot1.abilityAsset, slot2.abilityAsset) = (slot2.abilityAsset, slot1.abilityAsset);
    }

    #endregion


    #region Delete items
    public bool TryRemoveItem(ItemSlotUnit itemSlot, int amount)
    {
        if(itemSlot.itemSlotData.IsEmpty())
            return false;
        
        itemSlot.itemSlotData.Remove(amount);
        return true;
    }
    
    public void RemoveArmour(ArmourSlotUnit slotUnit, ArmourSlotEquipment equipmentUnit)
    {
        if (equipmentUnit.TryRemoveArmour(out ArmourAsset removedAsset))
        {
            ArmourAsset temp = slotUnit.armourAsset;
            slotUnit.armourAsset = removedAsset;
            equipmentUnit.armourAsset = temp;
        }
    }

    public void RemoveAllItemInSlot(int itemSlotIndex)
    {
        inventoryData._inventoryItemSlots[itemSlotIndex].itemSlotData.Amount = 0;
    }

    #endregion
}
