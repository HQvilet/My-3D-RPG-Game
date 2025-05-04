using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public interface IPool
{
    public void PoolObject(IPoolingObject _obj);
    public void SpawnObject(IPoolingObject _obj);
}
public class Pooling : MonoBehaviour ,IPool
{

    // Map of pooling object
    // 
    Queue<IPoolingObject> Pool = new Queue<IPoolingObject>();

    Dictionary<int ,Queue<IPoolingObject>> MyPool = new Dictionary<int, Queue<IPoolingObject>>();

    public void PoolObject(IPoolingObject _obj)
    {
        
    }

    public void SpawnObject(IPoolingObject _obj)
    {
        
    }

    // public IPoolingObject GetObjectFromPool()
    // {

    // }



}


public interface IPoolingObject
{
    public void Born();
    public void BackToPool();
}

