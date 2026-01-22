using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

[System.Serializable]
public class ItemStack : IInventorySlot
{
    public ItemData ItemData;

    [SerializeField] private int _amount;
    public int Amount{
        get => _amount;
        set {
            if(value <= 0)
                ItemData = null;
            _amount = Mathf.Max(value ,0);
        }
    }
    public bool IsStackable => ItemData.IsStackable;
    public int MaxStack => IsStackable ? InventoryData.MAX_STACK : 1;
    

    public void SetItemData(string itemID, int amount)
    {
        if (string.IsNullOrEmpty(itemID))
            return;
        ItemData = ItemIdentifyManager.Instance.GetItemByID(itemID);
        Amount = amount;
    }

    public void SetItemData(ItemData item, int amount)
    {
        ItemData = item;
        Amount = amount;
    }

    public ItemData GetItemData() => ItemData;

    public bool IsEmpty() => Amount <= 0 || ItemData == null;

    public bool IsFull() => Amount >= MaxStack;

    public void Add(int amount ,out int left)
    {
        Amount += amount;
        left = 0;
        if(IsFull())
        {
            left = Amount - MaxStack;
            Amount = MaxStack;
        }
    }

    public void GetDescription(){}
    
    public void Remove(int amount)
    {
        Amount -= amount;
    }

    public ItemStack Clone()
    {
        ItemStack _obj = new();
        _obj.SetItemData(ItemData.ID ,Amount);
        return _obj;
    }

    public void Copy(ItemStack other) => SetItemData(other.ItemData ,other.Amount);

    public SlotType GetSlotType() => SlotType.ITEM_SLOT;
    
}
