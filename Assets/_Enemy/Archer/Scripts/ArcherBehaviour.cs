using System;
using AdvanceFSM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Archer
{
    public class ArcherBehaviour : MonoBehaviour, IEntityPoolable
    {
        [Header("State Machine")]
        public float attackRange = 10f;
        public float dangerRange = 5f;

        [SerializeField] NavMeshAgent agent;
        [SerializeField] AgentRootMovement agentMovement;
        [SerializeField] Animator animator;
        public EntityComponent entity;

        [Header("Abilities")]
        [SerializeField] ProjectileHitbox arrowProjectile;
        [SerializeField] Transform shotPoint;

        public Vector2 LocalMovement
        {
            get => new Vector2(animator.GetFloat(ArcherAnimationConfig.p_MoveX), animator.GetFloat(ArcherAnimationConfig.p_MoveY));
            set
            {
                animator.SetFloat(ArcherAnimationConfig.p_MoveX, value.x);
                animator.SetFloat(ArcherAnimationConfig.p_MoveY, value.y);
            }
        }

        [Header("Effects")]
        public Transform deadEffect;

        public bool canAim;
        public bool canShot;

        EntityComponent target;
        public EntityComponent GetTarget() => target;

        ArcherStateMachine stateMachine;
        StateMachineDebugger debugger;

        float distance2Target;
        public bool isDodging = false;
        void Start()
        {
            debugger = GetComponentInChildren<StateMachineDebugger>();

            target = EntityComponentSystem.Instance.GetPlayerComponent();
            entity = GetComponent<EntityComponent>();

            entity.stateHandler.OnAnimationEvent += (evt) => EventHandler.RelyActionOnEvent(this, evt);
            entity.effectModifier.OnTakeDamage += (amt, src, type) =>
            {
                entity.effectModifier.OnTakeDamage += (amt, src, type) =>
                {
                    if(type == DmgType.PHYSIC && amt >= 10f){}
                        // if(animator.isActiveAndEnabled)
                        //     animator.CrossFade("Hit", 0.05f);
                };                
            };

            entity.damageableObject.OnEntityDied += () =>
            {
                WorldItemDropHandler.Instance.TryDropItemByRate(transform.position);
                
                Instantiate(deadEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            };

            stateMachine = new ArcherStateMachine();

            ArcherLocomotionState locomotionState = new ArcherLocomotionState(this, animator, agentMovement);
            ArcherAttackState attackState = new ArcherAttackState(this, animator, agentMovement);
            ArcherDodgeState dodgeState = new ArcherDodgeState(this, animator, agentMovement);

            stateMachine.SetState(locomotionState);

            stateMachine.AddTransition(locomotionState, attackState, new FuncPredicate(() => IsTargetInSight()));
            stateMachine.AddTransition(attackState, dodgeState, new FuncPredicate(() => IsInDanger() && !HaveObstacleInTheBack()));

            stateMachine.AddTransition(attackState, locomotionState, new FuncPredicate(() => !IsTargetInSight()));
            stateMachine.AddTransition(dodgeState, attackState, new FuncPredicate(() => !IsInDanger() && !isDodging));

        }

        void Update()
        {
            distance2Target = MyUtils.GetDistance(transform, target.transform);
            stateMachine.Update();
            
            if(debugger)
                debugger.SetState(stateMachine.GetCurrentStateForDebugging());
        }

        void FixedUpdate()
        {
            stateMachine.PhysicUpdate();
        }

        bool IsTargetInSight()
        {
            return distance2Target <= attackRange;
        }

        bool IsInDanger()
        {
            return distance2Target <= dangerRange;
        }

        bool HaveObstacleInTheBack()
        {
            return Physics.Raycast(transform.position + Vector3.up * 0.5f, -transform.forward, 2f , EnvironmentHelper.Instance.onlyStaticObject);
        }

        public void ShotArrow()
        {
            Instantiate(arrowProjectile, 
                shotPoint.position, 
                Quaternion.LookRotation((target.transform.position + Vector3.up * 0.5f + Random.insideUnitSphere * 0.9f) - shotPoint.position
                )).SetDamageData(entity);
        }

        public event Action OnFinishDodge;
        public void FinishDodge()
        {
            isDodging = false;
            OnFinishDodge?.Invoke();
        }

        public void RotateTowardTarget(float time, Action onComplete = null)
        {
            transform.DOLookAt(target.transform.position, time).onComplete += () => onComplete?.Invoke();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dangerRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }

        void OnDestroy()
        {
            transform.DOKill(true);
        }

        public void BackToPool()
        {
            transform.DOKill();
            gameObject.SetActive(false);
        }

        public void OutFromPool()
        {
            gameObject.SetActive(true);
        }
    }
}