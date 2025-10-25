using UnityEngine;
using MEC;
using System.Collections.Generic;

public class SlashHitBox : DamageHitbox
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent obj))
        {
            obj.damageableObject.OnGetHit(DamageHandler.Processor(sourceEntity.characterStats, calculatedDamage));
            sourceEntity.stateHandler.OnHitTarget?.Invoke(obj);
        }
    }

    public void DoDamage(DamageModifier damage)
    {
        SetAttackDamage(damage);
        Timing.RunCoroutine(TriggerDamageCollider());
    }


    IEnumerator<float> TriggerDamageCollider()
    {
        gameObject.SetActive(true);
        yield return Timing.WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}