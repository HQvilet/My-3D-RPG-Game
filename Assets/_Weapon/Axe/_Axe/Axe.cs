// using System;
using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class Axe : BaseWeapon
{
    [ReadOnly] public Animator playerAnimator;
    
    [SerializeField] private WeaponCombo weaponCombo;
    [SerializeField] private Transform model;

    public override void GetInitialized()
    {
        base.GetInitialized();
        
        SetSlashVFXToDefault();
        LoadData();
    }

    public override void OnSelected()
    {
        base.OnSelected();
        UpdateAbilityDataToUI();
        model.gameObject.SetActive(true);
        authenticatedOwner.stateHandler.OnAnimationEvent += AnimationEventBinding;
        authenticatedOwner.stateHandler.OnMeleePerformed += AttackPerform;
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        model.gameObject.SetActive(false);
        authenticatedOwner.stateHandler.OnAnimationEvent -= AnimationEventBinding;
        authenticatedOwner.stateHandler.OnMeleePerformed -= AttackPerform;
    }

    protected override void AnimationEventBinding(string eventName) => EventHandler.RelyActionOnEvent(this, eventName);
    
    void AttackActionPerform()
    {
        if (!gameObject.activeSelf)
            return;

        if (authenticatedOwner.stateHandler.AllowToInterupt)
            if(Input.GetKey(KeyCode.LeftShift))
                PerformSpecialAttack();
            else
                DoNormalAttack();
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.AddToPool(this.transform);
        config.SetRightHandedWeapon(model);
        model.gameObject.SetActive(false);
    }

    PlayerBehaviourHandler playerController;
    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        GameUIManager.Instance.weaponAbilityUIHandler.SetWeaponAbilitiesData(this);
        playerAnimator = authenticatedOwner.GetComponentInChildren<Animator>();
        playerController = authenticatedOwner.GetComponent<PlayerBehaviourHandler>();
        
        weaponCombo.SetStateMachine(playerAnimator);
        weaponCombo.SetWeaponStateHandler(authenticatedOwner.stateHandler);
        
    }

    public override void RegistryForInput(InputDataHandler inputHandler)
    {
        base.RegistryForInput(inputHandler);
        inputHandler.playerInputAction.Attack.performed += context => AttackActionPerform();
    }

    void OnDestroy()
    {
        
    }
}
