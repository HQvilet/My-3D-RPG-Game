using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryData : MonoBehaviour
{

    [SerializeField] private Transform p_item_inventory;
    [SerializeField] private Transform p_armour_inventory;
    public static int INVENTORY_CAPACITY = 40;
    public static int MAX_SLOT = 30;

    public static int MAX_STACK = 64; 

    // public List<ItemStack> itemStacks = new List<ItemStack>(INVENTORY_CAPACITY);
    // public List<ArmourAsset> armourAssets = new List<ArmourAsset>(INVENTORY_CAPACITY);

    public List<ItemSlotUnit> ItemSlots;
    public List<ArmourSlotUnit> ArmourSlots;

    void Awake()
    {
        LoadInventoryData();
        // LoadData();
    }

    private void LoadInventoryData()
    {
        ItemSlots = p_item_inventory.GetComponentsInChildren<ItemSlotUnit>().ToList();
        MAX_SLOT = ItemSlots.Count();
        foreach(ItemSlotUnit slotUnit in ItemSlots)
        {
            // slotUnit.SetSlotData(new ItemStack());
        }

        ArmourSlots = p_armour_inventory.GetComponentsInChildren<ArmourSlotUnit>().ToList();
        foreach(ArmourSlotUnit slotUnit in ArmourSlots)
        {
            // slotUnit.SetSlotData(new ArmourAsset());
        }
    }

    private void LoadArmourSlotData()
    {
        
    }

    public void SaveData()
    {

    }

    public void LoadData()
    {
        
    }

    public void ConfigData(ref List<ItemSlotUnit> itemSlotUnits)
    {

    }
}
