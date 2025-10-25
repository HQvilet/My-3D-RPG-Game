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

        public override void Enter()
        {
            agent.OnFinishTravel += Patrol;
            animator.CrossFade("Walk", 0.08f);
            animator.SetBool("IsWalking", true);
            Reset();
        }

        public override void Exit()
        {
            agent.OnFinishTravel -= Patrol;
        }

        public override void PhysicUpdate() { }

        public override void Update()
        {
            // DoPatrol();
            Debug.DrawLine(nextPatrol + Vector3.up * 1f,nextPatrol + Vector3.up * 10f, Color.green);
        }


        Vector3 nextPatrol;
        void Patrol()
        {
            nextPatrol = behaviour.transform.position + MyUtils.RandomizeVector3() * Random.Range(5f, 7f);
            
            
            if (!NavMesh.SamplePosition(nextPatrol, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                nextPatrol = hit.position;
            }
            Timing.RunCoroutine(IdleInSec(1f));
        }

        IEnumerator<float> IdleInSec(float time)
        {
            animator.SetBool("IsMoving", false);
            yield return Timing.WaitForSeconds(time);
            animator.SetBool("IsMoving", true);
            agent.agent.SetDestination(nextPatrol);
        }

        void Reset()
        {
            nextPatrol = Vector3.zero;
        }
    }
}