using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

namespace Inventory.SaveSystem
{
    [Serializable]
    public struct ItemSaveData
    {
        public ItemSaveData(string ID, int amount)
        {
            this.ID = ID;
            this.amount = amount;
        }
        public string ID;
        public int amount;
    }

    [Serializable]
    public struct ArmourItemSaveData
    {
        public ArmourItemSaveData(string ID, string serializedData)
        {
            this.ID = ID;
            this.data = serializedData;
        }

        public string ID;
        public string data;
    }

    [Serializable]
    public struct InventorySaveData
    {
        public List<ItemSaveData> items;
        public List<ArmourItemSaveData> armours;
    }

    public class InventorySaveSystem
    {
        public static string SavePath => Application.persistentDataPath + "/inventory.json";
        public static InventorySaveData inventorySaveData = new();

        public static void Save()
        {
            // Get data
            InventoryManager.Instance.inventoryData.SaveData(ref inventorySaveData);

            // File save streaming
            string jsonString = JsonUtility.ToJson(inventorySaveData, true);
            Debug.Log(jsonString);
            File.WriteAllText(SavePath, jsonString);
        }

        public static void Load()
        {
            if (!File.Exists(SavePath))
                return;
            inventorySaveData = JsonUtility.FromJson<InventorySaveData>(File.ReadAllText(SavePath));

            InventoryManager.Instance.inventoryData.LoadData(ref inventorySaveData);
        }
    }

}