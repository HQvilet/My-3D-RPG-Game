// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerMovementState : State
// {

//     private PlayerConfig PlayerInfo;    
//     private PlayerStateMachine playerStateMachine;

//     public PlayerMovementState(PlayerStateMachine StateMachine)
//     {
//         playerStateMachine = StateMachine;
//         PlayerInfo = playerStateMachine.Player.playerConfig;

//         timeToReachTargetRotation = 0.12f;
//     }

//     protected float speedModifier;

// //Rotation Modifier
//     private Vector3 currentTargetRotation;
//     private float timeToReachTargetRotation;
//     private float rotationCurrentVelocity;
//     private float dampedTargetRotationPassedTime;
// //--------------------------------------

//     protected bool walkToggle = false;


//     protected bool CanMove = true;
//     protected bool CanRotate;

//     public override void PhysicUpdate()
//     {


//         playerStateMachine.Player.rb.AddForce(Vector3.down * 120);
//         Vector3 InputDirection = GetInputDirection();
//         if(CanMove){
//             // playerStateMachine.Player.rb.velocity = InputDirection * speedModifier *Time.fixedDeltaTime *100f;
//             playerStateMachine.Player.rb.AddForce(InputDirection * speedModifier);

//             RotateTowardDirection();
//             Debug.Log(playerStateMachine.Player.playerInput.playerInputAction.Player.OnMove.ReadValue<Vector2>());
//             if(playerStateMachine.Player.transform.rotation != Quaternion.LookRotation(InputDirection)){
//                 dampedTargetRotationPassedTime = 0f;
//             }
//         }
//     }

//     public override void Update()
//     {
//         base.Update();
//         if(playerStateMachine.Player.playerInput.InputDirection == Vector2.zero){
//             playerStateMachine.ChangeState(playerStateMachine.idlingState);
               
//         }
//     }

//     private Vector3 GetInputDirection(){
        
//         Vector2 _dir = playerStateMachine.Player.playerInput.InputDirection;
//         // Vector2 _dir = playerStateMachine.Player.playerInput.playerInputAction.Player.OnMove.ReadValue<Vector2>();  

//         if (_dir != Vector2.zero){
//             Vector3 _camera_direction = GetCameraPointingDirection();

//             float _input_dir_radian = Mathf.Atan2(_dir.x ,_dir.y);
            
//             float _camera_dir_radian = Mathf.Atan2(_camera_direction.x ,_camera_direction.z);

//             float _look_dir_radian = (_input_dir_radian + _camera_dir_radian)*Mathf.Rad2Deg;

//             currentTargetRotation.y = _look_dir_radian;

//             Vector3 _camera_2_input_direction = Quaternion.Euler( 0f ,_look_dir_radian  ,0f) * Vector3.forward;

//             return _camera_2_input_direction;
//         }
//         return Vector3.zero;
//     }

//     private Vector3 GetCameraPointingDirection(){
//         Vector3 _look_direction = playerStateMachine.Player.transform.position - PlayerInfo.MyCameraTransform.position;
//         return _look_direction;
//     }

//     private void RotateTowardDirection(){
//         // playerStateMachine.Player.transform.rotation = Quaternion.Slerp(playerStateMachine.Player.transform.rotation, Quaternion.Euler(0f ,targetRotation ,0f) ,Time.fixedDeltaTime * smoothRotationValue);
//         float currentYRotaion = playerStateMachine.Player.rb.rotation.eulerAngles.y;
        
//         float smoothYRotation = Mathf.SmoothDampAngle(currentYRotaion ,currentTargetRotation.y ,ref rotationCurrentVelocity ,timeToReachTargetRotation - dampedTargetRotationPassedTime);

//         dampedTargetRotationPassedTime += Time.fixedDeltaTime;

//         playerStateMachine.Player.rb.MoveRotation(Quaternion.Euler(0f ,smoothYRotation ,0f));

//     } 

//     public void OnWalkToggle(InputAction.CallbackContext context){
//         if(context.started){
//             walkToggle = !walkToggle;
            
//         }
//     }

// }
