using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : BaseWeapon
{

    [SerializeField] private WandUtilities wandUtilities;
    Animator rigAnimator;
    public override void OnSelected()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rigAnimator.Play("Casting Spell");
            wandUtilities.SkillSet_1();
        }
    }


    public override void OnDeselected()
    {
        gameObject.SetActive(false);
    }
    


    public override void WeaponSideRiggingSetup(WeaponModelConfig config)
    {
        config.SetRightHandedWeapon(this.transform);
    }
        
    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        rigAnimator = weaponService.animationSystem.rigAnimator;
        wandUtilities.SetEnemyEnvironment(weaponService.enemyData);
    }
}
