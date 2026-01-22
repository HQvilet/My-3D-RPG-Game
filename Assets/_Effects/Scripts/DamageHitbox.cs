using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using System;


public abstract class DamageUnit : MonoBehaviour
{
    protected EntityComponent sourceDamage;

    public virtual void SetDamageData(EntityComponent sourceDamage)
    {
        this.sourceDamage = sourceDamage;
    }
}

public class DamageUnitWithEvent : DamageUnit
{
    public Action<DamageUnit,EntityComponent> OnEntityEnter;
    public Action<DamageUnit,EntityComponent> OnEntityStay;
    public Action<DamageUnit,EntityComponent> OnEntityExit;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent obj))
        {
            OnEntityEnter?.Invoke(this, obj);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent obj))
        {
            OnEntityStay?.Invoke(this, obj);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent obj))
        {
            OnEntityExit?.Invoke(this, obj);
        }
    }
}
