using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerMovementHandler : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            playerAnimator.SetFloat("MoveSpeed", value);
            moveSpeed = value;
        }
    }

    [Header("Physic Layer")]
    public ColliderDetection colliderDetection;
    public PlayerMovementData movementData;
    

    public Animator playerAnimator;
    public InputDataHandler Input;


    public CharacterStateHandler stateHandler;
    public MovementUtilities movementUtilities;
    public CharacterController controller;
    [SerializeField] PlayerRootMovementHandler rootMovement;

    void Awake()
    {
        MoveSpeed = 1.23f;
        movementUtilities = new MovementUtilities(transform, controller);
        stateHandler.OnMeleePerformed += OnMeleeAttackPerformed;
        stateHandler.OnMeleeCompletedState += OnFinishAttackState;

        rootMovement.OnDashFinish += () =>
        {
            stateHandler.CanMove = true;
        };
    }

    void DoDash()
    {
        if (stateHandler.CanDash && stateHandler.AllowToInterupt)
        {
            stateHandler.CanMove = false;
            playerAnimator.CrossFade("Roll1", 0.08f);
        }
    }

    void Update()
    {
        if (Input.HasMotionInput && stateHandler.CanMove && stateHandler.AllowToInterupt)
        {
            playerAnimator.SetBool("IsMoving", true);
            movementUtilities.DoMove(Input.MoveDirection(), movementData.runSpeed, true);
        }
        else
        {
            playerAnimator.SetBool("IsMoving", false);
        }

        movementUtilities.Gravity(movementData.gravity * movementData.gravityMultiplier);

        if(Input.PlayerInput.Dash.WasPerformedThisFrame())
        {
            DoDash();
        }
    }

    private void OnFinishAttackState()
    {
        stateHandler.CanMove = true;
    }

    private void OnMeleeAttackPerformed()
    {
        stateHandler.CanMove = false;
    }
}

