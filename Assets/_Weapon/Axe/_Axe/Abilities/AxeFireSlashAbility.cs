using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeFireSlashAbility", menuName = "Weapon/Abilities/Axe/FireSlash")]
public class AxeFireSlashAbility : WeaponAbility 
{
    public List<SlashVFX> fireHitAndSlashes;
    public GameObject specialFireSlash;
    
    public override bool OnEnableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;

        axe.damageMultiplier = 0.25f;
        axe.hitAndSlashVFX = fireHitAndSlashes;
        axe.lastSlashVFX = specialFireSlash;

        return true;
    }

    

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;

        axe.SetSlashVFXToDefault();

        return true;
    }
}