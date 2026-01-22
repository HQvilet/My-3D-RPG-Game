using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffFreezeAOEAbility", menuName = "Weapon/Abilities/Staff/FreezeAOE")]
public class StaffFreezeAOEAbility : CoolDownWeaponAbility 
{
    public float castDelay;
    public DamageUnit freezeAOEVFX;
    
    public override bool OnEnableAbility(object weapon)
    {
        if(!(weapon is Staff staff))
            return false;

        return true;
    }

    public override void Execute(object weapon)
    {
        if(!(weapon is Staff staff))
            return;

        staff.playerAnimator.CrossFade("Casting", 0.1f);
        Timing.RunCoroutine(MyUtils.WaitToAction(castDelay, () =>
        {
            Instantiate(freezeAOEVFX, staff.authenticatedOwner.transform.position + staff.authenticatedOwner.transform.forward, Quaternion.identity)
                .SetDamageData(staff.authenticatedOwner);
        }));
    }

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Staff staff))
            return false;
        
        return true;
    }
}