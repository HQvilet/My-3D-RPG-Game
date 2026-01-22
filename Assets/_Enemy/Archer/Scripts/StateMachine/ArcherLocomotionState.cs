
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
            DoPatrol();
            animator.SetBool(ArcherAnimationConfig.p_IsMoving, true);
        }

        Vector3 nextPatrol;
        void DoPatrol()
        {
            nextPatrol = behaviour.GetTarget().transform.position + MyUtils.RandomizeVector3() * Random.Range(7f, 10f);
            
            if (!NavMesh.Raycast(behaviour.transform.position, nextPatrol, out NavMeshHit hit, NavMesh.AllAreas))
            {
                nextPatrol = hit.position;
            }
            agentMovement.agent.SetDestination(nextPatrol);
            Debug.DrawRay(nextPatrol, Vector3.up * 10f, Color.magenta, 10);
        }



        public override void Exit()
        {
            
        }

        public override void PhysicUpdate()
        {
            Vector3 dir = agentMovement.GetLookDirection();
            Vector3 move = behaviour.transform.worldToLocalMatrix * dir;
            move = MyUtils.ModifyVector(move, y: 0);
            move.Normalize();
            behaviour.LocalMovement = new Vector2(move.x, move.z);
        }

        public override void Update()
        {
            
            if(agentMovement.agent.remainingDistance <= 0.6f)
                DoPatrol();
            
            Vector3 lookDirection = MyUtils.GetPlaneDirection(behaviour.transform.position, behaviour.GetTarget().transform.position);
            behaviour.transform.rotation = Quaternion.LookRotation(lookDirection);
            
        }
        
        void Reset()
        {
            // nextPatrol = Vector3.zero;
        }
    }
}