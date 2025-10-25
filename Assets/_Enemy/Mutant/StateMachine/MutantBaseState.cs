using AdvanceFSM;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantBaseState : IState
    {
        protected MutantBehaviour behaviour;
        protected Animator animator;
        protected AgentRootMovement agent;
        public MutantBaseState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent)
        {
            this.behaviour = behaviour;
            this.animator = animator;
            this.agent = agent;
        }

        public virtual void Enter() { }

        public virtual void Exit(){}

        public virtual void PhysicUpdate(){}

        public virtual void Update(){}
    }
}