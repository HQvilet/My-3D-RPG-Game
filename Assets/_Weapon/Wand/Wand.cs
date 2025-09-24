using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : BaseWeapon
{

    public DamageModifier damageModifier;
    Animator animator;

    void Update()
    {
        if (!AllowProcess())
            return;

        if (authenticatedOwner.TryGetEntityInput().Cast.WasPerformedThisFrame())
        {
            animator.CrossFade("Casting", 0.1f);
        }
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetRightHandedWeapon(this.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        animator = authenticatedOwner.GetModifiableAnimator().characterAnimator;
        SetEnemyEnvironment(weaponService.enemySense);
    }

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
    
    [SerializeField] private Transform AoE_VFX;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy) => this.senseOfEnemy = senseOfEnemy;


    public void SkillSet_1() => AttackPerform();

    public void LockMovement() => authenticatedOwner.stateHandler.CanMove = false;

    public void UnlockMovement() => authenticatedOwner.stateHandler.CanMove = true;

    public void AttackPerform()
    {
        senseOfEnemy.QueryEnemyInRange(5f, out Transform nearest_obj);
        if (nearest_obj != null)
        {
            var _obj = Instantiate(AoE_VFX, MyUtils.ModifyVector(nearest_obj.position, y: 0), Quaternion.identity);
            var a = _obj.GetComponent<DamageHitbox>();
            a.SetSourceDamage(authenticatedOwner);
            a.SetAttackDamage(damageModifier);

        }
    }
}
