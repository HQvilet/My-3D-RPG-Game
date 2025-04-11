using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;
    [SerializeField] private LayerMask layer;

    public Action<Transform> OnDetectedEnemy;
    Collider[] colliders;

    void Update()
    {
        colliders = Physics.OverlapSphere(transform.parent.position ,Radius ,layer);
        // if(colliders.Count() > 0)
        // {
        //     Collider collideInfo = colliders[0];
        //     if(collideInfo.TryGetComponent(out SimpleEnemy _obj))
        //         OnDetectedEnemy?.Invoke(_obj.transform);
        // }
        Transform neariestEnemy = null;
        foreach(Collider collider in colliders)
        {
            if(collider.TryGetComponent(out SimpleEnemy _obj))
            {
                if(neariestEnemy == null)
                    neariestEnemy = _obj.transform;
                else if(Vector3.Distance(neariestEnemy.position ,transform.position) > Vector3.Distance(_obj.transform.position ,transform.position))
                    neariestEnemy = _obj.transform;
            }
        }   
        if(neariestEnemy != null)
            OnDetectedEnemy?.Invoke(neariestEnemy);
            
    }

    public void GetNearestEnemy()
    {
        // foreach(Collider)
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
