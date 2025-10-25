using AdvanceFSM;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantChaseState : MutantBaseState
    {
        public MutantChaseState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent) { }

        public override void Enter()
        {
            animator.CrossFade("Run", 0.08f);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsMoving", true);
        }

        public override void Exit() { }

        public override void PhysicUpdate() { }

        public override void Update()
        {
            DoChase();
        }
        
        void DoChase()
        {
            if (behaviour.GetTarget() != null)
            {
                agent.agent.SetDestination(behaviour.GetTarget().transform.position);
            }
        }
    }
}