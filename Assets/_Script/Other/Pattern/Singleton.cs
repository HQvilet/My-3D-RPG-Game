using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance{get; protected set;}

    protected virtual void Awake()
    {
        if(Instance == null)
            Instance = this as T;
        else
        {
            Debug.Log("Instance has already been exsisted .Destroy game object");
            Destroy(gameObject);
        }
    }
}
