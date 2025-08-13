using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Armour Asset/Thorn Armour Set")]
public class ThornPlatedArmourRef : ArmourReference //,IArmourRef
{
    public ArmourItem item;
    public ThornArmourUtils utils;

    public override void Set(ArmourReference reference)
    {
        ThornPlatedArmourRef assetFile = reference as ThornPlatedArmourRef;
        item = assetFile.item;

        utils = new ThornArmourUtils();
    }

    public override ArmourUtils GetArmourUtils() => utils;
    public override ArmourItem GetItemData() => item;

}

public class ThornArmourUtils : ArmourUtils
{

    public override void OnEquipped(EntityComponent entity)
    {
        entity.stateHandler.OnGetHit += ReflectDamage;
    }

    private void ReflectDamage(EntityComponent target) 
    {
        target.effectModifier.OnTakePhysicDamage.Invoke(12);
    }

    public override void OnEquippedStay(EntityComponent entity)
    {
        
    }

    public override void OnUnequipped(EntityComponent entity)
    {

    }
}