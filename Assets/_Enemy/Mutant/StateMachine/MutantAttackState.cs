using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantAttackState : MutantBaseState
    {
        public MutantAttackState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent){}

        public override void Enter()
        {
            animator.CrossFade("Mutant Punch", 0.08f);
        }

        public override void Exit(){}

        public override void PhysicUpdate(){}

        public override void Update(){}
    }
}