using AdvanceFSM;
using UnityEngine;

namespace Archer
{
    public class ArcherStateMachine : StateMachine{}
    
    public class BaseArcherState : IState
    {
        protected ArcherBehaviour behaviour;
        protected Animator animator;
        protected AgentRootMovement agentMovement;
        public BaseArcherState(ArcherBehaviour behaviour, Animator animator, AgentRootMovement agentMovement)
        {
            this.behaviour = behaviour;
            this.animator = animator;
            this.agentMovement = agentMovement;
        }

        public virtual void Enter(){}

        public virtual void Exit(){}

        public virtual void PhysicUpdate(){}

        public virtual void Update(){}
    }
}