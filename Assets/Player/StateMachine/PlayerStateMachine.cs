using System;
using System.Collections.Generic;
using Unity;


public class PlayerStateMachine :StateMachine
{  

    public PlayerMovementHandler Player{ get;}
    public PlayerIdleState idlingState{ get; private set;}
    public PlayerRunState runningState{ get; private set;}
    public PlayerWalkState walkingState{ get; private set;}

    public PlayerJumpState jumpState{ get; private set;}

    public InAirState inAirState{ get; private set;}

    public PlayerStateMachine(PlayerMovementHandler player){
        Player = player; 
        
        idlingState = new PlayerIdleState(player ,this);
        runningState = new PlayerRunState(player ,this);
        walkingState = new PlayerWalkState(player ,this);

        jumpState = new PlayerJumpState(player ,this);
        inAirState = new InAirState(player ,this); 

        ChangeState(idlingState);
        
    }
}
