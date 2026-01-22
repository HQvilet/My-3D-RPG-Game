using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;

public class FreezeAOE : DamageUnit
{

    public float liveTime;
    public float timeBetween = 0.1f;
    public float DPS;
    public DmgType dmgType;

    Dictionary<BaseEffectModifier, float> _timedObjects = new();

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent entity))
        {
            Debug.Log(sourceDamage);
            if(DamageHandler.CanDamageThisEntity(sourceDamage, entity))
            {
                if(!_timedObjects.ContainsKey(entity.effectModifier))
                    _timedObjects.Add(entity.effectModifier, 0);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out EntityComponent entity))
        {
            if(DamageHandler.CanDamageThisEntity(sourceDamage, entity))
            {
                if(_timedObjects.ContainsKey(entity.effectModifier))
                    _timedObjects.Remove(entity.effectModifier);
            }
        }
    }

    

    void Update()
    {
        BaseEffectModifier[] effectors = _timedObjects.Keys.ToArray();
        foreach(var effector in effectors)
        {
            _timedObjects[effector] -= Time.deltaTime;
            if(_timedObjects[effector] < 0f)
            {
                _timedObjects[effector] = timeBetween;
                effector.GetDamage(DPS, sourceDamage, dmgType);
            }
        }

        liveTime -= Time.deltaTime;
        if (liveTime < 0)
            Destroy(this.gameObject);
    }
}
