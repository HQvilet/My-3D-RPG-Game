using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;


public interface IEvent{}

public static class Bus<T> where T : IEvent
{

    public static Action<T> _event;
    public static void AddRegister(Action<T> onEvent) => _event += onEvent;
    public static void RemoveRegister(Action<T> onEvent) => _event -= onEvent;
    public static void Raise([CanBeNull] T arg) => _event?.Invoke(arg);

}

public class OnCollectEvent : IEvent
{
    public OnCollectEvent(string a ,int b)
    {
        item = a;
        amount = b;
    }
    public string item;
    public int amount;
}

