using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

[System.Serializable]
public class WeaponAsset// : IInventorySlot
{

    public WeaponItem ItemData;
    public bool IsStackable{get => false;}

    [SerializeField] private BasicStatsConfig upgradeStats;

    public ItemData GetItemData()
    {
        return null;
    }

    public bool IsEmpty()
    {
        return false;
    }

    public bool IsFull()
    {
        return true;
    }
}
