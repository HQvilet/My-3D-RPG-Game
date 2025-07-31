using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


[RequireComponent(typeof(Collider))]
public class DamageHitbox : MonoBehaviour
{
    [SerializeField] private BaseDamageableObject sourceDamage;
    // private DamageStats attackStats;
    // public void SetAttackDamage(DamageStats attackStats) => this.attackStats = attackStats;

    private DamageModifier calculatedDamage;
    public void SetAttackDamage(DamageModifier damage) => this.calculatedDamage = damage;

    [SerializeField] private EntityComponent sourceEntity;
    public void SetSourceDamage(EntityComponent entity) => this.sourceEntity = entity;


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseDamageableObject damageableObject))
        {
            damageableObject.OnGetHit(DamageHandler.Processor(sourceEntity.characterStats, calculatedDamage));
            // sourceEntity.stateHandler.OnHitTarget?.Invoke(damageableObject.GetComponent<EntityComponent>());
        }
    }


    public void DoDamage(DamageModifier damage)
    {
        SetAttackDamage(damage);
        // StartCoroutine(TriggerDamageCollider());
        Timing.RunCoroutine(TriggerDamageCollider());
    }


    IEnumerator<float> TriggerDamageCollider()
    {
        gameObject.SetActive(true);
        // yield return new WaitForSeconds(0.1f);
        yield return Timing.WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
    
}
