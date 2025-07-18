using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : GroundingState
{

    public PlayerAttackState(PlayerMovementHandler player ,PlayerStateMachine stateMachine) : base(player ,stateMachine)
    {

    }

    public override void Enter()
    {
        // Debug.Log("Attack Trigger");
        player.stateHandler.OnMeleeCompletedState += DoTransition;
    }

    public override void Exit()
    {
        player.stateHandler.OnMeleeCompletedState -= DoTransition;
    }

    public override void UpdateLogic()
    {
        // if(player.Input.HasMotionInput)
        //     stateMachine.ChangeState(stateMachine.runningState);
    }

    void DoTransition()
    {
        // if(player.colliderDetection.IsGrounded)
        if(player.controller.isGrounded)
        {
            if(!player.Input.HasMotionInput)
                stateMachine.ChangeState(stateMachine.idlingState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }
    }


}
