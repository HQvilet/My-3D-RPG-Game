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

    public void SerilizeEffectSource(DamageStats stats)
    {
        if(stats.physicalDamage > 0) OnTakePhysicDamage?.Invoke(stats.physicalDamage);

        if(stats.fireDamage > 0) OnTakeFireDamage?.Invoke(stats.fireDamage);

        if(stats.knockBack > 0) OnGetKnockBack?.Invoke(stats.knockBack);
    }



}
