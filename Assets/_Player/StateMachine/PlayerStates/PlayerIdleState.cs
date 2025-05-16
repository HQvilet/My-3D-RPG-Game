using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerIdleState : GroundingState
{

    public PlayerIdleState(PlayerMovementHandler player ,PlayerStateMachine stateMachine) : base(player ,stateMachine)
    {

    }


    public override void Enter()
    {
        base.Enter();
        player.stateHandler.CanAttack = true;
        Debug.Log("Enter Idling");
        animator.SetBool("IsMoving" ,false);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(player.Input.HasMotionInput)
            stateMachine.ChangeState(stateMachine.runningState);
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
