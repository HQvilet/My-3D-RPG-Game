using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AxeUtilities : BaseWeaponUtilities
{

    //Hit And Slash Data
    [SerializeField] private HitAndSlashData hitAndSlashes;
    [SerializeField] private DamageStats damageStats;

    private EnemyDetection senseOfEnemy;
    public void SetEnemyEnvironment(EnemyDetection senseOfEnemy)
    {
        this.senseOfEnemy = senseOfEnemy;
    }

    CharacterStats playerStats;
    public void SetStats(CharacterStats stats)
    {
        this.playerStats = stats; 
    }
    
    public DamageModifier Processor(CharacterStats stats ,DamageStats damageStats)
    {
        DamageModifier dmg = new DamageModifier();

        dmg.physicalDamage = stats.Atk * damageStats.physicalDamage;
        dmg.fireDamage = stats.Atk * damageStats.fireDamage;
        dmg.elementalDamage = stats.Atk * damageStats.elementalDamage;
        dmg.knockBack = damageStats.knockBack;

        return dmg;
    }


    public BoxCollider hitCollider;
    void Start()
    {
        hitCollider.gameObject.SetActive(false);
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
    
    IEnumerator ShowHitBox()
    {
        hitCollider.size = size;
        hitCollider.transform.localPosition = Vector3.forward * distance;

        // Processor(stats ,damageStats);
        // hitCollider.GetComponent<DamageHitbox>().SetAttackDamage(damageStats);
        hitCollider.GetComponent<DamageHitbox>().SetAttackDamage(Processor(playerStats ,damageStats));
        hitCollider.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        hitCollider.gameObject.SetActive(false);
    }

    public void Slash_1()
    {
        Instantiate<Transform>(hitAndSlashes.SlashVFX[0].slash_vfx ,rootVFX).localRotation = hitAndSlashes.SlashVFX[0].slashQuaternion;
        StartCoroutine(ShowHitBox());
    }

    public void Slash_2()
    {
        Instantiate<Transform>(hitAndSlashes.SlashVFX[1].slash_vfx ,rootVFX).localRotation = hitAndSlashes.SlashVFX[1].slashQuaternion;
        StartCoroutine(ShowHitBox());
    }

    public void Slash_3()
    {
        Instantiate<Transform>(hitAndSlashes.SlashVFX[2].slash_vfx ,rootVFX).localRotation = hitAndSlashes.SlashVFX[2].slashQuaternion;
        StartCoroutine(ShowHitBox());
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
        foreach(var a in hitAndSlashes.SlashVFX)
        {
            Vector3 normal = a.slashQuaternion * Vector3.up;
            Handles.DrawWireDisc(rootVFX.position ,normal ,1.1f);
        }
        
    }


}
