using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class SenseOfNPC : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;
    [SerializeField] private LayerMask layer;

    void Update()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
