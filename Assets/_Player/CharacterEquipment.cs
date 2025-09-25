using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    HashSet<ArmourUtils> armourUtils = new();
    [SerializeField] EntityComponent owner;

    void Start()
    {
        // Bus<EquipArmourEvent>.AddRegister(DoEquip);
        // Bus<UnequipArmourEvent>.AddRegister(DoUnequip);
    }

    public void DoEquip(ArmourReference armour)
    {
        if(armour.GetArmourUtils() == null)
            return;
        armourUtils.Add(armour.GetArmourUtils());
        armour.GetArmourUtils().OnEquipped(owner);
    }

    public void DoUnequip(ArmourReference armour)
    {
        if(armour.GetArmourUtils() == null) 
            return;
        armourUtils.Remove(armour.GetArmourUtils());
        armour.GetArmourUtils().OnUnequipped(owner);
    }

    void Update()
    {
        foreach(ArmourUtils utils in armourUtils)
            utils.OnEquippedStay(owner);
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