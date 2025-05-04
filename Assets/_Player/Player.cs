using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player> 
{
    public PlayerMovementHandler PlayerMovementSystem;
    public PlayerAnimationSystem AnimationSystem;
    public PlayerStateHandler WeaponController;
}