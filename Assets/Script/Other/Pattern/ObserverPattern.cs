using System;
using System.Collections.Generic;


public interface ISubject
{
    public void Notify();
    public void Attach(IObserver observer);
    public void Detach(IObserver observer);
}

public interface IObserver
{
    public void OnNotify();
}

public abstract class Observer : IObserver
{
    public void OnNotify() {}
}

public class Subject :ISubject
{
    List<IObserver> Observers = new List<IObserver>();
    public void Notify()
    {
        Observers.ForEach(observer => observer.OnNotify());
    }

    public void Attach(IObserver observer) => Observers.Add(observer);

    public void Detach(IObserver observer) => Observers.Remove(observer); 
}
