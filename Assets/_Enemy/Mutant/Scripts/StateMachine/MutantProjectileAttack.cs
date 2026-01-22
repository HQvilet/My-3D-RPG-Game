using System.Collections.Generic;
using AdvanceFSM;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantProjectileAttack : MutantBaseState
    {
        public MutantProjectileAttack(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent) { }

        public override void Enter()
        {
            animator.SetBool(MutantAnimationConfig.p_IsMoving, false);
            Timing.RunCoroutine(Throw(behaviour.projectileCount, 1f));

        }

        IEnumerator<float> Throw(int count, float frequency)
        {
            behaviour.transform.DOKill();
            // behaviour.transform.DOLookAt(behaviour.target.transform.position, 0.1f);
            int r_count = count + Random.Range(-1, 2);
            for(int i = 0; i < count; ++i)
            {
                animator.CrossFade("Throw", 0.08f);
                yield return Timing.WaitForSeconds(frequency);
            }

            yield return Timing.WaitForSeconds(0.5f);
            behaviour.throwProjectileFlag = false;
        }

        public override void Exit()
        {
            animator.SetBool(MutantAnimationConfig.p_IsMoving, true);
        }

        public override void PhysicUpdate() { }

        public override void Update()
        {
            
        }
        
    }
}