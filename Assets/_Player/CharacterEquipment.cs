using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] StatMediator stats;
    [SerializeField] List<ArmourUtils> armourUtils;

    void Start()
    {
        Bus<EquipArmourEvent>.AddRegister(DoEquip);
    }

    private void DoEquip(EquipArmourEvent @event)
    {
        // Equip(@event.a.ItemData);
        Debug.Log("Equip " + @event.armourInfo.ItemData.Name);
    }

    public void Equip(ArmourItem item)
    {
        stats.AddStats(item.stats);
        // armour utilities callbacks
        // add utilities to list
    }

    public void Unequip(ArmourItem item)
    {
        stats.RemoveStats(item.stats);
    }

    // void Update()
    // {
    //     foreach(ArmourUtils utils in armourUtils)
    //         utils.OnEquippedStay();
    // }
}

public class EquipArmourEvent : IEvent
{
    public EquipArmourEvent(ArmourAsset asset){armourInfo = asset;}
    public ArmourAsset armourInfo;
}

public class SelectWeaponEvent : IEvent
{

}