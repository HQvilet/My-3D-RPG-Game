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

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetRightHandedWeapon(this.transform);
        utilities.SetRootVFX(config.rootVFX);

        config.AddHitBoxCollider(utilities.hitbox.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        utilities.SetPlayerUtilities(weaponService.playerMovementUtilities);
        weaponCombo.SetStateMachine(weaponService.animationSystem);
        weaponCombo.SetWeaponStateHandler(weaponService.stateHandler);

        utilities.SetEnemyEnvironment(weaponService.enemyData);
        utilities.SetStats(weaponService.GetCharacterStats());
        weaponService.stateHandler.OnMeleePerformed += utilities.AttackPerform;
    }
}
