using System;
using System.Xml;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Mutant
{
    public class MutantJumpAttackState : MutantBaseState
    {
        public MutantJumpAttackState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent, Action onLandingGround = null) : base(behaviour, animator, agent)
        {
            target = behaviour.GetTarget().transform;
        }
        Transform target;

        Transform model;
        CharacterController controller;
        public void SetData(Transform model, CharacterController controller)
        {
            this.model = model;
            this.controller = controller;       
        }

        public float sec = 2;
        public float angle = 60;

        Vector3 moveDirection;        
        float distance;
        float vy;
        float g;
        public void Calculate()
        {
            moveDirection = target.position - behaviour.transform.position;
            distance = (target.position - behaviour.transform.position).magnitude - 0.1f;

            moveDirection.Normalize();
            vy = distance / sec * Mathf.Tan(angle * Mathf.Deg2Rad);
            g = 2 * distance * Mathf.Tan(angle * Mathf.Deg2Rad) / (sec * sec);
        }
        public override void Enter()
        {
            animator.CrossFade("Jump Attack 1", 0.08f);
        }

        public override void Exit(){}

        public override void PhysicUpdate()
        {
            // Vector3 vel = moveDirection * distance / sec;
            // vy -= Time.fixedDeltaTime * g;
            // model.position += Vector3.up * vy * Time.fixedDeltaTime;

            // controller.Move(vel * Time.fixedDeltaTime);

            // if ((target.position - behaviour.transform.position).magnitude < 1f || model.position.y < 0)
            // {
            //     // GroundSmashLanding();
            //     model.localPosition = Vector3.zero;
            // }
            
            // if(Physics.Raycast(model.position, Vector3.down, out RaycastHit hitInfo, 10f))
            // {
            //     if(hitInfo.distance < 6f)
            //     {
            //         animator.SetTrigger("Hit Ground");
            //     }
            // }
        }

        public override void Update(){}
    }
}