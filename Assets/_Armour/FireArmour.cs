using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Armour Asset/Fire Armour Set")]
public class FireArmourRef : ArmourReference
{
    public ArmourItem item;
    public FireArmourUtils utils;

    public override void Set(ArmourReference reference)
    {
        FireArmourRef assetFile = reference as FireArmourRef;
        item = assetFile.item;

        utils = new FireArmourUtils();

    }

    public override ArmourUtils GetArmourUtils() => utils;
    public override ArmourItem GetItemData() => item;
}

public class FireArmourUtils : ArmourUtils
{
    public override void OnEquipped(EntityComponent entity)
    {
        entity.stateHandler.OnHitTarget += OnHitEffect;
    }

    
    
    private void OnHitEffect(EntityComponent target)
    {
        target.effectModifier.OnTakeFireDamage.Invoke(2, 5);
    }

    public override void OnUnequipped(EntityComponent entity)
    {
        entity.stateHandler.OnHitTarget -= OnHitEffect;
    }
}