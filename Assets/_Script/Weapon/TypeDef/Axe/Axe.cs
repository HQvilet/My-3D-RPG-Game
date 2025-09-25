using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct SlashVFX
{
    public Transform slash_vfx;
    public Vector3 slashQuaternion;    
}

public class Axe : BaseWeapon
{

    [SerializeField] private WeaponCombo weaponCombo;
    [SerializeField] private Transform model;

    void Update()
    {
        if (!AllowProcess())
            return;

        if (authenticatedOwner.TryGetEntityInput().Attack.WasPerformedThisFrame())
            weaponCombo.weaponStateMachine.TriggerAttack();
    }

    public override void OnSelected()
    {
        base.OnSelected();
        authenticatedOwner.stateHandler.OnAnimationEvent += RelyActionOnEvent;
        authenticatedOwner.stateHandler.OnMeleePerformed += AttackPerform;
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        authenticatedOwner.stateHandler.OnAnimationEvent -= RelyActionOnEvent;
        authenticatedOwner.stateHandler.OnMeleePerformed -= AttackPerform;
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
        weaponCombo.SetStateMachine(authenticatedOwner.GetModifiableAnimator());
        weaponCombo.SetWeaponStateHandler(authenticatedOwner.stateHandler);

        SetPlayerUtilities(weaponService.playerMovementUtilities);
        SetEnemyEnvironment(weaponService.enemySense);

        hitbox.SetSourceDamage(authenticatedOwner);
    }

    [Header("Utilities Field")]

    [SerializeField] private SlashVFX[] hitAndSlashes = new SlashVFX[3];
    [SerializeField] private DamageModifier damageModifier;
    [SerializeField] private Transform _vfx;
    [SerializeField] private SlashHitBox hitbox;

    private MovementUtilities playerMovementUtilities;
    public void SetPlayerUtilities(MovementUtilities movementUtilities) => playerMovementUtilities = movementUtilities;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy) => this.senseOfEnemy = senseOfEnemy;


    // WEAPON UTILITIES HANDLER
    private void SlashAttack()
    {
        hitbox.DoDamage(damageModifier);
    }
    private void DoSlash(SlashVFX slash)
    {
        Instantiate<Transform>(slash.slash_vfx, _vfx).localRotation = Quaternion.Euler(slash.slashQuaternion);
        SlashAttack();
    }

    public void Slash_1() => DoSlash(hitAndSlashes[0]);

    public void Slash_2() => DoSlash(hitAndSlashes[1]);

    public void Slash_3() => DoSlash(hitAndSlashes[2]);

    public void AttackPerform()
    {
        senseOfEnemy.QueryEnemyInRange(0.8f, out Transform nearest_obj);
        if (nearest_obj != null)
            playerMovementUtilities.RotateTowardTarget(nearest_obj.position);
    }


    void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        foreach (var a in hitAndSlashes)
        {
            Vector3 normal = Quaternion.Euler(a.slashQuaternion) * Vector3.up;
            Handles.DrawWireDisc(_vfx.position, normal, 1.1f);
        }
    }

}
