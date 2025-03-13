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

    public BoxCollider hitCollider;

    private Transform rootVFX;

    void Start()
    {
        hitCollider.gameObject.SetActive(false);
    }

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
        hitCollider.GetComponent<DamageHitbox>().SetAttackDamage(damageStats);

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
