using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ActionStateConfig
{
    public AnimationClip animationClip;
    public float _timeBufferPercent;
    public bool AllowToInterupt;

}

public class ActionState :State
{

    private AnimationSystem animationSystem;

    private Animator animator;
    private int animationName;

    private MeleeWeaponStateMachine stateMachine;
    AnimationClip stateAnimation;
    public ActionState(MeleeWeaponStateMachine stateMachine ,AnimationClip anim)
    {
        this.stateMachine = stateMachine;
        this._duration = anim.length;
        this.animationSystem = stateMachine.animator;
        this.stateAnimation = anim;
        this.animationName = Animator.StringToHash(anim.name);
        this.animator = stateMachine.animator.m_animator;
    }

    float _duration;
    float _duration_count;
    // public float _timeBuffer = 0.6f;
    public bool AllowToChange = false;
    bool CallbackOnce = true;

    public override void Enter()
    {
        ResetState();
        animationSystem.PlayOneShot(stateAnimation);
        // animator.CrossFade(animationName ,0.0f);
    }

    public override void Update()
    {
        _duration_count -= Time.deltaTime;
        if(_duration_count <= 0.25f && CallbackOnce)
        {
            AllowToChange = true;
            CallbackOnce = false;
            stateMachine.stateHandler.OnMeleeCompletedState?.Invoke();
        }
    }

    public override void Exit()
    {
        
    }

    private void ResetState()
    {
        AllowToChange = false;
        CallbackOnce = true;
        _duration_count = _duration;
    }
}

public class MeleeWeaponStateMachine : StateMachine
{    
    List<ActionState> WeaponComboStates = new List<ActionState>();
    private int MaxCombo;

    //Event
    public PlayerStateHandler stateHandler;

    public AnimationSystem animator;

    public MeleeWeaponStateMachine(WeaponCombo meleeCombo ,AnimationSystem animationSystem ,List<AnimationClip> clips)
    {
        MaxCombo = clips.Count;
        animator = animationSystem;
        SetUpActionChain(clips);
    }

    public void SetUpActionChain(List<AnimationClip> clips)
    {
        foreach(AnimationClip clip in clips)
            WeaponComboStates.Add(new ActionState(this ,clip));
    }

    private ActionState currentActionState;
    private int currentIndex = -1;
    // private float _timeBuffer;
    private float _resetStateBuffer = 4f;
    private float _currentResetStateTime;

    public void TriggerAttack()
    {
        if(currentActionState == null)
        {
            PerformAttack();
            return;
        }
        if(currentActionState.AllowToChange)
        {
            PerformAttack();
            return;
        }
    }

    public void LogicUpdate()
    {
        _currentResetStateTime -= Time.deltaTime;

        if(currentActionState == null) return;

        if(_currentResetStateTime <= 0)
            ResetCombo();

    }

    void PerformAttack()
    {
        if(!stateHandler.CanAttack)
            return;

        _currentResetStateTime = _resetStateBuffer;
        currentIndex++;
        if(currentIndex >= MaxCombo)
        {
            stateHandler.OnMeleeFinishedCombo?.Invoke();
            ResetCombo();
            return;
        }
        currentActionState = WeaponComboStates[currentIndex];
        ChangeState(currentActionState);
        stateHandler.OnMeleePerformed?.Invoke();
        
    }


    public void ResetCombo()
    {
        currentIndex = -1;
        currentActionState = null;
    }
}
