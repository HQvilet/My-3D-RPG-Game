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
    //Hit And Slash Data
    [SerializeField] private SlashVFX[] hitAndSlashes = new SlashVFX[3];
    // List<SlashVFX> hitAndSlashes;
    [SerializeField] private DamageModifier damageModifier;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy)
    {
        this.senseOfEnemy = senseOfEnemy;
    }

    private CharacterStats playerStats;
    public void SetStats(CharacterStats stats)
    {
        this.playerStats = stats;
    }
    
    public DamageHitbox hitbox;

    void Start()
    {
        hitbox.SetSourceDamage(EntityComponentSystem.Instance.GetPlayerComponent());
        hitbox.gameObject.SetActive(false);
        
        hitbox.transform.localPosition = Vector3.forward * distance;
    }

    private Transform rootVFX;
    public void SetRootVFX(Transform rootTransform)
    {
        rootVFX = rootTransform;
    }

    public Vector3 size;
    public float distance;

    private MovementUtilities playerMovementUtilities;
    public void SetPlayerUtilities(MovementUtilities movementUtilities)
    {
        playerMovementUtilities = movementUtilities;
    }

    private void SlashAttack()
    {
        hitbox.DoDamage(damageModifier);
        // StartCoroutine(TriggerDamageCollider());
    }

    IEnumerator TriggerDamageCollider()
    {
        hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitbox.gameObject.SetActive(false);
    }

    private void DoSlash(SlashVFX slash)
    {
        Instantiate<Transform>(slash.slash_vfx ,rootVFX).localRotation = Quaternion.Euler(slash.slashQuaternion);
        SlashAttack();
    }

    public void Slash_1()
    {
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


    public void BackStab()
    {
        
    }

    internal void AttackPerform()
    {
        senseOfEnemy.QueryEnemyInRange(1f ,out Transform nearest_obj);
        if(nearest_obj != null)
            playerMovementUtilities.RotateTowardTarget(nearest_obj.position);
    }

    void OnDrawGizmos()
    {
        Handles.color = Color.yellow;
        foreach(var a in hitAndSlashes)
        {
            Vector3 normal = Quaternion.Euler(a.slashQuaternion) * Vector3.up;
            Handles.DrawWireDisc(rootVFX.position ,normal ,1.1f);
        }
        
    }
}
