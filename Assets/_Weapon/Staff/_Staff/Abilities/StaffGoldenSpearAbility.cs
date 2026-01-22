using System.Collections.Generic;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffGoldenGateAbility", menuName = "Weapon/Abilities/Staff/GoldenGate")]
public class StaffGoldenGateAbility : CoolDownWeaponAbility 
{
    public ProjectileSpawner spawner;
    public float castDelay;
    public int gateCount = 20;
    
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
            for(int i = 0; i < gateCount; ++i)
            {
                Vector3 outerRandom = staff.transform.position + Random.onUnitSphere * Random.Range(12f, 16f);
                outerRandom.y = Mathf.Abs(outerRandom.y);
                Vector3 innerRandom = staff.transform.position + Random.onUnitSphere * 5f;
                Instantiate(spawner,
                    outerRandom, 
                    Quaternion.LookRotation(innerRandom - outerRandom)
                ).SetDamageData(staff.authenticatedOwner);
            }
        }));
        
    }

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Staff staff))
            return false;
        
        return true;
    }
}