using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MEC;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : DamageUnit
{

    public FollowProjectile projectile;
    public float spawnRate = 1f;

    float timer;
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = spawnRate + Random.Range(0, 0.2f);
            DoSpawn();
        }
    }

    public void DoSpawn()
    {
        List<EntityComponent> entities = EnvironmentHelper.Instance.GetAllEnemiesNearby(transform.position, 18f)
            .Where(e => Vector3.Dot(e.transform.position - transform.position, transform.forward) > 0.25f)
            .ToList();

        if(entities.Count <= 0)
            return;

        Timing.RunCoroutine(SpawnWithDelay(entities).CancelWith(gameObject));
    }

    IEnumerator<float> SpawnWithDelay(List<EntityComponent> entities)
    {
        int count = Random.Range(10 ,13);
        for(int i = 0; i < count; ++i)
        {
            EntityComponent target = entities[Random.Range(0, entities.Count - 1)];
            if(target != null && target.gameObject.activeInHierarchy)
            {
                var m_projectile = Instantiate(projectile, transform.position, Quaternion.identity);
                m_projectile.SetDamageData(sourceDamage);
                m_projectile.SetTarget(target.transform, transform.forward);
                yield return Timing.WaitForSeconds(Random.Range(0.2f, 0.35f));
            }
            
        }
    }
}
