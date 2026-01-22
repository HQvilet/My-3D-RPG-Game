using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AgentRootMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] public CharacterController controller;
    [SerializeField] public NavMeshAgent agent;
    public bool AllowRootMovement;
    public bool RotateTowardMovement = true;

    public Action OnFinishTravel;
    public Action<Vector3> OnAgentMove;


    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
        agent.speed = 0;
    }

    // private Vector2 Velocity;
    // private Vector2 SmoothDeltaPosition;
    void OnAnimatorMove()
    {
        if (AllowRootMovement)
            ApplyRootMovement();
    }

    bool once = true;
    void ApplyRootMovement()
    {        
        Vector3 deltaPos = animator.deltaPosition;
        Vector3 predictPos = agent.steeringTarget;
        
        if ((predictPos - transform.position).magnitude > 0.05f && RotateTowardMovement)
            agent.transform.DOLookAt(MyUtils.ModifyVector(predictPos, y:transform.position.y), 0.4f);

        if ((agent.destination - transform.position).magnitude < 0.4f)
        {
            if (once)
            {
                OnFinishTravel?.Invoke();
                once = false;
            }
        }
        else
        {
            once = true;
        }
        controller.Move(deltaPos);
    }

    public Vector3 GetLookDirection()
    {
        return (agent.steeringTarget - transform.position).normalized;
    }

    public void IgnoreAgent() => agent.enabled = false;
    public void UseAgent() => agent.enabled = true;
    
    // private void SynchronizeAnimatorAndAgent()
    // {
    //     Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
    //     worldDeltaPosition.y = 0;
    //     // Map 'worldDeltaPosition' to local space
    //     float dx = Vector3.Dot(transform.right, worldDeltaPosition);
    //     float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
    //     Vector2 deltaPosition = new Vector2(dx, dy);

    //     // Low-pass filter the deltaMove
    //     float smooth = Mathf.Min(1, Time.deltaTime / 0.1f);
    //     SmoothDeltaPosition = Vector2.Lerp(SmoothDeltaPosition, deltaPosition, smooth);

    //     Velocity = SmoothDeltaPosition / Time.deltaTime;
    //     if (agent.remainingDistance <= agent.stoppingDistance)
    //     {
    //         Velocity = Vector2.Lerp(Vector2.zero, Velocity, agent.remainingDistance);
    //     }

    //     bool shouldMove = Velocity.magnitude > 0.5f && agent.remainingDistance > agent.stoppingDistance;

    //     controller.Move(new Vector3(Velocity.x, 0, Velocity.y));

    //     // LookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

    //     //float deltaMagnitude = worldDeltaPosition.magnitude;
    //     //if (deltaMagnitude > Agent.radius / 2)
    //     //{
    //     //    transform.position = Vector3.Lerp(Animator.rootPosition, Agent.nextPosition, smooth);
    //     //}
    // }

}