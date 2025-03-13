using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class DamageHitbox : MonoBehaviour
{
    [SerializeField] private BaseDamageableObject sourceDamage;
    private DamageStats attackStats;
    public void SetAttackDamage(DamageStats attackStats) => this.attackStats = attackStats;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out BaseDamageableObject damageableObject))
        {
            damageableObject.OnGetHit(attackStats);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        
    }
}
