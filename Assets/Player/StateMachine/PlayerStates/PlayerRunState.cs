using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState :GroundingState//: PlayerMovementState
{


    public PlayerRunState(PlayerMovementHandler player ,PlayerStateMachine stateMachine) : base(player ,stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        player.stateHandler.CanAttack = true;
        Debug.Log("Enter Run");
        animator.SetBool("IsMoving" ,true);
        animator.SetBool("IsRunning" ,true);
    }

    public override void Exit()
    {
        base.Exit();
        animator.SetBool("IsRunning" ,false);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        player.movementUtilities.DoMove(player.Input.MoveDirection() ,data.runSpeed);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if(!player.Input.HasMotionInput)
            stateMachine.ChangeState(stateMachine.idlingState);
    }
}
