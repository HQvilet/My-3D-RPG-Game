using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public abstract class EventListener<TEventChannel ,TEventType> : MonoBehaviour where TEventChannel : EventChannel<TEventType> 
{

    [Header("Listen to Event Channels")]
    [SerializeField] protected TEventChannel EventChannel;
    [Tooltip("Responds to receiving signal from Event Channel")]
    [SerializeField] protected UnityEvent<TEventType> Response;
    protected virtual void OnEnable()
    {
        if (EventChannel != null)
        {
            EventChannel.OnEventRaised += OnEventRaised;
        }
    }
    protected virtual void OnDisable()
    {
        if (EventChannel != null)
        {
            EventChannel.OnEventRaised -= OnEventRaised;
        }
    }
    public void OnEventRaised(TEventType evt)
    {
        Response?.Invoke(evt);
    }
}