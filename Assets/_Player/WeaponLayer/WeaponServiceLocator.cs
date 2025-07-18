using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player data that weapon required
public class WeaponServiceLocator : MonoBehaviour
{
    public EntityComponent entityComponent;

    [SerializeField] PlayerMovementHandler PMovement;
    public MovementUtilities playerMovementUtilities
    {
        get => PMovement.movementUtilities;
    }
    public PlayerAnimationSystem animationSystem;
    [SerializeField] private CharacterStats stats;
    public CharacterStats GetCharacterStats() => stats;

    public EnemyDetection enemyData;

}
