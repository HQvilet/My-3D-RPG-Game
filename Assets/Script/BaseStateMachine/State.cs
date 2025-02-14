
public class State : IState
{
    public virtual void Enter(){}

    public virtual void Exit(){}

    public virtual void PhysicUpdate(){}

    public virtual void Update()
    {
        UpdateLogic();
    }

    public virtual void UpdateLogic(){}
}
