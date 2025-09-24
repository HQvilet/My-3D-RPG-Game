using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Inventory.SaveSystem;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;



public class InventoryData : MonoBehaviour
{

    [SerializeField] private Transform p_item_inventory;
    [SerializeField] private Transform p_armour_inventory;
    // public static int INVENTORY_CAPACITY = 40;
    // public static int MAX_SLOT = 20;
    public static int MAX_STACK = 64;


    // public List<ItemStack> itemStacks = new List<ItemStack>(INVENTORY_CAPACITY);
    // public List<ArmourAsset> armourAssets = new List<ArmourAsset>(INVENTORY_CAPACITY);

    [HideInInspector]
    public List<ItemSlotUnit> ItemSlots;
    [HideInInspector]
    public List<ArmourSlotUnit> ArmourSlots;

    void Awake()
    {
        // LoadInventoryData();
        // LoadData();
        ItemSlots = p_item_inventory.GetComponentsInChildren<ItemSlotUnit>().ToList();
        ArmourSlots = p_armour_inventory.GetComponentsInChildren<ArmourSlotUnit>().ToList();
        InventorySaveSystem.Load();
    }

    public void SaveData(ref InventorySaveData data)
    {
        if (data.items == null)
            data.items = new List<ItemSaveData>(ItemSlots.Count);
        else
            data.items.Clear();

        foreach (var slot in ItemSlots)
        {
            if (!slot.itemSlotData.IsEmpty())
                data.items.Add(new ItemSaveData(slot.itemSlotData.ItemData.ID, slot.itemSlotData.Amount));
            else
                data.items.Add(new ItemSaveData());
        }

        if (data.armours == null)
            data.armours = new List<ArmourItemSaveData>(ArmourSlots.Count);
        else
            data.armours.Clear();

        foreach (var slot in ArmourSlots)
        {
            if (!slot.armourAsset.IsEmpty())
                data.armours.Add(new ArmourItemSaveData(slot.armourAsset.armourRef.ID, ""));
            else
                data.armours.Add(new ArmourItemSaveData());
        }
    }

    public void LoadData(ref InventorySaveData data)
    {
        if (data.items == null)
            data.items = new List<ItemSaveData>(ItemSlots.Count);
        // if (ItemSlots.Count != data.items.Count)
        //     return;

        for (int i = 0; i < data.items.Count; i++)
            ItemSlots[i].itemSlotData.SetItemData(data.items[i].ID, data.items[i].amount);

        if (data.armours == null)
            data.armours = new List<ArmourItemSaveData>(ArmourSlots.Count);
        // if (ArmourSlots.Count != data.armours.Count)
        //     return;

        for (int i = 0; i < data.armours.Count; i++)
        {
            if (string.IsNullOrEmpty(data.armours[i].ID))
                continue;
            ArmourReference aRef = ItemPoolManager.Instance.GetArmourReferenceByID(data.armours[i].ID);

            ArmourReference _aObj = ScriptableObject.CreateInstance(aRef.GetType()) as ArmourReference;
            if (_aObj == null)
                continue;
            ArmourSlots[i].armourAsset.SetArmourRef(_aObj);
        }
            
    }

}
