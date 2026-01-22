using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TargetableProjectile : DamageUnit
{
    public float sec = 1.5f;
    public float angle = 75;

    public LayerMask groundLayer;
    public DamageUnit hitGroundEffect;

    Vector3 moveDirection;        
    float distance, vy, g;

    Vector3 startPosition, target;

    void Start()
    {
        startPosition = transform.position;
        sec += Random.Range(-0.1f, 0.1f);
        angle += Random.Range(-8, 8);
        Calculate();
        transform.DORotate(moveDirection * 360, 0.7f, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public void SetTarget(Transform target)
    {
        this.target = target.position + MyUtils.RandomizeVector3() * 2.5f;
    }

    public void Calculate()
    {
        moveDirection = target - startPosition;
        distance = moveDirection.magnitude;

        moveDirection.Normalize();
        vy = distance / sec * Mathf.Tan(angle * Mathf.Deg2Rad);
        g = 2 * distance * Mathf.Tan(angle * Mathf.Deg2Rad) / (sec * sec);
    }

    void FixedUpdate()
    {
        Vector3 vel = moveDirection * distance / sec;
        vy -= Time.fixedDeltaTime * g;
        transform.position += vel * Time.fixedDeltaTime + Vector3.up * vy * Time.fixedDeltaTime;
    }

    void Update()
    {
        if(Physics.CheckSphere(transform.position, 0.5f, groundLayer))
        {
            if(hitGroundEffect != null)
            {
                Instantiate(hitGroundEffect, transform.position, Quaternion.identity)
                    .SetDamageData(sourceDamage);
            }
            
            Destroy(gameObject);
        }
    }
}
