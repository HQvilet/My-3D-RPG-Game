using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MEC;
using UnityEngine;

[Serializable]
public struct MarkupTime
{
    public float timeStamp;
    public string methodName;
}


public class AOEHitBox2 : DamageHitbox
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
        Collider[] entities = Physics.OverlapSphere(transform.position, 3f);
        foreach (Collider collider in entities)
        {
            if (collider.TryGetComponent(out EntityComponent entity))
            {
                entity.effectModifier.GetDamage(40f);
            }
        }
    }

    public void BigExplode()
    {
        Collider[] entities = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider collider in entities)
        {
            if (collider.TryGetComponent(out EntityComponent entity))
            {
                entity.effectModifier.GetDamage(40f);
            }
        }
    }
}
