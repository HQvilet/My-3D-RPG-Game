using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateHandler : MonoBehaviour
{

    // State event listener
    public Action OnMeleePerformed;
    public Action OnMeleeFinishedCombo;
    public Action OnMeleeCompletedState;

    public Action OnAiming;

    public Action<string> OnAnimationEvent;

    public Action<EntityComponent> OnHitTarget;
    public Action<EntityComponent> OnGetHit;


    // [Header("Abilities")]
    // Abilities
    public bool CanJump;
    public bool CanAttack = true;
    public bool CanDash;
    public bool IsMoving;
    public bool CanMove = true;
    public bool IsIdling;
}
