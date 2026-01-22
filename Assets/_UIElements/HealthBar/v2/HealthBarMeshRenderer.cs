using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarMeshRenderer : MonoBehaviour
{

    [SerializeField] BaseDamageableObject healthObject;

    [SerializeField] ProgressMeshRenderer progressMesh;

    void Start()
    {
        healthObject.OnHealthChange += (currentHealth, maxHealth) =>
        {
            SetProgress(currentHealth/ maxHealth);  
        };
    }

    public void SetProgress(float progress)
    {
        progressMesh.SetProgress(progress);
    }

    private void Update()
    {
        transform.LookAt(CameraCaching.Instance.mainCamera.transform ,Vector3.up);
    }
}
