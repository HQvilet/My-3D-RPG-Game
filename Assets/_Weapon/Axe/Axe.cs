using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Axe : BaseWeapon
{

    [SerializeField] private WeaponCombo weaponCombo;
    [SerializeField] private AxeUtilities utilities;
    [SerializeField] private Transform model;


    void Start()
    {
        weaponCombo = GetComponent<WeaponCombo>();
    }

    void Update()
    {
        if(InputDataHandler.Instance.PlayerInput.Attack.WasPerformedThisFrame())
            weaponCombo.weaponStateMachine.TriggerAttack();
    }

    void OnEnable()
    {
        model.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        model.gameObject.SetActive(false);
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.AddToPool(this.transform);
        config.SetRightHandedWeapon(model);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        weaponCombo.SetStateMachine(weaponService.animationSystem);
        weaponCombo.SetWeaponStateHandler(weaponService.entityComponent.stateHandler);

        utilities.SetPlayerUtilities(weaponService.playerMovementUtilities);
        utilities.SetEnemyEnvironment(weaponService.enemyData);
        utilities.SetStats(weaponService.GetCharacterStats());

    }
}
