using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] EntityComponent healthObject;
    [SerializeField] private ProgressBar _healthBar;

    void Start()
    {
        healthObject.damageableObject.OnHealthChange += (currentHealth, maxHealth) =>
        {
            SetProgress(currentHealth/ maxHealth);  
        };
    }

    private void SetUpHealthBar()
    {
        // _healthBar.transform.SetParent(HealthBarCanvas.Instance.transform);
        HealthBarCanvas.Instance.AddToHealthBarCanvas(this.transform);
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
