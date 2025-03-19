using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

[System.Serializable]
public class ItemStack
{
    public ItemData ItemData;

    [SerializeField]private int _amount;
    public int Amount{
        get => _amount;
        set {
            if(value <= 0)
                ItemData = null;
            _amount = Mathf.Max(value ,0);
        }
    }
    public bool IsStackable{get => ItemData.IsStackable;}

    // [SerializeField] private int _maxStack = 64;
    public int MaxStack{
        get => IsStackable ? InventoryData.MAX_STACK : 1;
        // set{ _maxStack = value; }
    }

    public void SetData(int itemID,int amount)
    {
        ItemData = ItemPoolManager.Instance.GetItemByID(itemID);
        Amount = amount;
    }

    public void SetData(ItemData item,int amount)
    {
        ItemData = item;
        Amount = amount;
    }

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

    // public void Add(ItemData Item ,int amount ,out int left)
    // {
    //     left = 0;
    //     if(Item == this.ItemData)
    //         Add(amount ,out left);
    // }
    
    public void Remove(int amount)
    {
        Amount -= amount;
    }

    public ItemStack Clone()
    {
        var o = new ItemStack();
        o.SetData(ItemData.ID ,Amount);
        return o;
    }

    public void Copy(ItemStack other) => SetData(other.ItemData ,other.Amount);
    
}
