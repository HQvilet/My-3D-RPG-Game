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
                    break;
                }
                
            }
            
        }
    }

    void OnPickUpItem(BasicItem item)
    {
        InventoryManager.Instance.AddItem(item.ItemData.ID ,34);
        Debug.Log("Add Item " + item.ItemData.Name);
        Destroy(item.gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.parent.position ,Radius);
    }
}
