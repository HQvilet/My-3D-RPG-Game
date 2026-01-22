using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputDataHandler : MonoBehaviour
{
    PlayerInputAction playerInput;
    [HideInInspector] public PlayerInputAction.PlayerActions playerInputAction;
    [HideInInspector] public PlayerInputAction.UIInteractionActions PlayerUIInteraction;

    private Vector2 _movementInput;
    public bool HasMotionInput { get => playerInputAction.OnMove.ReadValue<Vector2>() != Vector2.zero; }
    
    public Vector2 look => playerInputAction.Look.ReadValue<Vector2>();
    public Vector2 move => playerInputAction.OnMove.ReadValue<Vector2>();
    public bool jump => playerInputAction.Jump.IsPressed();

    protected void Awake()
    {
        playerInput = new PlayerInputAction();
        playerInputAction = playerInput.Player;

        EnablePlayerInput();

        GameManager.Instance.OnGamePaused += DisablePlayerInput;
        GameManager.Instance.OnGameResumed += EnablePlayerInput;

    }

    public void EnablePlayerInput()
    {
        playerInputAction.Enable();
    }
    public void DisablePlayerInput()    
    {
        playerInputAction.Disable();
    }

    
    public void LockMovement() => playerInputAction.OnMove.Disable();
    public void UnloclMovement() => playerInputAction.OnMove.Enable();

    public Vector3 MoveDirection()
    {
        _movementInput = playerInputAction.OnMove.ReadValue<Vector2>();
        return MyUtils.VectorTranslate(_movementInput);
    }

    // public Vector3 AimPoint(float range, LayerMask layer, out RaycastHit hit)
    // {
    //     Ray aimRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
    //     if(Physics.Raycast(aimRay, out hit, , range))
    //     {
    //         return hit.point;
    //     }
    // }

    public void OnDestroy()
    {
        Debug.Log("destroy player input");
        playerInputAction.Disable();
        playerInputAction.Attack.RemoveAction();
        playerInputAction.Dash.RemoveAction();
        playerInputAction.AbilitySwap.RemoveAction();
        playerInputAction.WeaponSwap.RemoveAction();

        playerInput.Dispose();
    }

}
