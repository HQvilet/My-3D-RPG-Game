using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    // [SerializeField] Transform followTarget;
    [SerializeField] public float _cinemachineTargetYaw;
    [SerializeField] public float _cinemachineTargetPitch;
    [SerializeField] float aimSensitivity = 2f;

    void Start()
    {
        
    }

    public void Process(Vector2 motion)
    {
        if (motion.sqrMagnitude >= 0.1f)
        {
            _cinemachineTargetYaw += motion.x * Time.deltaTime * aimSensitivity;
            _cinemachineTargetPitch -= motion.y * Time.deltaTime* aimSensitivity;
        }
        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = MyUtils.ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = MyUtils.ClampAngle(_cinemachineTargetPitch, -80f, 80f); 

        // Cinemachine will follow this target
        transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + 0.0f,
            _cinemachineTargetYaw, 0.0f);
    }

    public void SetEulerAngle(Quaternion rotation)
    {
        _cinemachineTargetPitch = rotation.eulerAngles.x;
        _cinemachineTargetYaw = rotation.eulerAngles.y;
    }

    public void SynchnizeOther(ThirdPersonCamera camera)
    {
        _cinemachineTargetPitch = camera._cinemachineTargetPitch;
        _cinemachineTargetYaw = camera._cinemachineTargetYaw;
    }
}
