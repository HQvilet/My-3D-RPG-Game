using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;


//abstract function for animation event override
public abstract class BaseWeaponUtilities : MonoBehaviour
{

    public void RelyActionOnEvent(string eventName)
    {
        MethodInfo method = this.GetType().GetMethod(eventName);
        if(method != null)
        {
            method.Invoke(this ,null);
        }
        else
        {
            Debug.Log("No method found " + eventName);
        }
    }
}
