using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public Rigidbody rb;

    public InputDataHandler Input;
    public PlayerStateMachine StateMachine;
    public ColliderDetection colliderDetection;

    public Animator playerAnimator;
    public PlayerMovementData movementData;

    void Awake()
    {
        StateMachine = new PlayerStateMachine(this);
    }

    void FixedUpdate()
    {
        StateMachine.PhysicUpdate();
        if(!colliderDetection.IsGrounded)
            rb.AddForce(Vector3.down * movementData.gravity ,ForceMode.Force);
    }

    void Update()
    {
        StateMachine.Update();
    }

    public void DoMove(Vector3 move_direction ,float speed)
    {
        speed *= colliderDetection.IsOnSlope ? 0.8f : 1f;
        rb.AddForce(colliderDetection.GetSlopeDirection(move_direction) * speed  ,ForceMode.VelocityChange);
        RotateTowardDirection(move_direction);
    }

    void RotateTowardDirection(Vector3 direction)
    {
        float target_angle = Mathf.Atan2(direction.x ,direction.z) * Mathf.Rad2Deg + 360 ;

        float angle = transform.rotation.eulerAngles.y;

        angle = Mathf.LerpAngle(angle ,target_angle ,0.3f);
        transform.rotation = Quaternion.Euler(0 ,angle ,0);
    }

    public void DoJump(float jumpPulse)
    {
        rb.AddForce(Vector3.up * jumpPulse ,ForceMode.Impulse);
    }
    
}
