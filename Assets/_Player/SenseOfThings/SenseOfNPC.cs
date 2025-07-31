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
        if(InputDataHandler.Instance.PerformedAnInteract)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.parent.position ,Radius ,layer);
            if(colliders.Count() > 0)
            {
                Collider collideInfo = colliders[0];
                if(collideInfo.TryGetComponent(out IInteractable _obj))
                {
                    // if(!_obj.HasInteracted)
                        _obj.Interact();
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
