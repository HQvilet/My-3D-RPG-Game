using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDamage : DamageUnit
{
    public float range;
    public DmgType dmgType;
    public float multiplier;

    public Func<EntityComponent> damageResolver = null;
    public float delay = 0f;

    CountdownTimer delayTimer;

    void Start()
    {
        delayTimer = new CountdownTimer(delay);
        delayTimer.Start();
        delayTimer.OnTimerFinish += () =>
        {
            if(sourceDamage != null)
            {
                List<EntityComponent> entities = EnvironmentHelper.Instance.GetAllEnemiesNearby(transform.position, range, true);
                entities.ForEach(entity =>
                {
                    if(DamageHandler.CanDamageThisEntity(sourceDamage, entity))
                        entity.effectModifier.GetDamage(sourceDamage.characterStats.Atk * multiplier, sourceDamage, dmgType);
                });
            }
        };

    }

    void Update()
    {
        delayTimer.Tick(Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
