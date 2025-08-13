using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : BaseWeapon
{

    [SerializeField] private WandUtilities wandUtilities;
    Animator rigAnimator;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rigAnimator.Play("Casting Spell");
            wandUtilities.SkillSet_1();
        }
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetRightHandedWeapon(this.transform);
    }
        
    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        rigAnimator = weaponService.animationSystem.rigAnimator;
        wandUtilities.SetEnemyEnvironment(weaponService.enemyData);
    }
}
