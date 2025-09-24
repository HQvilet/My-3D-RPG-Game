using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : BaseWeapon
{
    Animator animator;

    bool canStop;

    public override void OnSelected()
    {
        base.OnSelected();
        authenticatedOwner.stateHandler.OnAnimationEvent += RelyActionOnEvent;
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        authenticatedOwner.stateHandler.OnAnimationEvent -= RelyActionOnEvent;
    }

    void Update()
    {
        if (!AllowProcess())
            return;
            
        if (authenticatedOwner.TryGetEntityInput().Parry.WasPerformedThisFrame())
        {
            animator.CrossFade("Shield Block", 0.08f);
            canStop = true;
        }

        if (authenticatedOwner.TryGetEntityInput().Parry.WasReleasedThisFrame())
        {
            animator.SetFloat("ParrySpeed", 1f);
            if (animator.GetFloat("ParrySpeed") > 0.05f)
            {
                canStop = false;
            }
        }
    }

    public void LockMovement() => authenticatedOwner.stateHandler.CanMove = false;

    public void UnlockMovement() => authenticatedOwner.stateHandler.CanMove = true;
    

    public void Stop()
    {
        if(canStop)
            animator.SetFloat("ParrySpeed", 0f);
    }


    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetShield(this.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        animator = authenticatedOwner.GetModifiableAnimator().characterAnimator;

    }
}
