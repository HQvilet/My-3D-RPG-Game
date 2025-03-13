using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class BaseDamageableObject : MonoBehaviour
{

    [SerializeField] HealthBar healthBar;

    [SerializeField] protected BaseEffectModifier effectModifier;

    [SerializeField] protected float _currentHealth;
    public float CurrentHealth{
        get => _currentHealth;
        set 
        {
            //Callback method
            
            if(value <= 0)
                OnDied();
            _currentHealth = Mathf.Clamp(value ,0 ,MaxHealth); 
            healthBar.SetProgress(_currentHealth/MaxHealth);
        } 
    }


    [SerializeField] protected float _maxHealth = 110f;
    public float MaxHealth
    {
        get => _maxHealth;
    }

    void Start()
    {
        InitObject();
    }

    protected virtual void InitObject() => CurrentHealth = MaxHealth;



    public virtual void OnGetHit(DamageStats attackStats)
    {
        // OnTakeDamage(attackStats.physicalDamage);
        effectModifier.SerilizeEffectSource(attackStats);
    }

    public virtual void OnTakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    protected virtual void OnDied()
    {
        Destroy(gameObject);
        Debug.Log("Enemy died");
    }

    void OnEnable()
    {
        effectModifier.OnTakePhysicDamage += test_1;
        effectModifier.OnGetKnockBack += test_2;
    }

    private void test_2(float damageFactor)
    {
        OnTakeDamage(damageFactor);
    }

    private void test_1(float damageFactor)
    {
        OnTakeDamage(damageFactor);
    }
}
