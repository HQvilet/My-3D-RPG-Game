using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SenseOfEnemy : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;
    [SerializeField] private LayerMask layer;

    public Action<Transform> OnDetectedEnemy;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.parent.position ,Radius ,layer);
        if(colliders.Count() > 0)
        {
            Collider collideInfo = colliders[0];
            if(collideInfo.TryGetComponent(out SimpleEnemy _obj))
                OnDetectedEnemy?.Invoke(_obj.transform);
        }
            
    }

    public void GetNearestEnemy()
    {

    }

    public void GetAllNearbyEnemy()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
