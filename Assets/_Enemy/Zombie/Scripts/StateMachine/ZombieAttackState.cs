using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombieAttackState : ZombieBaseState
    {
        public ZombieAttackState(ZombieBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent)
        {

        }

        public override void Enter()
        {
            animator.SetBool(ZombieAnimationConfig.p_IsAttack, true);
            animator.CrossFade(ZombieAnimationConfig.a_Punch, 0.08f);
            behaviour.attackRange += 0.15f;
        }

        
        public override void Update()
        {
            
        }

        public override void Exit()
        {
            animator.SetBool(ZombieAnimationConfig.p_IsAttack, false);
            behaviour.attackRange -= 0.15f;
        }
    }
}