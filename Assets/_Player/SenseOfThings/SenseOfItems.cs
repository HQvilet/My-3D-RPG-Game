using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.InputSystem;

public class SenseOfInteractable : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask layer;
    [SerializeField] InputAction interactAction;

    void Start()
    {
        interactAction.Enable();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.parent.position ,radius ,layer);
        foreach(Collider collideInfo in colliders)
        {
            if(collideInfo.TryGetComponent(out BasicItem item))
            {
                if(InventoryManager.Instance.TryAddItemByCategories(item._itemData ,1))
                    Destroy(item.gameObject);
            }
        }
        if(interactAction.WasPerformedThisFrame())
        {
            foreach(Collider collideInfo in colliders)
            {
                if(collideInfo.TryGetComponent(out IInteractable i))
                {
                    i.Interact();
                    break;
                }
            }
        }
        
    }

    void OnPickUpItem(ItemData item)
    {
        InventoryManager.Instance.TryAddItemByCategories(item ,1);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.parent.position ,radius);
    }
}
