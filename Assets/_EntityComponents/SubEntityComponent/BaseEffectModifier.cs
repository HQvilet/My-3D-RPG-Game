using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffectModifier : MonoBehaviour
{
    // Debuff state handler
    public bool isOnFire;
    public bool isStunning;
    public bool isGetKnockback;

    // On efffect target callbacks
    public Action<float> OnTakePhysicDamage;
    public Action<float> OnTakeFireDamage;
    public Action<float> OnGetKnockBack;

    public void SerilizeEffectSource(DamageModifier damage)
    {
        if(damage.physicalDamage > 0) OnTakePhysicDamage?.Invoke(damage.physicalDamage);

        if(damage.fireDamage > 0) OnTakeFireDamage?.Invoke(damage.fireDamage);

        if(damage.knockBack > 0) OnGetKnockBack?.Invoke(damage.knockBack);
    }

}
