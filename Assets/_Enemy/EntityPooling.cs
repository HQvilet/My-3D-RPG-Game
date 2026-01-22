using System.Collections;
using Type = System.Type;
using System.Collections.Generic;
using UnityEngine;
using Zombie;
using Archer;
using Unity.VisualScripting;

public interface IEntityPoolable
{
    void BackToPool();
    void OutFromPool();
}

// pooling by type
public class EntityPooling : Singleton<EntityPooling>
{
    public Dictionary<int, Queue<GameObject>> entityPool = new();

    public void AddToPool(GameObject instance)
    {
        if(!instance.TryGetComponent<PoolableInstance>(out PoolableInstance poolableInstance))
        {
            Debug.Log("Cannot pool non-poolable object");
            return;
        }

        if(!entityPool.ContainsKey(poolableInstance.poolID))
        {
            entityPool.Add(poolableInstance.poolID, new());
        }

        poolableInstance.entity.BackToPool();
        entityPool[poolableInstance.poolID].Enqueue(instance);
    }

    public void AddToPool(PoolableInstance poolableInstance)
    {
        if(!entityPool.ContainsKey(poolableInstance.poolID))
        {
            entityPool.Add(poolableInstance.poolID, new());
        }
        
        poolableInstance.entity.BackToPool();
        entityPool[poolableInstance.poolID].Enqueue(poolableInstance.gameObject);
    }

    public GameObject GetOrInstantiateGameObject(GameObject prefap, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if(prefap.TryGetComponent(out PoolableInstance poolableInstance))
        {
            if(!entityPool.ContainsKey(poolableInstance.poolID))
            {
                entityPool.Add(poolableInstance.poolID, new());
            }
                        
            if(entityPool[poolableInstance.poolID].Count <= 0)
            {
                GameObject a = Instantiate(prefap, position, rotation, parent);
                AddToPool(a);
            }

            GameObject dequeueObject = entityPool[poolableInstance.poolID].Dequeue();
            dequeueObject.transform.position = position;
            dequeueObject.transform.rotation = rotation;
            dequeueObject.transform.SetParent(parent);
            dequeueObject.GetComponent<IEntityPoolable>().OutFromPool();
            return dequeueObject;
        }
        else
        {
            return null;   
        }
    }

    public void OnDestroy()
    {
        foreach(var q in entityPool.Values)
        {
            q.Clear();
        }
    }
}
