using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArcherAnimationConfig
{
    
    public static int p_MoveSpeed = Animator.StringToHash("MoveSpeed");
    public static int p_IsMoving = Animator.StringToHash("IsMoving");
    public static int p_MoveX = Animator.StringToHash("VelX");
    public static int p_MoveY = Animator.StringToHash("VelZ"); 
    public static int p_IsAttack = Animator.StringToHash("IsAttack");
    public static int p_IsInDanger = Animator.StringToHash("IsInDanger");

    public static int a_Dodge = Animator.StringToHash("Dodge");
    
}
