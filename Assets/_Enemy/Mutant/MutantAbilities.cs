using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using MEC;
using UnityEngine;

namespace Mutant
{
    public class MutantAbilities : MonoBehaviour
    {
        [SerializeField] CharacterStateHandler stateHandler;
        [SerializeField] EntityComponent entity;
        [SerializeField] SlashHitBox hitBox;
        [SerializeField] DamageModifier modifier;

        [SerializeField] AgentRootMovement agentRootMovement;
        [SerializeField] Transform model;
        [SerializeField] Animator animator;

        void Start()
        {
            stateHandler.OnAnimationEvent += (string eventName) => EventHandler.RelyActionOnEvent(this, eventName);

            hitBox.SetSourceDamage(entity);
            
        }

        public void Punch()
        {
            hitBox.DoDamage(modifier);
        }

        public void Swiping()
        {
            hitBox.DoDamage(modifier);
        }

        Transform target;
        public void SetTarget(Transform transform) => target = transform;


        #region Jump Ability

        public void JumpToTarget()
        {
            agentRootMovement.AllowRootMovement = false;
            transform.DOKill();
            transform.DOLookAt(target.position, 0.1f).onComplete += () =>
            {
                Calculate();
                isJumping = true;
            };
        }

        public Action Landing;
        public void GroundSmashLanding()
        {
            isJumping = false;
            agentRootMovement.AllowRootMovement = true;
            Landing?.Invoke();
        }

        bool isJumping = false;
        public float sec = 2;
        public float angle = 60;

        Vector3 moveDirection;        
        float distance;
        float vy;
        float g;
        public void Calculate()
        {
            moveDirection = target.position - transform.position;
            distance = (target.position - transform.position).magnitude - 0.1f;
            moveDirection.Normalize();
            vy = distance / sec * Mathf.Tan(angle * Mathf.Deg2Rad);
            g = 2 * distance * Mathf.Tan(angle * Mathf.Deg2Rad) / (sec * sec);
        }
        #endregion

        void FixedUpdate()
        {
            if (!isJumping)
                return;

            Vector3 vel = moveDirection * distance / sec;
            vy -= Time.fixedDeltaTime * g;
            model.position += Vector3.up * vy * Time.fixedDeltaTime;

            agentRootMovement.controller.Move(vel * Time.fixedDeltaTime);

            if ((target.position - transform.position).magnitude < 1f || model.position.y < 0)
            {
                GroundSmashLanding();
                model.localPosition = Vector3.zero;
            }
            
            if(Physics.Raycast(model.position, Vector3.down, out RaycastHit hitInfo, 10f))
            {
                if(hitInfo.distance < 6f)
                {
                    animator.SetTrigger("Hit Ground");
                }
            }
        }
    }
}