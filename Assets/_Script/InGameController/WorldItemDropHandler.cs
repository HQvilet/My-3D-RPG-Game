using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaveLoadSystem;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldItemDropHandler : Singleton<WorldItemDropHandler>
{
    public float abilityDropRate;
    public float itemDropRate;
    public HashSet<WeaponAbilityItem> droppedAbilities = new();
    List<ItemData> normalItems;
    List<WeaponAbilityItem> abilityItems;

    public BasicItem _itemPref;

    void Start()
    {
        LoadAcquiredAbbilities();
        abilityItems = ItemIdentifyManager.Instance.GetAllNormalItem()
            .Where(item => item is WeaponAbilityItem)
            .Except(droppedAbilities)
            .Cast<WeaponAbilityItem>()
            .ToList();
        normalItems = ItemIdentifyManager.Instance.GetAllNormalItem()
            .Where(item => item is ConsumableItem)
            .ToList();

        
    }

    public void TryDropItemByRate(Vector3 position)
    {
        float rate = Random.Range(0, 1f);
        if(rate <= abilityDropRate)
        {
            WeaponAbilityItem item = MyUtils.ListRandChoice<WeaponAbilityItem>(abilityItems);
            if(!droppedAbilities.Contains(item))
            {
                DropRealWorldItem(position, item);
                abilityItems.Remove(item);
                droppedAbilities.Add(item);
                return;
            }
        }
        
        if(rate <= abilityDropRate + itemDropRate)
        {
            ItemData item = MyUtils.ListRandChoice<ItemData>(normalItems);
            DropRealWorldItem(position, item);
            return;
        }
    }

    public void SaveDroppedAbilities(ref GameData gameData)
    {
        gameData.acquiredAbilities = droppedAbilities.Select(w => w.ID).ToList();
    }

    public void LoadAcquiredAbbilities()
    {
        List<string> loadedData = GameDataManager.GetLoadedData().acquiredAbilities;
        if(loadedData == null)
            loadedData = new();
        droppedAbilities = loadedData.Select(id => ItemIdentifyManager.Instance.GetItemByID(id) as WeaponAbilityItem).ToHashSet<WeaponAbilityItem>();
    }

    public void DropRealWorldItem(Vector3 position, ItemData item)
    {
        Instantiate(_itemPref, position, Quaternion.identity)
            .SetData(item);
    }
}
