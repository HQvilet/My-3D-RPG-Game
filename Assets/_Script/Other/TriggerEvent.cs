

using UnityEngine;
using System.Reflection;

public class EventHandler
{
    public static void RelyActionOnEvent(object obj, string eventName)
    {
        MethodInfo method = obj.GetType().GetMethod(eventName);
        if (method != null)
        {
            method.Invoke(obj, null);
        }
        else
        {
            Debug.Log($"No method found {eventName} on {obj.GetType().ToString()}");
        }
    }
}