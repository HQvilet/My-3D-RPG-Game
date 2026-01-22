using DG.Tweening;
using UnityEngine;

namespace Mutant
{
    public class MutantAttackState : MutantBaseState
    {
        public MutantAttackState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent){}

        public override void Enter()
        {
            behaviour.attackRange += 0.2f;
            animator.SetBool(MutantAnimationConfig.p_IsAttack, true);
            animator.CrossFade(MutantAnimationConfig.a_Punch, 0.08f);
        }

        public override void Exit()
        {
            behaviour.attackRange -= 0.2f;
            animator.SetBool(MutantAnimationConfig.p_IsAttack, false);
        }

        public override void PhysicUpdate(){}

        public override void Update()
        {
            if(Vector3.Dot(behaviour.transform.forward, (behaviour.target.transform.position - behaviour.transform.position).normalized) > 0.1f)
            {
                behaviour.transform.DOLookAt(behaviour.target.transform.position, 0.2f);
            }
        }
    }
}