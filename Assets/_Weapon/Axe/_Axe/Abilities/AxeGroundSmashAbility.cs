using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeGroundSmashAbility", menuName = "Weapon/Abilities/Axe/GroundSmash")]
public class AxeGroundSmashAbility : CoolDownWeaponAbility 
{
    public DamageUnit groundSmash_VFX;

    public override bool OnEnableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;
        return true;
    }

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;
        return true;
    }

    public override void Execute(object weapon)
    {
        if(!(weapon is Axe axe))
            return;

        axe.playerAnimator.CrossFade("GroundSmash", 0.1f);
        Timing.RunCoroutine(MyUtils.WaitToAction(1.02f, () =>
        {
            Instantiate(groundSmash_VFX, axe.transform.position, Quaternion.identity)
                .SetDamageData(axe.authenticatedOwner);
        }));
    }

}

