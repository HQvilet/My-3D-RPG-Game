using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ActionStateConfig
{
    public AnimationClip animationClip;
    public float _timeBuffer;
}
public class ActionState :State
{
    public float _timeBuffer = 0.6f;
    public bool AllowToChange = false;
    Player player;

    private Animator animator;
    public ActionState(Player player ,AnimationClip anim)
    {
        this._duration = anim.length;
        this.anim_str = anim.name;
        this.player = player;
    }  

    string anim_str;
    float _duration;
    float _duration_count;

    public override void Enter()
    {
        //Trigger Animation
        ResetState();
        player.PlayerMovement.playerAnimator.SetTrigger(anim_str);
    }

    public override void Update()
    {
        _duration_count -= Time.deltaTime;
        if(_duration_count <= 0f)
            AllowToChange = true;
    }

    private void ResetState()
    {
        AllowToChange = false;
        _duration_count = _duration;
    }
}

public class MeleeWeaponStateMachine : StateMachine
{    
    List<ActionState> WeaponComboStates = new List<ActionState>();
    int maxCombo;
    string anim_str;

    public MeleeWeaponStateMachine(WeaponCombo meleeCombo ,Player player ,List<AnimationClip> clips)
    {
        maxCombo = meleeCombo.MaxCombo;
        anim_str = meleeCombo.WeaponAnimation;

        SetUpActionChain(player ,clips);
    }

    private ActionState currentActionState;
    int currentIndex = -1;
    float _timeBuffer;

    public void TriggerNextCombo()
    {
        if(currentActionState == null)
        {
            StateTransition();
        }
        _timeBuffer = currentActionState._timeBuffer;
    }

    public void LogicUpdate()
    {
        _timeBuffer -= Time.deltaTime;

        if(currentActionState == null) return;

        if(currentActionState.AllowToChange && _timeBuffer > 0)
            StateTransition();
    }

    void StateTransition()
    {
        currentIndex++;
        if(currentIndex >= maxCombo)
        {
            ResetCombo();
            return;
        }
        currentActionState = WeaponComboStates[currentIndex];
        ChangeState(currentActionState);        
    }

    public void SetUpActionChain(Player player ,List<AnimationClip> clips)
    {
        for(int i = 0; i < maxCombo; i++)
            WeaponComboStates.Add(new ActionState(player ,clips[i]));
    }

    public void ResetCombo()
    {
        currentIndex = -1;
        currentActionState = null;
    }
}
