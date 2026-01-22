using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroyObject : MonoBehaviour
{
    [SerializeField] float timeToLive; 

    void Update()
    {
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
