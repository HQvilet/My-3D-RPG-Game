using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private ProgressBar _healthBar;

    private void Awake()
    {
        // SetUpHealthBar();
    }

    private void Update()
    {
        // UpdateHealthBarPosition();
        transform.LookAt(CameraCaching.mainCamera.transform ,Vector3.up);
    }


    private void SetUpHealthBar()
    {
        _healthBar.transform.SetParent(HealthBarCanvas.Instance.transform);
    }

    private void UpdateHealthBarPosition()
    {
        _healthBar.transform.position = transform.position;
    }

    public void SetProgress(float value)
    {
        _healthBar.SetProgress(value);
    }

    public void Destroy_HealthBar()
    {
        Destroy(_healthBar.gameObject ,0.01f);
    }


}
