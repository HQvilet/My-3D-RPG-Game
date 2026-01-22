using UnityEngine;

public static class ZombieAnimationConfig
{
    public static int p_IsMoving = Animator.StringToHash("IsMoving");
    public static int p_TargetInSight = Animator.StringToHash("TargetInSight");
    public static int p_IsAttack = Animator.StringToHash("IsAttack");
    public static int p_IsInDanger = Animator.StringToHash("IsInDanger");

    public static int p_IsFalling = Animator.StringToHash("IsFalling");
    public static int p_IsLanding = Animator.StringToHash("IsLanding");
    public static int a_JumpAttack = Animator.StringToHash("Jump Attack");
    public static int a_Punch = Animator.StringToHash("Attack");
    
}
