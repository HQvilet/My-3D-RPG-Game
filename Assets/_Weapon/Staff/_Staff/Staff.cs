using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.InputSystem;



public partial class Staff : BaseWeapon
{
    [ReadOnly] public Animator playerAnimator;
    [SerializeField] WeaponCombo weaponCombo;
    [SerializeField] Transform model;

    public override void GetInitialized()
    {
        base.GetInitialized();
        LoadData();
    }

    public override void RegistryForInput(InputDataHandler inputHandler)
    {
        base.RegistryForInput(inputHandler);
        inputHandler.playerInputAction.Attack.performed += (ctx) =>
        {
            if(!AllowProcess())
                return;
            if(Input.GetKey(KeyCode.LeftShift))
                DoSpecialAttack();
            else
                DoNormalAttack();
        };
    }

    protected override void AnimationEventBinding(string eventName) => EventHandler.RelyActionOnEvent(this, eventName);

    public override void OnSelected()
    {
        base.OnSelected();
        UpdateAbilityDataToUI();
        model.gameObject.SetActive(true);
        authenticatedOwner.stateHandler.OnAnimationEvent += AnimationEventBinding;
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        model.gameObject.SetActive(false);
        authenticatedOwner.stateHandler.OnAnimationEvent -= AnimationEventBinding;
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        GameUIManager.Instance.weaponAbilityUIHandler.SetWeaponAbilitiesData(this);
        playerAnimator = authenticatedOwner.GetComponentInChildren<Animator>();
        
        weaponCombo.SetStateMachine(playerAnimator);
        weaponCombo.SetWeaponStateHandler(authenticatedOwner.stateHandler);
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.AddToPool(this.transform);
        config.SetRightHandedWeapon(model);
        model.gameObject.SetActive(false);
    }
}

