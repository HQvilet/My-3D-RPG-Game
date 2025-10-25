using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MEC;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem.iOS;

public class FreezeAOE : DamageHitbox
{

    public float liveTime;
    float timeBetween = 0.1f;

    Dictionary<BaseEffectModifier, CoroutineHandle> _objCoroutines = new();

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseEffectModifier effectModifier))
        {
            if (_objCoroutines.ContainsKey(effectModifier))
                return;
            
            _objCoroutines.Add(effectModifier, Timing.RunCoroutine(DealDoT(effectModifier, 2, timeBetween)));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BaseEffectModifier effectModifier))
        {
            StopEffectFromModify(effectModifier);
        }
    }
    
    void OnDestroy()
    {
        _objCoroutines.Keys.ToList().ForEach(modifier =>
        {
            Timing.KillCoroutines(_objCoroutines[modifier]);
        });
    }
    IEnumerator<float> DealDoT(BaseEffectModifier modifier, float dmg, float timeBetween, Action OnDOTStop = null)
    {
        float time = 100f; // effect max live time 
        while (time >= 0f)
        {
            time -= timeBetween;
            if (!modifier)
            {
                StopEffectFromModify(modifier);
            }
            modifier?.GetDamage(calculatedDamage.elementalDamage * sourceEntity.characterStats.Atk / 100);
            yield return Timing.WaitForSeconds(timeBetween);
        }
    }

    void StopEffectFromModify(BaseEffectModifier modifier)
    {
        Timing.KillCoroutines(_objCoroutines[modifier]);
        _objCoroutines.Remove(modifier);
    }


    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime < 0)
            Destroy(this.gameObject);
    }
}
