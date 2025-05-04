using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventChannel<T> : ScriptableObject
{
    [Tooltip("The action to perform; Listeners subscribe to this UnityAction")]
    public UnityAction<T> OnEventRaised;

    public void RaiseEvent(T parameter)
    {
        OnEventRaised?.Invoke(parameter);
    }
}

[System.Serializable]
public struct Empty
{

}

[CreateAssetMenu(menuName = "Events/Collect event channel")]
public class CollectEventChannel : EventChannel<Empty>
{

}


