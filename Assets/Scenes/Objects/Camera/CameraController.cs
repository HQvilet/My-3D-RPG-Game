using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraController : MonoBehaviour
{



    private CinemachineInputProvider InputProvider;
    private CinemachineFramingTransposer FramingTransposer;

    private float default_target_distance;
    [SerializeField] private float min_target_zoom = 1f;
    [SerializeField] private float max_target_zoom = 1f;
    private float current_target_distance;

    [SerializeField] private float zoom_sensitive = 3;

    private void Awake()
    {
        InputProvider = GetComponent<CinemachineInputProvider>();
        FramingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Start()
    {
        default_target_distance = FramingTransposer.m_CameraDistance;
    }

    private void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        float zoom_value = InputProvider.GetAxisValue(2) * zoom_sensitive;

        current_target_distance = Mathf.Clamp(current_target_distance + zoom_value ,default_target_distance - min_target_zoom ,default_target_distance + max_target_zoom);

        float current_distance = FramingTransposer.m_CameraDistance;
        if(current_distance == current_target_distance){ return; }

        float smooth_zoom_value = Mathf.Lerp(current_distance ,current_target_distance ,Time.deltaTime * 4f);

        FramingTransposer.m_CameraDistance = smooth_zoom_value;

        


    }
}
