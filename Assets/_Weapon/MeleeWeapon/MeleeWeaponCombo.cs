using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//single thread combo
public class WeaponCombo : MonoBehaviour
{

    public MeleeWeaponStateMachine weaponStateMachine;
    public List<AnimationClip> attackAnimationClips;

    void Update()
    {
        if(weaponStateMachine != null)
        {
            weaponStateMachine.Update();
            weaponStateMachine.LogicUpdate();
        }
    }

    public void SetWeaponStateHandler(CharacterStateHandler stateHandler) => weaponStateMachine.stateHandler = stateHandler;

    public void SetStateMachine(PlayerAnimationSystem animator)
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,animator.animationSystem ,attackAnimationClips);
    }

    public void SetEntityComponent(EntityComponent entity)
    {
        
    }
    
}
