using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{

    [Header("Physic Layer")]
    public Rigidbody rb;
    public ColliderDetection colliderDetection;
    public PlayerMovementData movementData;
    
    

    public Animator playerAnimator;
    public InputDataHandler Input;


    public PlayerStateHandler stateHandler;
    public PlayerStateMachine StateMachine;
    public MovementUtilities movementUtilities;
    void Awake()
    {
        StateMachine = new PlayerStateMachine(this);
        movementUtilities = new MovementUtilities(rb ,transform ,colliderDetection);
    }

    void FixedUpdate()
    {
        
        movementUtilities.Gravity(movementData.gravity * movementData.gravityMultiplier);
        StateMachine.PhysicUpdate();
    }

    void Update()
    {
        StateMachine.Update();
    }
    
}

public class MovementUtilities
{
    private Rigidbody rb;
    private Transform transform;
    private ColliderDetection colliderDetection;
    public MovementUtilities(Rigidbody rigidbody ,Transform transform ,ColliderDetection colliderDetection)
    {
        this.rb = rigidbody;
        this.transform = transform;
        this.colliderDetection = colliderDetection;
    }

    public void DoMove(Vector3 move_direction ,float speed ,bool rotate_on_move = true)
    {
        speed *= colliderDetection.IsOnSlope ? 0.8f : 1f;
        Vector3 currentVelocity = rb.velocity;
        // rb.AddForce(colliderDetection.GetSlopeDirection(move_direction) * speed - currentVelocity  ,ForceMode.VelocityChange);
        rb.velocity = colliderDetection.GetSlopeDirection(move_direction) * speed;
        if(rotate_on_move)
            RotateTowardDirection(move_direction);
        
    }

    public void Dash(float force)
    {
        DoMove(transform.forward ,force);
    }

    public void RotateTowardDirection(Vector3 direction)
    {
        // float target_angle = Mathf.Atan2(direction.x ,direction.z) * Mathf.Rad2Deg + 360 ;

        // float angle = transform.rotation.eulerAngles.y;

        transform.DOLookAt(direction + transform.position ,0.15f ,up : Vector3.up);

        // angle = Mathf.LerpAngle(angle ,target_angle ,0.3f);
        // transform.rotation = Quaternion.Euler(0 ,angle ,0);
    }

    public void RotateTowardTarget(Vector3 target)
    {
        // float target_angle = Mathf.Atan2(direction.x ,direction.z) * Mathf.Rad2Deg + 360 ;

        // float angle = transform.rotation.eulerAngles.y;

        transform.DOLookAt(new Vector3(target.x ,0 ,target.z) ,0.15f ,up : Vector3.up);

        // angle = Mathf.LerpAngle(angle ,target_angle ,0.3f);
        // transform.rotation = Quaternion.Euler(0 ,angle ,0);
    }

    public void DoJump(float jumpPulse)
    {
        // rb.AddForce(Vector3.up * jumpPulse ,ForceMode.VelocityChange);

        DoMove(Vector3.up ,jumpPulse);
    }

    public void Gravity(float gravity)
    {
        if(!colliderDetection.IsGrounded)
            rb.AddForce(Vector3.down * gravity ,ForceMode.Force);
    }
}
