using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMeshRenderer : MonoBehaviour
{

    [SerializeField] ProgressMeshRenderer progressMesh;

    public void SetProgress(float progress)
    {
        progressMesh.SetProgress(progress);
    }

    private void Update()
    {
        transform.LookAt(CameraCaching.mainCamera.transform ,Vector3.up);
    }
}
