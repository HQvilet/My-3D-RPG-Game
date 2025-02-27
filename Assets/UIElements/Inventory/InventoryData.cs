using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public static int INVENTORY_CAPACITY = 25;
    public static int MAX_SLOT = 15;

    public static int MAX_STACK = 64; 

    public List<ItemStack> Items = new List<ItemStack>(INVENTORY_CAPACITY);
    void Awake()
    {
        LoadInventoryData();
    }

    private void LoadInventoryData()
    {

        // Resources.LoadAll<>
        for(int i = 0; i < MAX_SLOT; i++)
        {
            Items.Add(new ItemStack());
        }
    }
}
