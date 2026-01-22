using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shield : BaseWeapon
{
    [Header("Input")]
    public InputAction parryAction; 

    [SerializeField] Transform model;
    Animator animator;

    bool isParrying = false;

    public override void OnSelected()
    {
        base.OnSelected();
        parryAction.Enable();
        authenticatedOwner.stateHandler.OnAnimationEvent += AnimationEventBinding;
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        parryAction.Disable();
        ToNormalState();
        authenticatedOwner.stateHandler.OnAnimationEvent -= AnimationEventBinding;
    }

    protected override void AnimationEventBinding(string eventName) => EventHandler.RelyActionOnEvent(this, eventName);

    public void LockMovement() => authenticatedOwner.stateHandler.CanMove = true;

    public void UnlockMovement() => authenticatedOwner.stateHandler.CanMove = true;

    public override void RegistryForInput(InputDataHandler inputHandler)
    {
        parryAction.performed += DoParryStuff;
        parryAction.canceled += ShieldRelease;

        authenticatedOwner.effectModifier.OnGetHit += OnShieldTakeDamage;

        parryAction.Enable();
    }

    private void OnShieldTakeDamage(float dmg, EntityComponent component, DmgType type)
    {
        if(isParrying)
            if(Vector3.Dot((component.transform.position - authenticatedOwner.transform.position).normalized, authenticatedOwner.transform.forward) > 0.25f)
            {
                ParrySuccessfully();
            }
    }

    void ParrySuccessfully()
    {
        animator.CrossFade("ParryHit", 0.08f);
    }

    private void DoParryStuff(InputAction.CallbackContext context)
    {
        if(!AllowProcess())
            return;

        isParrying = true;
        authenticatedOwner.stateHandler.CanMove = false;
        authenticatedOwner.effectModifier.AddEffect("Parrying");
        animator.CrossFade("Parry", 0.1f);
        animator.SetBool("IsParrying", true);
    }

    private void ShieldRelease(InputAction.CallbackContext context)
    {
        if(!AllowProcess())
            return;

        // isParrying = false;
        // authenticatedOwner.effectModifier.RemoveEffect("Parrying");
        // animator.SetBool("IsParrying", false);
        ToNormalState();
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetShield(this.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        animator = authenticatedOwner.GetComponentInChildren<Animator>();
        // GameUIManager.Instance.weaponAbilityUIHandler.SetWeaponAbilitiesData(this);
    }

    void ToNormalState()
    {
        isParrying = false;
        authenticatedOwner.stateHandler.CanMove = true;
        authenticatedOwner.effectModifier.RemoveEffect("Parrying");
        animator.SetBool("IsParrying", false);
    }
}
