using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnEnable : MonoBehaviour
{
    public GameObject obj;
    void OnEnable()
    {
        if(obj)
            Instantiate(obj, transform.position, transform.rotation);
    }
}
