using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class DamageHitbox : MonoBehaviour
{
    [SerializeField] private BaseDamageableObject sourceDamage;
    // private DamageStats attackStats;
    // public void SetAttackDamage(DamageStats attackStats) => this.attackStats = attackStats;

    private DamageModifier calculatedDamage;
    public void SetAttackDamage(DamageModifier damage) => this.calculatedDamage = damage;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out BaseDamageableObject damageableObject))
        {
            damageableObject.OnGetHit(calculatedDamage);
        }
    }

    // public void DoDamageTrigger(DamageModifier damage)
    // {
    //     SetAttackDamage(damage);
    //     StartCoroutine(TriggerDamageCollider());
    // }

    // IEnumerator TriggerDamageCollider()
    // {
    //     gameObject.SetActive(true);
    //     yield return new WaitForSeconds(0.1f);
    //     gameObject.SetActive(false);
    // }
    
}
