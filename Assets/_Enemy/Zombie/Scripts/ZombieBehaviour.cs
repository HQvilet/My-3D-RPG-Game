using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using AdvanceFSM;
using UnityEngine;
using DG.Tweening;

namespace Zombie
{
    public class ZombieBehaviour : MonoBehaviour, IEntityPoolable
    {

        [Header("State Section")]
        [SerializeField] float chaseRange = 5f;
        [SerializeField] public float attackRange = 1f;

        [SerializeField] Animator animator;
        [SerializeField] AgentRootMovement agentRoot;

        EntityComponent entity;
        ZombieStateMachine stateMachine;
        StateMachineDebugger debugger;

        [ReadOnly] public Transform target;

        [SerializeField] Transform deadEffect;


        void Start()
        {
            stateMachine = new ZombieStateMachine();
            debugger = GetComponentInChildren<StateMachineDebugger>();
            
            entity = GetComponent<EntityComponent>();
            target = EntityComponentSystem.Instance.GetPlayerComponent().transform;

            ZombiePatrolState patrolState = new ZombiePatrolState(this, animator, agentRoot);
            ZombieChaseState chaseState = new ZombieChaseState(this, animator, agentRoot);
            ZombieAttackState attackState = new ZombieAttackState(this, animator, agentRoot);

            Scream();

            stateMachine.SetState(patrolState);

            stateMachine.AddTransition(patrolState, chaseState, new FuncPredicate(() => distance2Target <= chaseRange));
            stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => distance2Target <= attackRange));

            stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => distance2Target > attackRange));
            stateMachine.AddTransition(chaseState, patrolState, new FuncPredicate(() => distance2Target > chaseRange));

            //
            hitBox.OnEntityEnter += (hitbox, target) =>
            {
                if(DamageHandler.CanDamageThisEntity(entity, target))
                {
                    target.effectModifier.GetDamage(entity.characterStats.Atk * 1f, entity, DmgType.PHYSIC);
                }
            };
            entity.stateHandler.OnAnimationEvent += (evt) => EventHandler.RelyActionOnEvent(this, evt);
            entity.effectModifier.OnTakeDamage += (amt, src, type) =>
            {
                if(type == DmgType.PHYSIC && amt >= 10f)
                    if(animator.isActiveAndEnabled)
                        animator.CrossFade("Hit", 0.05f);
            };
            entity.damageableObject.OnEntityDied += () =>
            {
                WorldItemDropHandler.Instance.TryDropItemByRate(transform.position);
                
                Instantiate(deadEffect, transform.position, transform.rotation);
                
                EntityPooling.Instance.AddToPool(this.gameObject);
                // transform.DOKill();
                // Destroy(gameObject);
            };
        }

        public void Scream()
        {
            animator.Play("zombie_scream");
        }
        
        float distance2Target;
        void Update()
        {
            distance2Target = MyUtils.GetDistance(transform.position, target.transform.position);
            stateMachine.Update();

            if(debugger)
                debugger.SetState(stateMachine.GetCurrentStateForDebugging());
        }

        void FixedUpdate()
        {
            stateMachine.PhysicUpdate();
        }

        [Header("Ability")]
        [SerializeField] SlashHitBox hitBox;

        public void DoAttack()
        {
            hitBox.DoFlashHitbox();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseRange);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        
        void ResetAnimator()
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("TargetInSight", false);
        }

        void OnDestroy()
        {
            transform.DOKill();
        }

        public void BackToPool()
        {
            transform.DOKill();
            ResetAnimator();
            gameObject.SetActive(false);
        }

        public void OutFromPool()
        {
            gameObject.SetActive(true);
            if(entity)
                entity.damageableObject.ResetHealthState();
        }
    }
}
