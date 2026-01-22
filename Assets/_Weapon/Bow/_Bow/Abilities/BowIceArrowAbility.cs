using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BowProjectileAbility", menuName = "Weapon/Abilities/Bow/BowProjectileAbility")]
public class BowProjectileAbility : CoolDownWeaponAbility 
{
    public DamageUnit projectile;
    public override bool OnEnableAbility(object weapon)
    {
        if(!(weapon is Bow bow))
            return false;

        return true;
    }

    public override void Execute(object weapon)
    {
        if(!(weapon is Bow bow))
            return;

        bow.ShotProjectile(projectile);
    }

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Bow bow))
            return false;

        return true;
    }
}