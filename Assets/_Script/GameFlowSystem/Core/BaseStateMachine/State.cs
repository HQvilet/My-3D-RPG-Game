

using System.Collections.Generic;

namespace AdvanceFSM
{
    // public class State : IState
    // {
    //     public virtual void Enter() { }

    //     public virtual void Exit() { }

    //     public virtual void PhysicUpdate() { }

    //     public virtual void Update() { }

    // }

    public class StateNode
    {
        public IState state;
        public HashSet<ITransition> transitions = new();
        public StateNode(IState state)
        {
            this.state = state;
            // transitions = new();
        }

        public void AddTransition(ITransition transition) => transitions.Add(transition);
    }

}
