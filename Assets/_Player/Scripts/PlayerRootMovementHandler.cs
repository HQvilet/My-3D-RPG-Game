using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRootMovementHandler : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterController controller;
    [SerializeField] public bool allowRootMovement;

    public Action OnDashFinish;

    void Start()
    {
        
    }

    public void TurnOnRootMovement()
    {
        
    }

    public void TurnOffRootMovement()
    {
        
    }

    void OnAnimatorMove()
    {
        if (!allowRootMovement)
            return;

        controller.Move(animator.deltaPosition);
    }

    public void FinishDash() => OnDashFinish?.Invoke();

}
