using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponCombo : MonoBehaviour
{

    public MeleeWeaponStateMachine weaponStateMachine;

    // public WeaponStateHandler weaponStateHandler;

    public List<AnimationClip> AttackAnimationClips = new();

    void Awake()
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,Player.Instance.AnimationSystem.animationSystem ,AttackAnimationClips);
        SetWeaponStateHandler(null);
    }


    void Update()
    {
        if(weaponStateMachine != null)
        {
            weaponStateMachine.Update();
            weaponStateMachine.LogicUpdate();
        }
    }

    public void SetWeaponStateHandler(PlayerStateHandler stateHandler) => weaponStateMachine.stateHandler = Player.Instance.WeaponController;

    public void SetStateMachine(PlayerAnimationSystem animator)
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,animator.animationSystem ,AttackAnimationClips);
    }
    

    public void OnStateStartListen()
    {
        
    }

    public void OnStateStopListen()
    {

    }
}
