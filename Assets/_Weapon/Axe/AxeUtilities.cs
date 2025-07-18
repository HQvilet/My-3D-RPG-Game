using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


// define slash quaternion vector
[System.Serializable]
public struct SlashVFX
{
    public Transform slash_vfx;
    public Vector3 slashQuaternion;    
}

public class AxeUtilities : BaseWeaponUtilities
{
    [SerializeField] private SlashVFX[] hitAndSlashes = new SlashVFX[3];
    [SerializeField] private DamageModifier damageModifier;
    [SerializeField] private Transform _vfx;
    [SerializeField] private DamageHitbox hitbox;

    void Start()
    {
        EntityComponentSystem.Instance.GetPlayerComponent().stateHandler.OnMeleePerformed += AttackPerform;
        hitbox.SetSourceDamage(EntityComponentSystem.Instance.GetPlayerComponent());
        hitbox.gameObject.SetActive(false);
    }

    private MovementUtilities playerMovementUtilities;
    public void SetPlayerUtilities(MovementUtilities movementUtilities) => playerMovementUtilities = movementUtilities;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy) => this.senseOfEnemy = senseOfEnemy;

    private CharacterStats playerStats;
    public void SetStats(CharacterStats stats) => this.playerStats = stats;


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

    public void Slash_1()
    {
        Debug.Log("Slasheded");
        DoSlash(hitAndSlashes[0]);
    }

    public void Slash_2()
    {
        DoSlash(hitAndSlashes[1]);
    }

    public void Slash_3()
    {
        DoSlash(hitAndSlashes[2]);
    }

    public void BackStab(){}
    
    public void AttackPerform()
    {
        senseOfEnemy.QueryEnemyInRange(0.8f, out Transform nearest_obj);
        if (nearest_obj != null)
            playerMovementUtilities.RotateTowardTarget(nearest_obj.position);
    }


    void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        foreach(var a in hitAndSlashes)
        {
            Vector3 normal = Quaternion.Euler(a.slashQuaternion) * Vector3.up;
            Handles.DrawWireDisc(_vfx.position ,normal ,1.1f);
        }
    }
}
