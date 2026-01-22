using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "StaffRedvelvetAbility", menuName = "Weapon/Abilities/Staff/RedVelvet")]
public class StaffRedVelvetAbility : CoolDownWeaponAbility 
{
    public float castDelay;
    public DamageUnit redVelvetVFX;
    
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
            List<EntityComponent> entities = EnvironmentHelper.Instance.GetAllEnemiesNearby(staff.transform.position, 25f, false).Take(10).ToList();
            IEnumerator<float> DelaySpawn()
            {
                foreach(var target in entities)
                {
                    Instantiate(redVelvetVFX, target.transform.position, Quaternion.identity)
                        .SetDamageData(staff.authenticatedOwner);
                    yield return Timing.WaitForSeconds(Random.Range(0.1f, 0.23f));
                }
            }
            Timing.RunCoroutine(DelaySpawn().CancelWith(staff.gameObject));
        }));
    }

    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Staff staff))
            return false;
        
        return true;
    }
}