using System.Collections;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowProjectile : DamageUnit
{
    public float speed;
    public float multiplier = 0.1f;
    public bool destroyOnDamage = true;
    public bool useUpperDistortion;
    public float curveStrenght;
    Transform target;
    Vector3 targetPositon;
    public GameObject hitEffect;

    public void SetDefault()
    {
        
    }

    public void SetTarget(Transform targetTransform, Vector3 parentForward)
    {
        target = targetTransform;
        if(targetTransform == null)
            this.targetPositon = transform.position + parentForward * 10f;
        else
            this.targetPositon = targetTransform.position;
        initialDirection = (this.targetPositon - transform.position).normalized;
        lerped = Random.onUnitSphere;
        if(useUpperDistortion)
            lerped.y = Mathf.Abs(lerped.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<EntityComponent>(out EntityComponent entity))
        {
            if(DamageHandler.CanDamageThisEntity(sourceDamage, entity))
            {
                entity.effectModifier.GetDamage(sourceDamage.characterStats.Atk * multiplier, sourceDamage, DmgType.MAGIC);

                if(hitEffect)
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                if(destroyOnDamage)
                    Destroy(this.gameObject);
            }
        }
    }
    
    Vector3 lerped;
    Vector3 initialDirection;
    Vector3 lastDirection;
    void Update()
    {
        if(target != null)
        {
            lastDirection = (target.position + Vector3.up * 0.9f - transform.position).normalized;
            lerped = Vector3.Lerp(lerped, lastDirection, curveStrenght).normalized;
            transform.position += lerped * speed * Time.deltaTime;
        }
        else
        {
            lastDirection = (targetPositon + Vector3.up * 0.9f - transform.position).normalized;
            lerped = Vector3.Lerp(lerped, lastDirection, curveStrenght).normalized;
            transform.position += lerped * speed * Time.deltaTime;
        }
    }

}
