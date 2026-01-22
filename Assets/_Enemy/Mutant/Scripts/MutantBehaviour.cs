using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using AdvanceFSM;
using MEC;
using UnityEngine;
using UnityEngine.AI;
using EditorAttributes;

namespace Mutant
{
    public class MutantBehaviour : MonoBehaviour
    {
        [Header("Environment")]
        public EntityComponent entity;  
        public EntityComponent target;

        [Header("State Machine")]
        public float chaseRange;
        public float attackRange;
        
        
        [SerializeField] Animator animator;
        [SerializeField] AgentRootMovement agentRoot;

        [SerializeField] SlashHitBox hitBox;


        public MutantStateMachine stateMachine;
        StateMachineDebugger debugger;
        float damageMultiplier;

        [Header("Effects")]
        public GameObject deadEffect;
        public GameObject groundLandingEffect;
        public TargetableProjectile rockProjectile;
        public AudioSource roaringSFX;
        public int projectileCount = 1;
        public int projectileTimeOffset;

        public bool isInGameActivelyTargeting;
        public bool throwProjectileFlag;
        public bool justLandedFlag;
        public CountdownTimer throwProjectileRandomTimer;

        MutantPatrolState patrolState;
        void Start()
        {
            target = EntityComponentSystem.Instance.GetPlayerComponent();

            debugger = GetComponentInChildren<StateMachineDebugger>();
            
            entity.stateHandler.OnAnimationEvent += (string eventName) => EventHandler.RelyActionOnEvent(this, eventName);
            hitBox.OnEntityEnter += (hitbox, target) =>
            {
                if(DamageHandler.CanDamageThisEntity(entity, target))
                {
                    target.effectModifier.GetDamage(entity.characterStats.Atk * damageMultiplier, entity, DmgType.PHYSIC);
                }
            };

            GetComponent<EntityComponent>().effectModifier.OnTakeDamage += (amt, src, type) =>
            {
                if(amt >= 30 && type == DmgType.PHYSIC)
                    animator.CrossFade("Hit", 0.05f);
            };

            throwProjectileRandomTimer = new CountdownTimer(30);
            throwProjectileRandomTimer.Start();
            throwProjectileRandomTimer.OnTimerFinish += () =>
            {
                throwProjectileFlag = true;
                throwProjectileRandomTimer.Reset(Random.Range(29, 37));
                throwProjectileRandomTimer.Start();
            };

            entity.damageableObject.OnEntityDied += () =>
            {
                gameObject.SetActive(false);
                Instantiate(deadEffect, transform.position, transform.rotation);
            };

            stateMachine = new MutantStateMachine();
            patrolState = new MutantPatrolState(this, animator, agentRoot);
            MutantProjectileAttack projectileAttack = new MutantProjectileAttack(this, animator, agentRoot);
            
            MutantChaseState chaseState = new MutantChaseState(this, animator, agentRoot);
            MutantAttackState attackState = new MutantAttackState(this, animator, agentRoot);
            MutantJumpAttackState jumpAttackState = new MutantJumpAttackState(this, animator, agentRoot);
            

            ResetStateBehaviour();

            stateMachine.AddTransition(patrolState, projectileAttack, new FuncPredicate(() => throwProjectileFlag && !IsInChaseRange()));
            stateMachine.AddTransition(projectileAttack, patrolState, new FuncPredicate(() => !isInGameActivelyTargeting && !throwProjectileFlag));

            stateMachine.AddTransition(patrolState, chaseState, new FuncPredicate(() => isInGameActivelyTargeting && IsInChaseRange()));
            stateMachine.AddTransition(patrolState, jumpAttackState, new FuncPredicate(() => isInGameActivelyTargeting && !IsInChaseRange()));

            stateMachine.AddTransition(chaseState, attackState, new FuncPredicate(() => isInGameActivelyTargeting && IsInAttackRange()));
            stateMachine.AddTransition(attackState, chaseState, new FuncPredicate(() => isInGameActivelyTargeting && !IsInAttackRange()));

            stateMachine.AddTransition(chaseState, projectileAttack, new FuncPredicate(() => isInGameActivelyTargeting && !IsInChaseRange() && throwProjectileFlag));
            stateMachine.AddTransition(chaseState, jumpAttackState, new FuncPredicate(() => isInGameActivelyTargeting && !IsInChaseRange()));
            stateMachine.AddTransition(jumpAttackState, chaseState, new FuncPredicate(() => isInGameActivelyTargeting && IsInChaseRange() && justLandedFlag));
            stateMachine.AddTransition(jumpAttackState, projectileAttack, new FuncPredicate(() => isInGameActivelyTargeting && !IsInChaseRange() && justLandedFlag));
            stateMachine.AddTransition(projectileAttack, chaseState, new FuncPredicate(() => isInGameActivelyTargeting && !throwProjectileFlag));

        }

        public void ResetStateBehaviour()
        {
            isInGameActivelyTargeting = false;
            throwProjectileFlag = false;
            stateMachine?.SetState(patrolState);
            entity?.damageableObject.ResetHealthState();
            SetPhase_1();
        }

        public void SetPhase_1()
        {
            projectileCount = 3;
            if(entity)
                entity.effectModifier.canDamage = false;
        }

        public void SetPhase_2()
        {
            projectileCount = 4;
        }

        public void SetPhase_3()
        {
            projectileCount = 5;
        }

        [Button("Boss Trigger")]
        public void SetBossPhase()
        {
            isInGameActivelyTargeting = true;
            entity.effectModifier.canDamage = true;
        }

        float distance2Target;
        void Update()
        {
            distance2Target = MyUtils.GetDistance(transform.position, target.transform.position);
            stateMachine.Update();

            throwProjectileRandomTimer.Tick(Time.deltaTime);

            if(debugger)
                debugger.SetState(stateMachine.GetCurrentStateForDebugging());

        }

        void FixedUpdate()
        {
            stateMachine.PhysicUpdate();
        }

        bool IsInAttackRange() => distance2Target <= attackRange;
        bool IsInChaseRange() => distance2Target <= chaseRange;

        public IEnumerator<float> WaitForSecs(float time, Action onFinish = null)
        {
            yield return Timing.WaitForSeconds(time);
            onFinish?.Invoke();
        }

        public void Throw()
        {
            TargetableProjectile projectile = Instantiate(rockProjectile, transform.position + Vector3.up * 1f + transform.forward * 0.5f, Quaternion.LookRotation(Random.onUnitSphere));
            projectile.SetDamageData(entity);
            projectile.SetTarget(target.transform);
        }

        public void Punch()
        {
            damageMultiplier = 0.3f;
            hitBox.DoFlashHitbox();
        }

        public void Swiping()
        {
            damageMultiplier = 0.5f;
            hitBox.DoFlashHitbox();
        }

        public void Roaring()
        {
            roaringSFX?.Play();
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
