using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Bus<T> 
{

    public Action<T> _event;
    public void AddRegister(Action<T> onEvent) => _event += onEvent;
    public void RemoveRegister(Action<T> onEvent) => _event -= onEvent;
    public void Raise([CanBeNull]T arg) => _event?.Invoke(arg);

}

public class Bus
{
    Bus<int > a = new Bus<int>();
    public event Action _event;
    public void AddRegister(Action onEvent) => _event += onEvent;
    public void RemoveRegister(Action onEvent) => _event -= onEvent;
    public void Raise() => _event?.Invoke();

}