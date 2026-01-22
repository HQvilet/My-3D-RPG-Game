using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using EditorAttributes;



public interface IDamageObject
{
    float CurrentHealth{get; set;}

    float MaxHealth{get; set;}

    void DealDamage(float damage);

    void AddHealth(float healthPoint);
}
 

public class BaseDamageableObject : MonoBehaviour
{

    [SerializeField] CharacterStats _stats;

    [SerializeField] protected BaseEffectModifier effectModifier;

    public event Action<float, float> OnHealthChange = delegate{};
    public event Action OnEntityDied = delegate{};
    [ReadOnly] [SerializeField] protected float _currentHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            //Callback method
            if (value <= 0)
                OnDied();
            _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
            OnHealthChange?.Invoke(_currentHealth, MaxHealth);
        }
    }

    public float MaxHealth
    {
        get
        {
            if (_stats != null)
                return _stats.Health;
            return 100;
        }
    }
    
    void Start()
    {
        ResetHealthState();
    }


    public void ResetHealthState() => CurrentHealth = MaxHealth;


    public virtual void OnTakeDamage(float damage, DmgType type = 0)
    {
        CurrentHealth -= damage;
        GameUIManager.Instance.damageVisualization.CreateVisualizeDamage(transform.position + Vector3.up * 1.1f, damage, type);
    }

    public virtual void OnGetHeal(float healAmount)
    {
        CurrentHealth += healAmount;
        GameUIManager.Instance.damageVisualization.CreateVisualizeDamage(transform.position + Vector3.up * 1.1f, healAmount, DmgType.HEAL);
    }

    protected virtual void OnDied()
    {
        OnEntityDied?.Invoke();
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}
