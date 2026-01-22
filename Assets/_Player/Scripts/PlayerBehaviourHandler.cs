using System;
using System.Collections;
using DG.Tweening;
using JetBrains.Annotations;
using MEC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerBehaviourHandler : MonoBehaviour
{
    [Header("Input Action")]
    public InputDataHandler Input;

    [SerializeField] float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            playerAnimator.SetFloat(PlayerAnimationConfig.p_MoveSpeed, value);
            moveSpeed = value;
        }
    }

    // [Header("Physic Data")]
    // // public ColliderDetection colliderDetection;
    // public float gravity = -10f;
    // public float gravityMultiplier = 1f;

    // public float JumpHeight;
    // public float JumpTimeout = 0.50f;
    // public float FallTimeout = 0.15f;

    bool isGrounded;
    
    [Header("Controller")]
    public Animator playerAnimator;
    public EntityComponent entity;
    public CharacterController controller;
    [SerializeField] PlayerRootMovementHandler rootMovement;

    [Header("Camera Controller")]
    [SerializeField] ThirdPersonCamera aimCameraController;
    [SerializeField] ThirdPersonCamera freelookCameraController;

    public Transform savePoint;
    void Start()
    {
        MoveSpeed = 1.25f;
        
        entity.stateHandler.OnMeleePerformed += () => entity.stateHandler.CanMove = false;
        entity.stateHandler.OnMeleeCompletedState += () => entity.stateHandler.CanMove = true;
        
        entity.effectModifier.OnTakeDamage += (dmg, source, dmgType) =>
        {
            entity.effectModifier.SetInvincibleTime(0.3f);
            if(entity.stateHandler.AllowToInterupt && !entity.stateHandler.IsAttacking)
                playerAnimator.CrossFade(PlayerAnimationConfig.a_GetHit, Time.deltaTime);
        };

        rootMovement.OnDashFinish += () =>
        {
            entity.stateHandler.CanMove = true;
            entity.stateHandler.AllowToInterupt = true;
            entity.effectModifier.isInvincible = false;
        };

        Input.playerInputAction.Dash.performed += (ctx) =>
        {
            if(entity.stateHandler.AllowToInterupt && entity.stateHandler.CanRoll)
                DoMove(Input.MoveDirection(), Time.deltaTime * 2f, true, () => DoForwardRoll());
        };
        
        entity.damageableObject.OnEntityDied += () =>
        {
            playerAnimator.SetLayerWeight(1, 0f);
            gameObject.SetActive(false);
        };
                
        InGameEventHandler.Instance.OnGameResetState.AddListener(() =>
        {
            entity.damageableObject.ResetHealthState();
            transform.position = savePoint.position;
            gameObject.SetActive(true);
        });
    }

    void DoForwardRoll()
    {
        
        entity.effectModifier.SetInvincibleTime(0.95f);
        entity.stateHandler.CanMove = true;
        entity.stateHandler.IsAttacking = false;
        playerAnimator.CrossFade(PlayerAnimationConfig.a_Roll, Time.deltaTime * 2f);
    }

    // float _verticalVelocity;
    // float _terminalVelocity = 53.0f;

    // // timeout deltatime
    // float _jumpTimeoutDelta;
    // float _fallTimeoutDelta;
    // float _jumpBufferTime;
    // private void JumpAndGravity()
    // {
    //     if (isGrounded)
    //     {
    //         // reset the fall timeout timer
    //         _fallTimeoutDelta = FallTimeout;
            
    //         // playerAnimator.SetBool(_animIDJump, false);
    //         // playerAnimator.SetBool(_animIDFreeFall, false);

    //         // stop our velocity dropping infinitely when grounded
    //         if (_verticalVelocity < 0.0f)
    //         {
    //             _verticalVelocity = -2f;
    //         }

    //         // Jump
    //         if (Input.PlayerInput.Jump.WasPerformedThisFrame() && _jumpTimeoutDelta <= 0.0f)
    //         {
    //             // the square root of H * -2 * G = how much velocity needed to reach desired height
    //             _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * gravity);

    //             // update animator if using character
    //             // playerAnimator.SetBool(_animIDJump, true);
    //         }

    //         // jump timeout
    //         if (_jumpTimeoutDelta >= 0.0f)
    //         {
    //             _jumpTimeoutDelta -= Time.deltaTime;
    //         }
    //     }
    //     else
    //     {
    //         // reset the jump timeout timer
    //         _jumpTimeoutDelta = JumpTimeout;

    //         // fall timeout
    //         if (_fallTimeoutDelta >= 0.0f)
    //         {
    //             _fallTimeoutDelta -= Time.deltaTime;
    //         }
    //         else
    //         {
    //             // update animator if using character
    //             // playerAnimator.SetBool(_animIDFreeFall, true);
    //         }

    //         // if we are not grounded, do not jump
    //         // _input.jump = false;
    //     }

    //     // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
    //     if (_verticalVelocity < _terminalVelocity)
    //     {
    //         _verticalVelocity += gravity * Time.deltaTime;
    //     }
    // }

    // void DoJump()
    // {
    //     isGrounded = Physics.CheckSphere(transform.position, 0.2f);
    //     JumpAndGravity();
    //     controller.Move(Vector3.up * _verticalVelocity);
    // }

    Vector3 smooth_direction = Vector3.zero;
    public Vector3 AnimatorMoveDirection
    {
        get => new Vector3(playerAnimator.GetFloat(PlayerAnimationConfig.p_MoveX), 0, playerAnimator.GetFloat(PlayerAnimationConfig.p_MoveY));
        set
        {
            smooth_direction = Vector3.Lerp(smooth_direction, value, 0.25f);
            playerAnimator.SetFloat(PlayerAnimationConfig.p_MoveX, smooth_direction.x);
            playerAnimator.SetFloat(PlayerAnimationConfig.p_MoveY, smooth_direction.z);
        }
    }

    float blockTimer;

    void Update()
    {
        if(!entity.stateHandler.AllowToInterupt)
        {
            blockTimer -= Time.deltaTime;
            if(blockTimer <= 0)
            {
                blockTimer = 0.7f;
                entity.stateHandler.AllowToInterupt = true;
            }
        }
        else
        {
            blockTimer = 0.7f;
        }
        
        if (Input.HasMotionInput && entity.stateHandler.CanMove && entity.stateHandler.AllowToInterupt)
        {
            Vector3 move_direction = Input.MoveDirection();
            playerAnimator.SetBool(PlayerAnimationConfig.p_IsMoving, true);
            if(!CameraCaching.Instance.isAimingCamera)
            {
                DoMove(move_direction, 0.2f, true);
            }
                
        }
        else
        {
            playerAnimator.SetBool(PlayerAnimationConfig.p_IsMoving, false);
        }

        AnimatorMoveDirection = Input.MoveDirection();

        if(CameraCaching.Instance.isAimingCamera)
        {
            transform.rotation = Quaternion.Euler(0, aimCameraController.transform.rotation.eulerAngles.y, 0);
        }
    }

    void LateUpdate()
    {
        aimCameraController.Process(Input.look);
        freelookCameraController.Process(Input.look);
    }

    void FixedUpdate()
    {
        Gravity(9.8f);
    }

    Vector3 CalculateDirectionOnCameraProjection(Vector3 direction)
    {
        Vector3 look_direction = transform.position - CameraCaching.Instance.mainCamera.transform.position;
        look_direction = MyUtils.ModifyVector(look_direction, y : 0);
        Quaternion t = Quaternion.LookRotation(look_direction);

        Vector3 move_orientation = t * direction;

        return move_orientation.normalized;
    }

    public void DoMove(Vector3 move_direction, float rotationSpeed, bool rotate_on_move = true, Action onFinishRotate = null)
    {
        Vector3 move_orientation = CalculateDirectionOnCameraProjection(move_direction);
        if (rotate_on_move)
            transform.DOLookAt(move_orientation + transform.position, rotationSpeed, up: Vector3.up)
                .onComplete += () => onFinishRotate?.Invoke();
    }

    public void RotateTowardTarget(Vector3 target) => transform.DOLookAt(new Vector3(target.x, 0, target.z), 0.1f, up: Vector3.up);

    public void DashForward(float distance, float duration)
    {
        controller.Move(transform.forward * distance);
    }

    public void Gravity(float gravity)
    {
        controller.Move(Vector3.down * gravity * Time.fixedDeltaTime);
    }
}

