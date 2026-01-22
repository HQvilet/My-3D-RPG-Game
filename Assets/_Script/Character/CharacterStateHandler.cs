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

    public Action<string> OnAnimationEvent;

    public Action<EntityComponent> OnHitTarget;

    public bool CanMove = true;
    public bool CanRoll = true;
    public bool AllowToInterupt = true;
    public bool IsAttacking = false;

    //handle animation interupt
    public float animationResistance = 0f;
}
