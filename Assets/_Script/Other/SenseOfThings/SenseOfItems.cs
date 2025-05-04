using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class SenseOfItems : MonoBehaviour
{
    [SerializeField] private float Radius = 10f;
    [SerializeField] private LayerMask layer;

    void Awake()
    {
        Bus<OnCollectEvent>.AddRegister(RES);
    }

    private void RES(OnCollectEvent @event)
    {
        Debug.Log(@event.item);
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.parent.position ,Radius ,layer);
        foreach(Collider collideInfo in colliders)
        {
            if(collideInfo.TryGetComponent(out BasicItem item))
            {
                //Send Data to UI
                if(InputDataHandler.Instance.PerformedAnInteract) //or UI selected
                {
                    OnPickUpItem(item);
                    Bus<OnCollectEvent>.Raise(new OnCollectEvent("asd" ,1));
                    break;
                }
                
            }
            
        }
    }

    void OnPickUpItem(BasicItem item)
    {
        InventoryManager.Instance.AddItemByCategories(item.ItemData ,1);
        Debug.Log("Add Item " + item.ItemData.Name);
        Destroy(item.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
