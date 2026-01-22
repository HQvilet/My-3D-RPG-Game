using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEditor.Rendering;
using UnityEngine;




public class BaseEffectModifier : MonoBehaviour
{

    [SerializeField] BaseDamageableObject damageableObject;

    // Handle object effects status more effective than hard-coded boolen check
    public HashSet<string> effects = new();
    
    public bool isInvincible;
    public bool canDamage = true;
    public float invincibleTime;
    public void SetInvincibleTime(float time)
    {
        isInvincible = true;
        invincibleTime = Mathf.Max(invincibleTime, time);
    }

    void Update()
    {
        if(invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime <= 0)
                isInvincible = false;                
        }
        
    }

    public void AddEffect(string effect)
    {
        if (!effects.Add(effect))
        {
            Debug.Log("Effect name " + effect + " existed.");
        }
    }

    public void RemoveEffect(string effect)
    {
        if (!effects.Remove(effect))
        {
            Debug.Log("No effect name " + effect + " found.");
        }
    }

    // On efffect target callbacks 
    public bool AllowDamage(float dmg, EntityComponent sourceDamage, DmgType type = DmgType.NONE)
    {
        if(effects.Contains("Parrying"))
        {
            if(Vector3.Dot((sourceDamage.transform.position - transform.position).normalized, transform.forward) > 0.25f)
            {
                return false;
            }            
        }

        if(isInvincible)
            return false;

        if(!canDamage)
            return false;

        return true;
    }

    public Action<float, EntityComponent, DmgType> OnTakeDamage;
    public Action<float, EntityComponent, DmgType> OnGetHit;

    public void GetDamage(float dmg, EntityComponent sourceDamage, DmgType type = DmgType.NONE)
    {
        if(AllowDamage(dmg, sourceDamage, type))
        {
            damageableObject.OnTakeDamage(dmg, type);
            OnTakeDamage?.Invoke(dmg, sourceDamage, type);            
        }
        OnGetHit?.Invoke(dmg, sourceDamage, type);

    }

    public void GetDamage(List<DamageVerifier> damages)
    {
        damages.ForEach(dmg =>
        {
           damageableObject.OnTakeDamage(dmg.amount, dmg.type); 
        });
    }

}
