using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;

// public class PlayerDashState : GroundingState
// {
//     public PlayerDashState(PlayerMovementHandler player ,PlayerStateMachine stateMachine) : base(player ,stateMachine)
//     {

//     }
    

//     public override void Enter()
//     {
//         base.Enter();
//         Debug.Log("Player Dashing");
//         Timing.RunCoroutine(Dash());
//     }

//     public override void Update()
//     {
//         base.Update();
//     }

//     public override void UpdateLogic()
//     {

//     }

//     IEnumerator<float> Dash()
//     {
//         player.movementUtilities.DoMove(player.transform.forward ,30);
//         yield return Timing.WaitForSeconds(0.5f);
//         DoTransition();
//     }

//     private void DoTransition()
//     {
//         if(player.colliderDetection.IsGrounded)
//         {
//             if(!player.Input.HasMotionInput)
//                 stateMachine.ChangeState(stateMachine.idlingState);
//             else
//                 stateMachine.ChangeState(stateMachine.runningState);
//         }
//     }
    
//     public override void Exit()
//     {
//         base.Exit();
//     }
// }

public class PlayerDashState : State
{
    protected PlayerStateMachine stateMachine;
    protected PlayerMovementData data;
    protected PlayerMovementHandler player;
    protected Animator animator;
    public PlayerDashState(PlayerMovementHandler player ,PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.data = player.movementData;
        this.animator = player.playerAnimator;
    }
    bool CanUpdateLogic = false;

    public override void Enter()
    {
        base.Enter();
        player.stateHandler.CanAttack = false;
        Debug.Log("Player Dashing");
        CanUpdateLogic = false;
        // Timing.RunCoroutine(Dash());
        // player.movementUtilities.DoDash(20f, () => CanUpdateLogic = true);
        
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateLogic()
    {
        if(CanUpdateLogic)
            DoTransition();
    }

    private void DoTransition()
    {
        if(player.colliderDetection.IsGrounded)
        {
            if(!player.Input.HasMotionInput)
                stateMachine.ChangeState(stateMachine.idlingState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
