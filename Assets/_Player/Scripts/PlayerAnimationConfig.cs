using UnityEngine;

public static class PlayerAnimationConfig
{
    public static int p_MoveSpeed = Animator.StringToHash("MoveSpeed");
    public static int p_IsMoving = Animator.StringToHash("IsMoving");
    public static int p_MoveX = Animator.StringToHash("MoveX");
    public static int p_MoveY = Animator.StringToHash("MoveY");
     
    public static int a_Roll = Animator.StringToHash("Roll");
    public static int a_GetHit = Animator.StringToHash("Hit");

}
