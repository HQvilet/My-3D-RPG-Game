using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//single thread combo
public class WeaponCombo : MonoBehaviour
{

    public MeleeWeaponStateMachine weaponStateMachine;
    public List<AnimationClip> AttackAnimationClips;

    void Awake()
    {
        // weaponStateMachine = new MeleeWeaponStateMachine(this ,Player.Instance.AnimationSystem.animationSystem ,AttackAnimationClips);
        // SetWeaponStateHandler(null);
    }

    void Update()
    {
        if(weaponStateMachine != null)
        {
            weaponStateMachine.Update();
            weaponStateMachine.LogicUpdate();
        }
    }

    public void SetWeaponStateHandler(PlayerStateHandler stateHandler) => weaponStateMachine.stateHandler = stateHandler;

    public void SetStateMachine(PlayerAnimationSystem animator)
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,animator.animationSystem ,AttackAnimationClips);
    }

    public void SetEntityComponent(EntityComponent entity)
    {
        
    }
    
}
