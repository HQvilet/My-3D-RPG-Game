using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponCombo : MonoBehaviour
{
    public Player playerRef;

    public string WeaponAnimation;
    public int MaxCombo;

    public MeleeWeaponStateMachine weaponStateMachine;

    public List<AnimationClip> AttackAnimationClips = new();

    void Start()
    {
        weaponStateMachine = new MeleeWeaponStateMachine(this ,Player.Instance ,AttackAnimationClips);
    }

    void Update()
    {
        if(weaponStateMachine != null)
        {
            weaponStateMachine.Update();
            weaponStateMachine.LogicUpdate();            
        }
    }
}
