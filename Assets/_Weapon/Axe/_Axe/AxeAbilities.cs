using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EditorAttributes;
using MEC;
using UnityEngine;

[System.Serializable]
public struct SlashVFX
{
    public Transform slash_vfx;
    public Vector3 slashQuaternion;    
}
public partial class Axe
{
    void Start()
    {
        hitbox.OnEntityEnter += OnDamageEntity;    
    }

    private void OnDamageEntity(DamageUnit hitbox, EntityComponent target)
    {
        if(DamageHandler.CanDamageThisEntity(authenticatedOwner, target))
            target.effectModifier.GetDamage(authenticatedOwner.characterStats.Atk * damageMultiplier * stepMultiplier, authenticatedOwner, DmgType.PHYSIC);
    }

    public float damageMultiplier = 0.15f;
    float stepMultiplier = 1f;

    [Header("Utilities Field")]
    
    public List<SlashVFX> hitAndSlashVFX;
    public void SetSlashVFXToDefault()
    {
        damageMultiplier = 0.15f;
        hitAndSlashVFX = defaultHitAndSlashes;
    }

    [SerializeField, DataTable(false, true)] private List<DamageVerifier> damageModifier;
    [SerializeField] private Transform _vfx;
    [SerializeField] private SlashHitBox hitbox;

    [Header("Effects")]    
    [SerializeField] List<SlashVFX> defaultHitAndSlashes;
    public GameObject lastSlashVFX;

    // WEAPON UTILITIES HANDLER
    private void SlashAttack()
    {
        hitbox.DoFlashHitbox();
    }
    
    private void DoSlash(SlashVFX slash)
    {
        var a = Instantiate(slash.slash_vfx, _vfx);
        if(a.TryGetComponent<DamageUnit>(out DamageUnit damageUnit))
        {
            damageUnit.SetDamageData(authenticatedOwner);
        }
        a.localRotation = Quaternion.Euler(slash.slashQuaternion);
        a.SetParent(null);
        SlashAttack();
    }

    public void Slash_1()
    {
        stepMultiplier = 1f;
        DoSlash(hitAndSlashVFX[0]);
    }

    public void Slash_2()
    {
        stepMultiplier = 1.1f;
        DoSlash(hitAndSlashVFX[1]);
    }
    

    public void Slash_3()
    {
        stepMultiplier = 1.5f;
        DoSlash(hitAndSlashVFX[2]);
        if(lastSlashVFX != null)
        {
            var obj = Instantiate(lastSlashVFX, transform.position+Vector3.up*1.5f, Quaternion.identity);
            obj.transform.forward = transform.forward;
            obj.GetComponent<DamageUnit>().SetDamageData(authenticatedOwner);            
        }
    }

    public void DoNormalAttack()
    {
        weaponCombo.weaponStateMachine.TriggerAttack();
    }

    public void PerformSpecialAttack()
    {
        TryExecuteAbilityAtIndex(selectedAbilityIndex);
    }

    public void AttackPerform()
    {
        Transform toRotate = EnvironmentHelper.Instance.GetFirstOrDefaultEnemyNearby(transform.position, 2f)?.transform ?? null;
        if(toRotate != null)
            authenticatedOwner.transform.DOLookAt(toRotate.position, Time.deltaTime * 2f, AxisConstraint.Y, Vector3.up);
    }

    public void DashForward(float distance)
    {
        playerController.DashForward(distance, 0.1f);
    }

}