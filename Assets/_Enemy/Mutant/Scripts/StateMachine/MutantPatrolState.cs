using System.Collections;
using System.Collections.Generic;
using AdvanceFSM;
using MEC;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantPatrolState : MutantBaseState
    {
        public MutantPatrolState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent) { }

        CoroutineHandle patrolCoroutine;

        public override void Enter()
        {
            agentMovement.OnFinishTravel += Patrol;
            animator.SetBool(MutantAnimationConfig.p_IsMoving, true);
            Patrol();
        }
        

        public override void Exit()
        {
            agentMovement.OnFinishTravel -= Patrol;
            
        }

        public override void PhysicUpdate() { }


        Vector3 nextPatrol;
        void Patrol()
        {
            nextPatrol = behaviour.transform.position + MyUtils.RandomizeVector3() * Random.Range(5f, 7f);
            if (!NavMesh.Raycast(behaviour.transform.position, nextPatrol, out NavMeshHit hit, NavMesh.AllAreas))
            {
                nextPatrol = hit.position;
            }
            animator.SetBool(MutantAnimationConfig.p_IsMoving, false);
            Timing.RunCoroutine(behaviour.WaitForSecs(1f,() => {
                animator.SetBool(MutantAnimationConfig.p_IsMoving, true);
                if(agentMovement.agent.enabled && behaviour.gameObject.activeInHierarchy)
                    agentMovement.agent.SetDestination(nextPatrol);
            }).CancelWith(behaviour.gameObject));
        }
    }
}