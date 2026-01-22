using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(CameraCaching.Instance.mainCamera.transform ,Vector3.up);
    }
}
