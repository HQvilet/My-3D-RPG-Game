using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Player data that weapon may required
public class WeaponServiceLocator : MonoBehaviour
{
    [SerializeField] PlayerMovementHandler _pMovement;
    public MovementUtilities playerMovementUtilities => _pMovement.movementUtilities;
    
    public EnemyDetection enemySense;

}