using System;
using System.Xml;
using DG.Tweening;
using MEC;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

namespace Mutant
{
    public class MutantJumpAttackState : MutantBaseState
    {
        public MutantJumpAttackState(MutantBehaviour behaviour, Animator animator, AgentRootMovement agent) : base(behaviour, animator, agent)
        {
            
        }
        public Action JumpLanded;
        Vector3 target;

        public float sec = 1.5f;
        public float angle = 73;

        Vector3 moveDirection;        
        float distance, vy, g;
        bool landed;
        bool valid = false;
        public void Calculate()
        {
            moveDirection = target - behaviour.transform.position;
            distance = moveDirection.magnitude - 0.1f;

            moveDirection.Normalize();
            vy = distance / sec * Mathf.Tan(angle * Mathf.Deg2Rad);
            g = 2 * distance * Mathf.Tan(angle * Mathf.Deg2Rad) / (sec * sec);
        }
        public override void Enter()
        {
            animator.SetBool(MutantAnimationConfig.p_IsFalling, false);
            animator.SetBool(MutantAnimationConfig.p_IsLanding, false);
            animator.CrossFade(MutantAnimationConfig.a_JumpAttack, 0.08f);

            landed = false;
            valid = false;
            
            target = behaviour.target.transform.position;
            behaviour.justLandedFlag = false;
            
            agentMovement.IgnoreAgent(); 
            agentMovement.AllowRootMovement = false;
            behaviour.transform.DOKill();
            behaviour.transform.DOLookAt(target, 0.5f).onComplete += () =>
            {
                Calculate();
                valid = true;
            };
        }

        public override void Exit()
        {
            agentMovement.UseAgent();
            agentMovement.AllowRootMovement = true;
        }

        public override void PhysicUpdate()
        {
            if(!valid)
                return;
            Vector3 vel = moveDirection * distance / sec;
            vy -= Time.fixedDeltaTime * g;

            if(!landed)
                agentMovement.controller.Move(vel * Time.fixedDeltaTime + Vector3.up * vy * Time.fixedDeltaTime);

            if(vy < 0)
            {
                animator.SetBool("IsFalling", true);
            }
            
            if(Physics.Raycast(behaviour.transform.position, Vector3.down, out RaycastHit hitInfo, 10f, EnvironmentHelper.Instance.onlyStaticObject))
            {
                if(hitInfo.distance < 2f && vy < 0 && !landed)
                {
                    landed = true;
                    SnapToGround();
                    GroundLandingEffect();
                }
            }
        }

        void GroundLandingEffect()
        {
            GameObject.Instantiate(behaviour.groundLandingEffect, behaviour.transform.position, Quaternion.identity)
                .GetComponent<DamageUnit>()
                ?.SetDamageData(behaviour.entity);
        }

        void SnapToGround()
        {
            
            behaviour.transform.position = target;
            Timing.RunCoroutine(behaviour.WaitForSecs(0.7f, () =>
            {
                animator.SetBool("IsLanding", true);
                behaviour.justLandedFlag = true;
                
            }));
        }

        public override void Update()
        {
            
        }
    }
}