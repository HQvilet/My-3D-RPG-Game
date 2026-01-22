using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Staff
{
    public FollowProjectile normalAttackProjectile;

    public void SkillSet_1(){}

    void DoNormalAttack() => weaponCombo.weaponStateMachine.TriggerAttack();

    public void NormalAttack_1()
    {
        CastProjectile(11f);
    }

    public void NormalAttack_2()
    {
        CastProjectile(11f);
    }

    public void CastProjectile(float range)
    {
        EntityComponent ent = EnvironmentHelper.Instance.GetAllEnemiesNearby(transform.position, range)
            .OrderByDescending(e => Vector3.Dot(transform.forward , (e.transform.position - transform.position).normalized))
            .FirstOrDefault();

        Transform target = ent?.transform ?? null;
        
        var a = Instantiate(normalAttackProjectile, transform.position + Vector3.up * 1f + transform.forward * 0.5f, Quaternion.identity);
        a.SetDamageData(authenticatedOwner);
        a.SetDefault();
        a.SetTarget(target, transform.forward);
    }

    public void NormalAttack_3()
    {
        
    }

    void DoSpecialAttack()
    {
        if(!TryExecuteAbilityAtIndex(selectedAbilityIndex))
        {
            
        }
    }

    protected override void OnAbilitySelected(int index)
    {
        
    }

    public void DoLongCast()
    {
        playerAnimator.CrossFade("Casting", 0.1f);
    }
}
