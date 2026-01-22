using AdvanceFSM;
using UnityEngine;

namespace Zombie
{
    public class ZombieStateMachine : StateMachine{}
    
    public class ZombieBaseState : IState
    {
        protected ZombieBehaviour behaviour;
        protected Animator animator;
        protected AgentRootMovement agent;
        public ZombieBaseState(ZombieBehaviour behaviour, Animator animator, AgentRootMovement agent)
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