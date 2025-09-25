
using UnityEngine.XR;

public abstract class StateMachine
{
    protected State currentState;

    public void ChangeState(State newState){
        // if(currentState == null){return ;}
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update(){
        currentState?.Update();
    }

    public void PhysicUpdate(){
        currentState?.PhysicUpdate();
    }
}
