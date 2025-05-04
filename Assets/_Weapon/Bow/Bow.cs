using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon
{
    Animator rigAnimator;

    public override void OnSelected()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if(InputDataHandler.Instance.PlayerInput.Parry.WasPressedThisFrame())
            rigAnimator.Play("Parrying");
        else if(InputDataHandler.Instance.PlayerInput.Parry.WasReleasedThisFrame())
            rigAnimator.Play("Parrying Reverse");
        
    }

    private void ParryAction()
    { 
        Debug.Log("Is Parrying");
    }

    public override void OnDeselected()
    {
        gameObject.SetActive(false);
        rigAnimator.Play("New State");
    }
    void OnDisable()
    {
        
    }


    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetShield(this.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        rigAnimator = weaponService.animationSystem.rigAnimator;
    }
}
