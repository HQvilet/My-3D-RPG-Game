using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSystem : MonoBehaviour
{
    // [SerializeField] private PlayerSubSkillAnimation AnimationEvent;
    [SerializeField] private Animator playerAnimator;
    public Animator rigAnimator;

    public AnimationSystem animationSystem;

    void Awake()
    {
        animationSystem = new AnimationSystem(playerAnimator ,playerAnimator.runtimeAnimatorController);
        // playerAnimator.CrossFade("Attack_1" ,1f);
    }


    void OnDestroy()
    {
        animationSystem.Destroy();        
    }

}
