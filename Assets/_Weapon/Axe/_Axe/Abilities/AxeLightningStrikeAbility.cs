using System;
using MEC;
using UnityEngine;

[CreateAssetMenu(fileName = "AxeDashHitAbility", menuName = "Weapon/Abilities/Axe/DashHit")]
public class AxeLightningStrikeAbility : CoolDownWeaponAbility
{
    public ParticleSystem lightningTrailVFX;
    public DamageUnit lightningStrike;
    public override bool OnEnableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;

        // axe.lightningTrail = lightningTrailVFX;
        return true;
    }

    public override void Execute(object weapon)
    {
        if(!(weapon is Axe axe))
            return;
        
        StrikeThroughAttack(axe);
    }

    public void StrikeThroughAttack(Axe axe)
    {

        axe.playerAnimator.CrossFade("AttackStrike", 0.1f);

        var lightTrail = Instantiate(lightningTrailVFX, axe.transform);
        Ray ray = new Ray(axe.authenticatedOwner.transform.position + Vector3.up * 1f, axe.authenticatedOwner.transform.forward);
        float hitDistance = 6f;
        if(Physics.Raycast(ray, out RaycastHit hitInfo, hitDistance, EnvironmentHelper.Instance.onlyStaticObject))
        {
            hitDistance = hitInfo.distance;
        }

        Vector3 lastPosition = axe.transform.position + Vector3.up * 0.5f;
        Timing.RunCoroutine(MyUtils.WaitToAction(0.5f, () =>
        {
            
            axe.DashForward(hitDistance);
            Timing.RunCoroutine(MyUtils.WaitToAction(0.3f, () => lightTrail.Stop()));

            Vector3 currentPosition = axe.authenticatedOwner.transform.position + Vector3.up * 0.5f;
            RaycastHit[] colliders = Physics.SphereCastAll(lastPosition, 1f, (currentPosition - lastPosition).normalized, (currentPosition - lastPosition).magnitude, EnvironmentHelper.Instance.onlyEnemy);
            
            Debug.DrawRay(lastPosition, currentPosition - lastPosition, Color.blue, 10f);
            // Debug.Log(colliders.Length);
            Array.ForEach(colliders, hit =>
            {
                if(hit.collider.TryGetComponent(out EntityComponent entity))
                {
                    if(DamageHandler.CanDamageThisEntity(axe.authenticatedOwner, entity))
                    {
                        entity.effectModifier.GetDamage(axe.authenticatedOwner.characterStats.Atk * 0.1f, axe.authenticatedOwner, DmgType.PHYSIC);
                        Instantiate(lightningStrike, entity.transform.position, Quaternion.identity)
                            .SetDamageData(axe.authenticatedOwner);
                    }
                }
            });
        }));
    }
    public override bool OnDisableAbility(object weapon)
    {
        if(!(weapon is Axe axe))
            return false;

        // axe.lightningTrail = null;
        return true;
    }
}

