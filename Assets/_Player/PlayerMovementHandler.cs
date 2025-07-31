using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMovementHandler : MonoBehaviour
{

    [Header("Physic Layer")]
    public ColliderDetection colliderDetection;
    public PlayerMovementData movementData;
    

    public Animator playerAnimator;
    public InputDataHandler Input;


    public PlayerStateHandler stateHandler;
    public PlayerStateMachine StateMachine;
    public MovementUtilities movementUtilities;

    public Rigidbody rb;
    public CharacterController controller;
    void Awake()
    {
        StateMachine = new PlayerStateMachine(this);
        movementUtilities = new MovementUtilities(rb, transform, colliderDetection, controller);
    }

    void FixedUpdate()
    {
        
        movementUtilities.Gravity(movementData.gravity * movementData.gravityMultiplier);
        movementUtilities.PhysicUpdate();
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
    public CharacterController controller;
    public MovementUtilities(Rigidbody rigidbody, Transform transform, ColliderDetection colliderDetection, CharacterController controller)
    {
        this.rb = rigidbody;
        this.transform = transform;
        this.colliderDetection = colliderDetection;
        this.controller = controller;
    }


    public void DoMove(Vector3 move_direction, float speed, bool rotate_on_move = true)
    {
        // if (rotate_on_move)
        //     RotateTowardDirection(move_direction);
        // controller.Move(move_direction * speed * Time.fixedDeltaTime);

        Vector3 look_direction = transform.position - CameraCaching.mainCamera.transform.position;

        look_direction = MyUtils.ModifiyVector(look_direction, y : 0);

        Quaternion t = Quaternion.LookRotation(look_direction);

        Vector3 move_orientation = t * move_direction;

        move_orientation.Normalize();
        controller.Move(move_orientation * speed * Time.fixedDeltaTime);
        if (rotate_on_move)
            RotateTowardDirection(move_orientation);
    }


    float dash_time = 0;
    Action OCD;
    public void DoDash(float force, Action OnCompleteDashing)
    {
        Timing.RunCoroutine(Dash(force, 0.2f, OnCompleteDashing));
    }

    IEnumerator<float> Dash(float force, float time, Action OnCompleteDashing)
    {
        dash_time = time;
        while (dash_time > 0)
        {
            dash_time -= Time.deltaTime;
            yield return 0;
        }
        OnCompleteDashing.Invoke();

    }

    public void RotateTowardDirection(Vector3 direction)
    {
        // float target_angle = Mathf.Atan2(direction.x ,direction.z) * Mathf.Rad2Deg + 360 ;

        // float angle = transform.rotation.eulerAngles.y;

        transform.DOLookAt(direction + transform.position, 0.15f, up: Vector3.up);

        // angle = Mathf.LerpAngle(angle ,target_angle ,0.3f);
        // transform.rotation = Quaternion.Euler(0 ,angle ,0);
    }

    public void RotateTowardTarget(Vector3 target)
    {
        // float target_angle = Mathf.Atan2(direction.x ,direction.z) * Mathf.Rad2Deg + 360 ;

        // float angle = transform.rotation.eulerAngles.y;

        transform.DOLookAt(new Vector3(target.x, 0, target.z), 0.15f, up: Vector3.up);

        // angle = Mathf.LerpAngle(angle ,target_angle ,0.3f);
        // transform.rotation = Quaternion.Euler(0 ,angle ,0);
    }

    public void DoJump(float jumpPulse)
    {
        // rb.AddForce(Vector3.up * jumpPulse ,ForceMode.VelocityChange);

        DoMove(Vector3.up, jumpPulse);
    }

    public void Gravity(float gravity)
    {
        // if(!colliderDetection.IsGrounded)
        //     rb.AddForce(Vector3.down * gravity ,ForceMode.Force);
        // if(!controller.IsGrounded())
            controller.Move(Vector3.down * gravity * Time.fixedDeltaTime);
    }

    public void PhysicUpdate()
    {

        if (dash_time > 0)
            DoMove(transform.forward, 20f, false);
    }
}