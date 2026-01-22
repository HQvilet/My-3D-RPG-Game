using System;
using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;


public class RedVelvatAOE : DamageUnit
{
    [SerializeField] List<MarkupTime> markups;

    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        Timing.RunCoroutine(TimelineEventTrigger());
    }

    IEnumerator<float> TimelineEventTrigger()
    {
        float prevStamp = 0f;
        foreach (MarkupTime markup in markups)
        {
            yield return Timing.WaitForSeconds(Math.Max(markup.timeStamp - prevStamp, 0f) * Time.timeScale);
            this.Invoke(markup.methodName, 0f);
            prevStamp = markup.timeStamp;
        }
        yield return 0;
    }

    public void SmallExplode()
    {
        EnvironmentHelper.Instance.GetAllEnemiesNearby(transform.position, 3f).ForEach(entity =>
        {
            entity.effectModifier.GetDamage(10f, sourceDamage);
        });
    }

    public void BigExplode()
    {
        EnvironmentHelper.Instance.GetAllEnemiesNearby(transform.position, 5f).ForEach(entity =>
        {
            entity.effectModifier.GetDamage(20f, sourceDamage);
        });
    }
}
