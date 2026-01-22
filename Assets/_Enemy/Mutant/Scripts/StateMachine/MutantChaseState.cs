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
            animator.SetBool(MutantAnimationConfig.p_TargetInSight, true);
            animator.SetBool(MutantAnimationConfig.p_IsMoving, true);
        }

        public override void Exit()
        {
            animator.SetBool(MutantAnimationConfig.p_TargetInSight, false);
        }

        public override void PhysicUpdate() { }

        public override void Update()
        {
            DoChase();
        }
        
        void DoChase()
        {
            if (behaviour.target != null)
            {
                if(agentMovement.agent.enabled)
                    agentMovement.agent.SetDestination(behaviour.target.transform.position);
            }
        }
    }
}