using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState :GroundingState//: PlayerMovementState
{


    public PlayerWalkState(PlayerMovementHandler player ,PlayerStateMachine stateMachine) : base(player ,stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
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
        player.movementUtilities.DoMove(player.Input.MoveDirection() ,data.walkSpeed);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateLogic()
    {
        if(!player.Input.HasMotionInput)
            stateMachine.ChangeState(stateMachine.idlingState);
    }
}
