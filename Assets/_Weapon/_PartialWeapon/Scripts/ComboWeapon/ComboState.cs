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

public class ActionState
{
    private MeleeWeaponStateMachine stateMachine;
    AnimationClip stateAnimation;
    public ActionState(MeleeWeaponStateMachine stateMachine, AnimationClip anim)
    {
        this.stateMachine = stateMachine;

        this.stateAnimation = anim;
        this.animationHash = Animator.StringToHash(anim.name);
        
    }

    private int animationHash;
    float _duration;
    float _duration_count;
    public bool allowToChange = false;
    bool callbackOnce = true;

    public void Enter()
    {
        ResetState();
        this._duration = stateAnimation.length / stateMachine.animator.GetFloat("Attack Speed");
        stateMachine.stateHandler.IsAttacking = true;
        stateMachine.animator.CrossFade(animationHash, Time.deltaTime * 2.1f);
    }

    public void Update()
    {
        _duration_count -= Time.deltaTime;
        if(_duration_count <= _duration * 0.11f && callbackOnce)
        {
            allowToChange = true;
            callbackOnce = false;
            stateMachine.stateHandler.IsAttacking = false;
            stateMachine.stateHandler.OnMeleeCompletedState?.Invoke();
        }
    }

    public void Exit()
    {
        
    }

    private void ResetState()
    {
        allowToChange = false;
        callbackOnce = true;
        _duration_count = _duration;
    }
}

public class MeleeWeaponStateMachine
{    
    List<ActionState> WeaponComboStates = new();
    private int maxCombo;

    //Event
    public CharacterStateHandler stateHandler;
    public Animator animator;

    public MeleeWeaponStateMachine(WeaponCombo meleeCombo ,Animator animator ,List<AnimationClip> clips)
    {
        maxCombo = clips.Count;
        this.animator = animator;
        SetUpActionChain(clips);
    }

    public void SetUpActionChain(List<AnimationClip> clips)
    {
        foreach (AnimationClip clip in clips)
            WeaponComboStates.Add(new ActionState(this, clip));
    }

    private ActionState currentActionState;
    private int currentIndex = -1;
    
    private float _resetStateBuffer = 3f;
    private float _currentResetStateTime;

    public void TriggerAttack()
    {
        if(currentActionState == null)
        {
            PerformAttack();
            return;
        }
        if(currentActionState.allowToChange)
        {
            PerformAttack();
            return;
        }
    }

    public void Update()
    {
        currentActionState?.Update();
    }

    public void LogicUpdate()
    {
        _currentResetStateTime -= Time.deltaTime;

        if (currentActionState == null)
            return;

        if (_currentResetStateTime <= 0)
            ResetCombo();
    }

    void PerformAttack()
    {
        if(!stateHandler.AllowToInterupt)
            return;

        _currentResetStateTime = _resetStateBuffer;
        currentIndex++;
        if(currentIndex >= maxCombo)
        {
            stateHandler.OnMeleeFinishedCombo?.Invoke();
            ResetCombo();
            PerformAttack();
            return;
        }
        // currentActionState = WeaponComboStates[currentIndex];
        ChangeState(WeaponComboStates[currentIndex]);
        stateHandler.OnMeleePerformed?.Invoke();
    }

    void ChangeState(ActionState state)
    {
        if (currentActionState == state)
            return;
        currentActionState?.Exit();
        currentActionState = state;
        currentActionState.Enter();
    }

    public void ResetCombo()
    {
        currentIndex = -1;
        currentActionState = null;
    }
}
