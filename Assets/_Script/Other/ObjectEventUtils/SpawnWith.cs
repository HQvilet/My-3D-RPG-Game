using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWith : MonoBehaviour
{
    [SerializeField] GameObject obj;
    void Start()
    {
        if(obj != null)
            Instantiate(obj, transform.position, transform.rotation, null);
    }

}
