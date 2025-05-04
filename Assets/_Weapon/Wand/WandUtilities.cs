using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandUtilities : BaseWeaponUtilities
{

    [SerializeField] private Transform AoE_VFX;
    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy)
    {
        // this.senseOfEnemy = senseOfEnemy;
        senseOfEnemy.OnDetectedEnemy += DDD; 
    }

    private void DDD(Transform t_transform)
    {
        target = t_transform;
    }

    [SerializeField] private Transform target;

    void Start()
    {
        
    }



    public void SkillSet_1()
    {
        //play animation

        //Spawn vfx
        Instantiate(AoE_VFX ,target.position ,Quaternion.identity);
        //Attack hit box
    }

    public void SkillSet_2()
    {
        
    }

    public void SkillSet_3()
    {
        
    }

}
