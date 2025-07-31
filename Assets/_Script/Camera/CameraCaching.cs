using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCaching : MonoBehaviour
{
    public static Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
        // Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }
}
