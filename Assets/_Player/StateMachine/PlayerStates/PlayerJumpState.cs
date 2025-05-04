using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerJumpState : State
{
    private PlayerStateMachine stateMachine;
    private PlayerMovementHandler player;

    public float _jumpBufferTime = 0.1f;
    public float _jumpCoyoteTime = 0.1f;

    public float _jumpBufferCount;
    public float _jumpCoyoteCount;


    public PlayerJumpState(PlayerMovementHandler player ,PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.player = player;
    }


    public override void Enter()
    {
        Debug.Log("Do Jump");
        player.movementUtilities.DoJump(player.movementData.JumpImpulse);
        stateMachine.ChangeState(stateMachine.inAirState);
    }

    
}
