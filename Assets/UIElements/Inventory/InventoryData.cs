using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InventoryData : MonoBehaviour
{
    public static int INVENTORY_CAPACITY = 40;
    public static int MAX_SLOT = 30;

    public static int MAX_STACK = 64; 

    public List<ItemStack> itemStacks = new List<ItemStack>(INVENTORY_CAPACITY);
    void Awake()
    {
        // LoadInventoryData();
    }

    private void LoadInventoryData()
    {
        // Resources.LoadAll<>
        for(int i = 0; i < MAX_SLOT; i++)
        {
            itemStacks.Add(new ItemStack());
        }
    }

    private void LoadArmourSlotData()
    {
        
    }
}
