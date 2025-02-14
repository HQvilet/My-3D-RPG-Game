using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActionState :State
{
    float _duration = 2f;
    float _duration_count;
    public float _timeBuffer = 1f;
    public bool AllowToChange = false;
    Animator animator;
    public ActionState(Player player ,string anim)
    {
        this.anim_str = anim;
        // animator = player.animator;
    }   

    string anim_str;

    public override void Enter()
    {
        //Trigger Animation
        Debug.Log("Do " +  anim_str);
        ResetState();
        
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

public class MeleeWeaponStateMachine :StateMachine
{    
    List<ActionState> WeaponComboStates = new List<ActionState>();
    int maxCombo;
    string anim_str;

    public MeleeWeaponStateMachine(WeaponCombo meleeCombo ,Player player)
    {
        maxCombo = meleeCombo.MaxCombo;
        anim_str = meleeCombo.WeaponAnimation;

        SetUpActionChain(player);
    }


    ActionState currentActionState = null;
    int currentIndex = -1;
    float _timeBuffer;

    public void TriggerNextCombo()
    {
        

        if(currentActionState == null)
        {
            StateTransition();
        }
        // else if(currentActionState.AllowToChange)
        // {
        //     StateTransition();
        // }
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

    public void SetUpActionChain(Player player)
    {
        for(int i = 0; i < maxCombo; i++)
            WeaponComboStates.Add(new ActionState(player ,anim_str + i));
    }

    public void ResetCombo()
    {
        currentIndex = -1;
        currentActionState = null;
    }
}
