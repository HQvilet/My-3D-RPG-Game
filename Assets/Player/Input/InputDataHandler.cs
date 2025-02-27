using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class InputDataHandler : MonoBehaviour
{
    [HideInInspector] public PlayerInputAction SystemInput;
    [HideInInspector] public PlayerInputAction.PlayerActions PlayerInput;
    [HideInInspector] public PlayerInputAction.UIInteractionActions PlayerUIInteraction;

    private Vector2 MovementInput;
    public bool HasMotionInput { get => PlayerInput.OnMove.ReadValue<Vector2>() != Vector2.zero; }
    public bool HasJumpInput { get => PlayerInput.Jump.WasPerformedThisFrame(); }

    void Awake()
    {
        SystemInput = new PlayerInputAction();
        PlayerInput = SystemInput.Player;

        PlayerUIInteraction = SystemInput.UIInteraction;

        // PlayerInput.Jump.WasPerformedThisFrame()
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

// void Update() => Debug.Log(PlayerInput.OnMove.ReadValue<Vector2>());
    public Vector3 MoveDirection()
    {
        MovementInput = PlayerInput.OnMove.ReadValue<Vector2>();
        return MyUtils.VectorTranslate(MovementInput);
    }

}
