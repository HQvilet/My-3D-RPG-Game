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


    // [Header("Abilities")]
    // Abilities
    public bool CanJump;
    public bool CanAttack;
    public bool CanDash;
}
