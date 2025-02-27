using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//abstract function for animation event override
public abstract class BaseWeaponUtilities : MonoBehaviour
{

    public UnityEvent<string> AnimationEvent;

    void Start()
    {
        AnimationEvent.AddListener(RelyActionOnEvent);
    }

    protected void RelyActionOnEvent(string eventName)
    {
        Type type = this.GetType();
        type.GetMethod(eventName).Invoke(this ,null);
    }

    public virtual void ActionEvent_1(){}
    public virtual void ActionEvent_2(){}
    public virtual void ActionEvent_3(){}
    public virtual void ActionEvent_4(){}
    public virtual void ActionEvent_5(){}
    public virtual void ActionEvent_6(){}
    public virtual void ActionEvent_7(){}
    public virtual void ActionEvent_8(){}
}
