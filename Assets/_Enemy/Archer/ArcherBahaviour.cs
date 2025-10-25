using AdvanceFSM;
using Mutant;
using UnityEngine;
using UnityEngine.AI;

namespace Archer
{
    public class ArcherBehaviour : MonoBehaviour
    {
        [SerializeField] float dangerZone;
        [SerializeField] NavMeshAgent agent;
        [SerializeField] AgentRootMovement agentMovement;
        [SerializeField] Animator animator;

        public Vector2 LocalMovement
        {
            get => new Vector2(animator.GetFloat("VelX"), animator.GetFloat("VelZ"));
            set
            {
                animator.SetFloat("VelX", value.x);
                animator.SetFloat("VelZ", value.y);
            }
        }

        public bool canAim;
        public bool canShot;

        EntityComponent target;
        public EntityComponent GetTarget() => target;

        ArcherStateMachine stateMachine;

        float distance2Target;
        void Start()
        {
            target = EntityComponentSystem.Instance.GetPlayerComponent();
            Random.InitState((int)System.DateTime.Now.Ticks);

            stateMachine = new ArcherStateMachine();

            ArcherLocomotionState locomotionState = new ArcherLocomotionState(this, animator, agentMovement);
            ArcherShotState shotAimState = new ArcherShotState(this, animator, agentMovement);
            ArcherDodgeState dodgeState = new ArcherDodgeState(this, animator, agentMovement);

            stateMachine.SetState(locomotionState);

            stateMachine.AddAnyTransition(locomotionState, new FuncPredicate(() => false));
        }

        void Update()
        {
            distance2Target = (target.transform.position - transform.position).magnitude;
            stateMachine.Update();
        }

        void FixedUpdate()
        {
            stateMachine.PhysicUpdate();
        }

        bool IsInDanger() => distance2Target <= dangerZone;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dangerZone);
        }
    }
}