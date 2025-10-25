using System.Collections;
using System.Collections.Generic;
using AdvanceFSM;
using UnityEngine;
using UnityEngine.AI;

namespace Mutant
{
    public class MutantBehaviour : MonoBehaviour
    {

        [SerializeField] float chaseRange;
        [SerializeField] float attackRange;
        [SerializeField] NavMeshAgent agent;
        // AgentRootMovement agentMovement;
        [SerializeField] Animator animator;
        [SerializeField] AgentRootMovement agentRoot;

        // [SerializeField] Transform model;

        EntityComponent target;
        public EntityComponent GetTarget() => target;

        MutantStateMachine stateMachine;
        MutantAbilities abilities;

        

        public bool isJumpingToTarget = false;
        void Start()
        {
            target = EntityComponentSystem.Instance.GetPlayerComponent();
            Random.InitState((int)System.DateTime.Now.Ticks);

            abilities = GetComponent<MutantAbilities>();
            abilities.SetTarget(target.transform);
            abilities.Landing += () => isJumpingToTarget = false;

            stateMachine = new MutantStateMachine();

            MutantAttackState attackState = new MutantAttackState(this, animator, agentRoot);
            MutantPatrolState patrolState = new MutantPatrolState(this, animator, agentRoot);
            MutantChaseState chaseState = new MutantChaseState(this, animator, agentRoot);
            MutantJumpAttackState jumpAttackState = new MutantJumpAttackState(this, animator, agentRoot);
            // jumpAttackState.SetData(model, GetComponent<CharacterController>());

            stateMachine.SetState(patrolState);

            stateMachine.AddTransition(patrolState, jumpAttackState, new FuncPredicate(() => isJumpingToTarget));

            stateMachine.AddAnyTransition(attackState, new FuncPredicate(() => distance2Target <= attackRange && !isJumpingToTarget));
            stateMachine.AddAnyTransition(chaseState, new FuncPredicate(() => distance2Target <= chaseRange && !isJumpingToTarget));
            stateMachine.AddAnyTransition(patrolState, new FuncPredicate(() => distance2Target > chaseRange && !isJumpingToTarget));

        }
        float distance2Target;
        void Update()
        {
            distance2Target = (target.transform.position - transform.position).magnitude;
            stateMachine.Update();

        }

        void FixedUpdate()
        {
            stateMachine.PhysicUpdate();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);

        }
    }

}
