using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorAttributes;
using GameSaveLoadSystem;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public static int MAX_STACK = 5;

    public List<ItemSlotUnit> _playerHotbarItemSlots;
    public List<ItemSlotUnit> _inventoryItemSlots;

    // public List<ArmourSlotUnit> ArmourSlots;
    public List<WeaponAbilitySlot> _weaponAbilitySlots;

    // public HashSet<WeaponAbilityItem> acquiredAbilities = new();

    void Awake()
    {
        _playerHotbarItemSlots = InventoryManager.Instance.inventoryUI.PlayerHotbarContainer.GetComponentsInChildren<ItemSlotUnit>().ToList();
        _inventoryItemSlots.AddRange(_playerHotbarItemSlots);
        _inventoryItemSlots.AddRange(InventoryManager.Instance.inventoryUI.InventoryContainer.GetComponentsInChildren<ItemSlotUnit>().ToList());
        _weaponAbilitySlots = InventoryManager.Instance.inventoryUI.WeaponAbilityContainer.GetComponentsInChildren<WeaponAbilitySlot>().ToList();
        
        
        LoadData();
    }

    public void SaveData(ref GameData data)
    {
        
        // basic items
        if (data.items == null)
            data.items = new List<ItemSaveData>(_inventoryItemSlots.Count);
        else
            data.items.Clear();

        foreach (var slot in _inventoryItemSlots)
        {
            if (!slot.itemSlotData.IsEmpty())
                data.items.Add(new ItemSaveData(slot.itemSlotData.ItemData.ID, slot.itemSlotData.Amount));
            else
                data.items.Add(new ItemSaveData());
        }

        // abilities
        if (data.abilities == null)
            data.abilities = new List<string>(_weaponAbilitySlots.Count);
        else
            data.abilities.Clear();

        foreach (var slot in _weaponAbilitySlots)
        {
            if (!slot.IsEmpty())
                data.abilities.Add(slot.abilityAsset.ItemData.ID);
            else
                data.abilities.Add(string.Empty);
        }

        // data.acquiredAbilities = acquiredAbilities.Select(w => w.ID).ToList();

    }

    public void LoadData()
    {
        GameData data = GameDataManager.GetLoadedData();

        // acquiredAbilities = data.acquiredAbilities.Select(id => ItemIdentifyManager.Instance.GetItemByID(id) as WeaponAbilityItem).ToHashSet<WeaponAbilityItem>();

        if (data.items == null)
            data.items = new List<ItemSaveData>(_inventoryItemSlots.Count);

        for (int i = 0; i < data.items.Count; i++)
            _inventoryItemSlots[i].itemSlotData.SetItemData(data.items[i].ID, data.items[i].amount);

        if (data.abilities == null)
            data.abilities = new List<string>(_weaponAbilitySlots.Count);

        for (int i = 0; i < data.abilities.Count; i++)
            _weaponAbilitySlots[i].abilityAsset.ItemData = ItemIdentifyManager.Instance.GetItemByID(data.abilities[i]) as WeaponAbilityItem;

        // if (data.armours == null)
        //     data.armours = new List<ArmourItemSaveData>(ArmourSlots.Count);
        // // if (ArmourSlots.Count != data.armours.Count)
        // //     return;

        // for (int i = 0; i < data.armours.Count; i++)
        // {
        //     if (string.IsNullOrEmpty(data.armours[i].ID))
        //         continue;
        //     ArmourReference aRef = ItemPoolManager.Instance.GetArmourReferenceByID(data.armours[i].ID);

        //     ArmourReference _aObj = ScriptableObject.CreateInstance(aRef.GetType()) as ArmourReference;
        //     if (_aObj == null)
        //         continue;
        //     _aObj.Set(aRef);
        // }
    }


}
