using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponCombo : MonoBehaviour
{
    public Player playerRef;

    public string WeaponAnimation;
    public int MaxCombo;

    MeleeWeaponStateMachine weaponStateMachine;

    void Start()
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,playerRef);
    }

    void Update()
    {
        weaponStateMachine.Update();
        weaponStateMachine.LogicUpdate();
        
        if(Input.GetKeyDown(KeyCode.Return))
        {
            weaponStateMachine.TriggerNextCombo();
        }
    }
}
