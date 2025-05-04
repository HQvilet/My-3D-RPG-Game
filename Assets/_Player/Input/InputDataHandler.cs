using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class InputDataHandler : Singleton<InputDataHandler>
{
    [HideInInspector] public PlayerInputAction.PlayerActions PlayerInput;
    [HideInInspector] public PlayerInputAction.UIInteractionActions PlayerUIInteraction;

    public Action OnPerformedAnAttack;


    private Vector2 MovementInput;
    public bool HasMotionInput { get => PlayerInput.OnMove.ReadValue<Vector2>() != Vector2.zero; }
    public bool HasJumpInput { get => PlayerInput.Jump.WasPerformedThisFrame(); }
    public bool PerformedAnInteract {get => PlayerInput.Interact.WasPerformedThisFrame(); }
    // public bool PerformedAnAttack {get => PlayerInput.Attack.WasPerformedThisFrame(); }


    protected override void Awake()
    {
        base.Awake();
        PlayerInput = SystemInputManager.Instance.SystemInput.Player;

        PlayerUIInteraction = SystemInputManager.Instance.SystemInput.UIInteraction;

        EnablePlayerInput();
    }

    public void EnablePlayerInput()
    {
        PlayerInput.Enable();
        PlayerUIInteraction.Enable();
    }
    public void DisablePlayerInput()    
    {
        PlayerInput.Disable();
        PlayerUIInteraction.Disable();
    }

    
    public void LockMovement() => PlayerInput.OnMove.Disable();
    public void UnloclMovement() => PlayerInput.OnMove.Enable();

    public Vector3 MoveDirection()
    {
        MovementInput = PlayerInput.OnMove.ReadValue<Vector2>();
        return MyUtils.VectorTranslate(MovementInput);
    }

}
