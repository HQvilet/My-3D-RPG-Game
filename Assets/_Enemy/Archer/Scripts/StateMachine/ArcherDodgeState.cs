using System.Collections;
using System.Collections.Generic;
using AdvanceFSM;
using DG.Tweening;
using MEC;
using UnityEngine;

namespace Archer
{
    public class ArcherDodgeState : BaseArcherState
    {
        public ArcherDodgeState(ArcherBehaviour behaviour, Animator animator, AgentRootMovement agentMovement) : base(behaviour, animator, agentMovement)
        {

        }
        
        public override void Enter()
        {            

            DoDodge();
            behaviour.OnFinishDodge += DoDodge;
            animator.SetBool(ArcherAnimationConfig.p_IsInDanger, true);
        }

        void DoDodge()
        {
            behaviour.RotateTowardTarget(0.1f, () =>
            {
                behaviour.isDodging = true;
                animator.CrossFade(ArcherAnimationConfig.a_Dodge, 0.1f);
            });
        }

        public override void Exit()
        {
            behaviour.OnFinishDodge -= DoDodge;
            animator.SetBool(ArcherAnimationConfig.p_IsInDanger, false);
        }

        public override void PhysicUpdate(){}

        public override void Update()
        {
                
        }
    }
}