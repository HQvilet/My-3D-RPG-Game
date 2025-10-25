
using System.Collections.Generic;
using AdvanceFSM;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.AI;

namespace Archer
{
    public class ArcherLocomotionState : BaseArcherState
    {
        public ArcherLocomotionState(ArcherBehaviour behaviour, Animator animator, AgentRootMovement agentMovement) : base(behaviour, animator, agentMovement){}

        public override void Enter()
        {
            Patrol();
            agentMovement.OnFinishTravel += Patrol;
            animator.SetBool("IsMoving", true);
        }

        Vector3 nextPatrol;
        // move to safe area(as far as player position and in shotting range)
        void Patrol()
        {
            nextPatrol = behaviour.GetTarget().transform.position + MyUtils.RandomizeVector3() * Random.Range(7f, 10f);
            nextPatrol = MyUtils.ModifyVector(nextPatrol, y: 0);
            if (!NavMesh.SamplePosition(nextPatrol, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                nextPatrol = hit.position;
            }
            Timing.RunCoroutine(IdleInSec(2f));
            // agentMovement.agent.SetDestination(nextPatrol);
        }


        bool isStandingIdle = false;
        IEnumerator<float> IdleInSec(float time)
        {
            isStandingIdle = true;
            // float t = 1f;
            // Vector2 a = behaviour.LocalMovement;
            // while(t >= -0.01)
            // {
            //     t -= Time.deltaTime*2;
            //     Vector2 smooth = Vector2.Lerp(a, Vector2.zero, t);
            //     behaviour.LocalMovement = smooth; 
                
            //     yield return 0;
            // }
            behaviour.LocalMovement = Vector2.zero;
            yield return Timing.WaitForSeconds(time);
            isStandingIdle = false;

            agentMovement.agent.SetDestination(nextPatrol);
        }
        public override void Exit()
        {
            agentMovement.OnFinishTravel -= Patrol;
            Reset();
        }

        public override void PhysicUpdate() { }

        public override void Update()
        {
            Debug.DrawLine(nextPatrol + Vector3.up * 0.5f, behaviour.transform.position, Color.black);
            behaviour.transform.DOLookAt(behaviour.GetTarget().transform.position, 0.1f);

            if (isStandingIdle)
                return;
            Vector3 m_pos = MyUtils.ModifyVector(behaviour.transform.position, y: 0);
            Vector3 dir = nextPatrol - m_pos;
            Vector3 move = behaviour.transform.worldToLocalMatrix * dir.normalized;

            if (dir.magnitude > 0.11f)
            {
                move = MyUtils.ModifyVector(move, y: 0);
                move.Normalize();
                behaviour.LocalMovement = new Vector2(move.x, move.z);
            }
        }
        
        void Reset()
        {
            nextPatrol = Vector3.zero;
        }
    }
}