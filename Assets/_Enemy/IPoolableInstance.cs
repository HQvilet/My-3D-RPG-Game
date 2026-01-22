using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolableInstance : MonoBehaviour
{
    public int poolID;
    public IEntityPoolable entity;
    void Awake()
    {
        entity = GetComponent<IEntityPoolable>();
    }
}
