using UnityEngine;

namespace Zombie
{
    public class ZombieChaseState : ZombieBaseState
    {
        public ZombieChaseState(ZombieBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent)
        {
            offset = Random.Range(0, 1f);
        }
        float offset;
        public override void Enter()
        {
            animator.SetBool(ZombieAnimationConfig.p_TargetInSight, true);
        }
        public override void Update()
        {
            offset -= Time.deltaTime;
            if(offset <= 0f)
            {
                offset = 0.15f;
                if(agent.agent.enabled)
                    agent.agent.SetDestination(behaviour.target.position);
            }
        }
    }
}