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
        if (InputDataHandler.Instance.PerformedAnInteract)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.parent.position ,Radius ,layer);
            foreach(Collider collideInfo in colliders)
            {
                if(collideInfo.TryGetComponent(out BasicItem item))
                {
                    //Send Data to UI
                    OnPickUpItem(item);
                    break;
                    
                }else if(collideInfo.TryGetComponent(out CustomItem c_item))
                {
                    OnPickUpArmourItem(c_item);
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

    void OnPickUpArmourItem(CustomItem item)
    {
        InventoryManager.Instance.AddArmourItem(item.armourRef);
        Destroy(item.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
