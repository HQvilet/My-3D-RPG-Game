using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    HashSet<ArmourUtils> armourUtils = new();

    void Start()
    {
        Bus<EquipArmourEvent>.AddRegister(DoEquip);
        Bus<UnequipArmourEvent>.AddRegister(DoUnequip);
    }


    private void DoEquip(EquipArmourEvent @event)
    {
        if(@event.armourInfo.GetArmourUtils() == null) 
            return;
        armourUtils.Add(@event.armourInfo.GetArmourUtils());
        @event.armourInfo.GetArmourUtils().OnEquipped(EntityComponentSystem.Instance.GetPlayerComponent());
    }

    private void DoUnequip(UnequipArmourEvent @event)
    {
        if(@event.armourInfo.GetArmourUtils() == null) 
            return;
        armourUtils.Remove(@event.armourInfo.GetArmourUtils());
        @event.armourInfo.GetArmourUtils().OnUnequipped(EntityComponentSystem.Instance.GetPlayerComponent());
    }
    
    void Update()
    {
        // foreach(ArmourUtils utils in armourUtils)
        //     utils.OnEquippedStay();
    }
}

public class EquipArmourEvent : IEvent
{
    public EquipArmourEvent(ArmourReference asset) => armourInfo = asset;
    public ArmourReference armourInfo;
}

public class UnequipArmourEvent : IEvent
{
    public UnequipArmourEvent(ArmourReference asset) => armourInfo = asset;
    public ArmourReference armourInfo;
}