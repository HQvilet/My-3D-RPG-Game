using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class SystemInputManager : Singleton<SystemInputManager>
{
    [HideInInspector] public PlayerInputAction SystemInput;

    protected override void Awake()
    {
        SystemInput = new PlayerInputAction();
        base.Awake();
        
    }
}