using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    // [SerializeField] private PlayerSubSkillAnimation AnimationEvent;
    [SerializeField] public Animator characterAnimator;
    public Animator rigAnimator;
    public RuntimeAnimatorController controller;

    public AnimationSystem animationSystem;

    void Awake()
    {
        // characterAnimator.fireEvents = false;
        animationSystem = new AnimationSystem(characterAnimator, controller);
        // playerAnimator.CrossFade("Attack_1" ,1f);
    }


    void OnDestroy()
    {
        animationSystem.Destroy();
    }

}
