using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEditor.Rendering;
using UnityEngine;


// Handle all character's effects
// Caculate damage
// classify damage

public enum EffectType
{
    BURN, SLOW, POISON, FREEZE
}

public class BaseEffectModifier : MonoBehaviour
{
    // Debuff state handler
    public bool isOnFire;
    public bool isStunning;
    public bool isGetKnockback;

    [SerializeField] BaseDamageableObject damageableObject;

    public HashSet<string> effects = new(); // Handle object effects status more effective than hard-coded boolen check
    public void AddEffect(string effect)
    {
        if (!effects.Add(effect))
        {
            Debug.Log("Effect name " + effect + " existed.");
        }
    }

    public void RemoveEffect(string effect)
    {
        if (effects.Remove(effect))
        {
            Debug.Log("No effect name " + effect + " found.");
        }
    }

    // On efffect target callbacks

    public Action<float> OnTakePhysicDamage;
    public Action<float, float> OnTakeFireDamage;
    public Action<float> OnGetKnockBack;

    public Action<float> OnGetDoTDamage; //time between


    //mixed damage 
    public void SerilizeEffectSource(DamageModifier damage)
    {
        if (damage.physicalDamage > 0) OnTakePhysicDamage?.Invoke(damage.physicalDamage);

        if (damage.fireDamage > 0) OnTakeFireDamage?.Invoke(damage.fireDamage, 2);

        if (damage.knockBack > 0) OnGetKnockBack?.Invoke(damage.knockBack);
    }

    public void GetDamage(float dmg, string type = "")
    {
        damageableObject.OnTakeDamage(dmg, DmgType.NONE);
    }

    public void GetDamage(DamageModifier damageModifier)
    {

    }

    // public void GetDoT(float dmg, string type, float timeBetween)
    // {
    //     if(!currentDoTEffeects.ContainsKey(type))
    //         currentDoTEffeects.Add(type, Timing.RunCoroutine(DoTEnumerator(dmg, timeBetween)));
    // }

    // IEnumerator<float> DoTEnumerator(float dmg, float timeBetween)
    // {
    //     float time = 100f; // effect max live time 
    //     while (time >= 0f)
    //     {
    //         time -= Time.deltaTime;
    //         damageableObject.OnTakeDamage(dmg);
    //         yield return Timing.WaitForSeconds(timeBetween);
    //     }
    // }

    // public void StopDoT(string type)
    // {
    //     if (currentDoTEffeects.TryGetValue(type, out CoroutineHandle coroutine))
    //     {
    //         Timing.KillCoroutines(coroutine);
    //         currentDoTEffeects.Remove(type);
    //     }
    // }
}
