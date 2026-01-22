using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

namespace GameSaveLoadSystem
{
    [Serializable]
    public class ItemSaveData
    {
        public ItemSaveData(string ID = "", int amount = 0)
        {
            this.ID = ID;
            this.amount = amount;
        }
        public string ID;
        public int amount;
    }

    [Serializable]
    public class WeaponAbilitySaveData
    {
        public int weaponID;
        public List<string> abilityIDs;
    }

    [Serializable]
    public class ArmourItemSaveData
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
    public class GameData
    {
        public List<ItemSaveData> items;
        public List<string> abilities;
        public List<WeaponAbilitySaveData> weaponAbilities;
        public bool isBossDead;
        public List<string> acquiredAbilities;
    }

    public class GameDataManager
    {
        public static string SavePath => Application.persistentDataPath + "/inventory.json";
        public static GameData inGameSavedData = new();
        public static int loadState;

        public static void Save()
        {
            if(InventoryManager.Instance == null || GameUIManager.Instance == null || WorldItemDropHandler.Instance == null || InGameEventHandler.Instance == null)
                return;

            InventoryManager.Instance.inventoryData.SaveData(ref inGameSavedData);
            GameUIManager.Instance.weaponAbilityUIHandler.SaveWeaponAbilityData(ref inGameSavedData.weaponAbilities);
            WorldItemDropHandler.Instance.SaveDroppedAbilities(ref inGameSavedData);
            InGameEventHandler.Instance.SaveData(ref inGameSavedData);
            // File save streaming
            string jsonString = JsonUtility.ToJson(inGameSavedData, true);
            
            File.WriteAllText(SavePath, jsonString);
        }

        public static void Load()
        {
            if (!File.Exists(SavePath))
            {
                inGameSavedData = new();
                return;
            }
            inGameSavedData = JsonUtility.FromJson<GameData>(File.ReadAllText(SavePath));
        }

        public static GameData GetLoadedData() => inGameSavedData;
    }
}