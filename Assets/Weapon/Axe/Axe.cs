using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Axe : BaseWeapon
{

    [SerializeField] private WeaponCombo weaponCombo;
    // [SerializeField] private WeaponServiceLocator weaponService;
    [SerializeField] private AxeUtilities utilities;

    void Start()
    {
        weaponCombo = GetComponent<WeaponCombo>();
    }

    void Update()
    {
        if(InputDataHandler.Instance.PlayerInput.Attack.WasPerformedThisFrame())
            weaponCombo.weaponStateMachine.TriggerAttack();
    }

    public override void OnSelected()
    {
        gameObject.SetActive(true);
    }

    public override void OnDeselected()
    {
        gameObject.SetActive(false);
    }

    
    void OnDisable()
    {
        
    }

    public override void WeaponSideRiggingSetup(WeaponModelConfig config)
    {
        config.SetRightHandedWeapon(this.transform);
        utilities.SetRootVFX(config.rootVFX);

        config.AddHitBoxCollider(utilities.hitCollider.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        utilities.SetPlayerUtilities(weaponService.playerMovementUtilities);
        weaponCombo.SetStateMachine(weaponService.animationSystem);
        weaponCombo.SetWeaponStateHandler(weaponService.stateHandler);
    }
}
