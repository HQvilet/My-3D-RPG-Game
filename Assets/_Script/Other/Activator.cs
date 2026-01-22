using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
    public UnityEvent onTriggeEnterEvent;
    public UnityEvent onTriggerExitEvent;

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerBehaviourHandler>())
            onTriggeEnterEvent?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerBehaviourHandler>())
            onTriggeEnterEvent?.Invoke();
    }
}
