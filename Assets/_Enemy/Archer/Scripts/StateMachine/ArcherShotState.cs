using AdvanceFSM;
using UnityEngine;

namespace Archer
{
    public class ArcherAttackState : BaseArcherState
    {
        public ArcherAttackState(ArcherBehaviour behaviour, Animator animator, AgentRootMovement agentMovement) : base(behaviour, animator, agentMovement)
        {
            
        }
        
        public override void Enter()
        {
            behaviour.attackRange += 0.25f;
            animator.SetBool(ArcherAnimationConfig.p_IsAttack, true);
        }

        public override void Exit()
        {
            behaviour.attackRange -= 0.25f;
            animator.SetBool(ArcherAnimationConfig.p_IsAttack, false);
        }

        public override void PhysicUpdate(){}

        public override void Update()
        {
            if(!behaviour.isDodging)
                behaviour.RotateTowardTarget(Time.deltaTime);
        }
    }
}