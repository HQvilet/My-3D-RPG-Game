using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponServiceLocator : MonoBehaviour
{
    public PlayerStateHandler stateHandler;

    [SerializeField] PlayerMovementHandler PMovement;
    public MovementUtilities playerMovementUtilities
    {
        get => PMovement.movementUtilities;
    }
    public PlayerAnimationSystem animationSystem;

    public SenseOfEnemy enemyData;
}
