using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    public class ZombiePatrolState : ZombieBaseState
    {
        public ZombiePatrolState(ZombieBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent)
        {

        }

        public override void Enter()
        {
            animator.SetBool(ZombieAnimationConfig.p_IsMoving, true);
        }

        
        public override void Update()
        {
            if(agent.agent.remainingDistance <= 0.5f)
                DoPatrol();
        }

        Vector3 nextPatrol;
        void DoPatrol()
        {
            nextPatrol = behaviour.transform.position + MyUtils.RandomizeVector3() * Random.Range(5f, 7f);
            if (!NavMesh.SamplePosition(nextPatrol, out NavMeshHit hit, 7.5f, NavMesh.AllAreas))
            {
                nextPatrol = hit.position;
            }
            agent.agent.SetDestination(nextPatrol);
        }
    }
}