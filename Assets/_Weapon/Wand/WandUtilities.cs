using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandUtilities : BaseWeaponUtilities
{

    [SerializeField] private Transform AoE_VFX;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy) => this.senseOfEnemy = senseOfEnemy;

    public void AttackPerform()
    {
        senseOfEnemy.QueryEnemyInRange(5f, out Transform nearest_obj);
        if (nearest_obj != null)
        {
            var _obj = Instantiate(AoE_VFX, MyUtils.ModifyVector(nearest_obj.position, y : 0), Quaternion.identity);
            _obj.GetComponent<DamageHitbox>().SetSourceDamage(EntityComponentSystem.Instance.GetPlayerComponent());
        }
    }

    public void SkillSet_1()
    {
        AttackPerform();
    }

    void DoAttack()
    {
        var _obj = Instantiate(AoE_VFX, Vector3.zero, Quaternion.identity);
        _obj.GetComponent<DamageHitbox>().SetSourceDamage(EntityComponentSystem.Instance.GetPlayerComponent());

    }
    

}
