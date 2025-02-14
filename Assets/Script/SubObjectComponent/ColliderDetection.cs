using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ColliderDetection : MonoBehaviour
{

    [SerializeField] private Transform _groundTransform;
    [SerializeField] private LayerMask layerMask;

    [Header("Ground")]
    [SerializeField] private float _groundRayDistance;

    [Header("Slope")]
    [SerializeField] private float _slopeRayDistance;
    [SerializeField] private float MaxSlopeAngle = 40;

    public bool IsGrounded;
    public bool IsOnSlope;

    public float SlopeAngle;
    RaycastHit hitInfo;
    private void Awake(){
        
    }

    void Update()
    {
        GroundCheck();
        SlopeCheck();
    }

    private void GroundCheck() => IsGrounded = Physics.CheckSphere(_groundTransform.position ,_groundRayDistance ,layerMask);

    // private void GroundCheck() => IsGrounded = Physics.CheckCapsule(_groundTransform.position ,_groundRayDistance ,layerMask);


    private void SlopeCheck()
    {
        Physics.Raycast(_groundTransform.position ,Vector3.down ,out hitInfo ,_slopeRayDistance ,layerMask);
        SlopeAngle = Vector3.Angle(hitInfo.normal , Vector3.up);
        IsOnSlope = SlopeAngle > 0 && SlopeAngle < MaxSlopeAngle && IsGrounded;
    }

    public Vector3 GetSlopeDirection(Vector3 _move_direction)
    {
        if(IsOnSlope)
            return Vector3.ProjectOnPlane(_move_direction ,hitInfo.normal).normalized;
        return _move_direction;
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundTransform.position ,_groundRayDistance);
        Gizmos.DrawRay(_groundTransform.position ,Vector3.down * _slopeRayDistance);
    }





}
