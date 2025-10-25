using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateHandler : MonoBehaviour
{

    // State event listener
    public Action OnMeleePerformed;
    public Action OnMeleeFinishedCombo;
    public Action OnMeleeCompletedState;

    public Action OnAiming;

    public Action<string> OnAnimationEvent;

    public Action<EntityComponent> OnHitTarget;
    public Action<EntityComponent> OnGetHit;



    public bool CanJump = true;
    public bool CanAttack = true;
    public bool CanDash = true;
    public bool CanMove = true;
    public bool AllowToInterupt = true;
}
