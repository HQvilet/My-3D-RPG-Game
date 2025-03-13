using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundingState : State
{
    protected PlayerStateMachine stateMachine;
    protected PlayerMovementData data;
    protected PlayerMovementHandler player;
    protected Animator animator;
    public GroundingState(PlayerMovementHandler player ,PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.data = player.movementData;
        this.animator = player.playerAnimator;
    }

    public override void Enter()
    {
        player.stateHandler.OnMeleePerformed += ChangeToAttackState;
    }

    public override void Exit()
    {
        player.stateHandler.OnMeleePerformed -= ChangeToAttackState;
    }

    public override void PhysicUpdate()
    {
        
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdateLogic()
    {

        if(player.Input.PlayerInput.Dash.WasPerformedThisFrame())
            stateMachine.ChangeState(stateMachine.dashState);
            
        // if(player.Input.HasJumpInput || stateMachine.jumpState._jumpBufferCount > 0)
        //     stateMachine.ChangeState(stateMachine.jumpState);

        if(!player.colliderDetection.IsGrounded)
            stateMachine.ChangeState(stateMachine.inAirState);

    }

    void ChangeToAttackState()
    {
        stateMachine.ChangeState(stateMachine.attackState);
    }
}
