using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : State
{
    protected PlayerStateMachine stateMachine;
    protected PlayerMovementData data;
    protected PlayerMovementHandler player;
    protected Animator animator;
    private float exitTime;
    public InAirState(PlayerMovementHandler player ,PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.data = player.movementData;
        this.animator = player.playerAnimator;
    }

    public override void Enter()
    {
        Debug.Log("Enter Air State");
        exitTime = 0.05f;
    }

    public override void Exit()
    {

    }

    public override void PhysicUpdate()
    {
        // player.rb.AddForce(Vector3.down * data.gravity ,ForceMode.Force);
        player.movementUtilities.DoMove(player.Input.MoveDirection() ,data.walkSpeed);
    }

    public override void Update()
    {
        base.Update();
        if(player.Input.HasJumpInput)
            stateMachine.jumpState._jumpBufferCount = stateMachine.jumpState._jumpBufferTime;
            
        stateMachine.jumpState._jumpBufferCount -= Time.deltaTime;
    }

    public override void UpdateLogic()
    {
        exitTime -= Time.deltaTime;
        if(player.colliderDetection.IsGrounded && exitTime <= 0)
        {
            if(!player.Input.HasMotionInput)
                stateMachine.ChangeState(stateMachine.idlingState);
            else
                stateMachine.ChangeState(stateMachine.runningState);
        }
    }
}
