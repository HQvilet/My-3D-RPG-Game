using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEHitBox : DamageHitbox
{
    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out BaseDamageableObject damageableObject))
        {
            damageableObject.OnGetHit(DamageHandler.Processor(sourceEntity.characterStats, calculatedDamage));
            sourceEntity.stateHandler.OnHitTarget?.Invoke(damageableObject.GetComponent<EntityComponent>());
        }
    }
}
