using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BaseDamageableObject : MonoBehaviour
{

    [SerializeField] HealthBar healthBar;
    [SerializeField] CharacterStats _stats;
    // [Serializable]

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


    [SerializeField] protected float _maxHealth = 100f;
    public float MaxHealth
    {
        get
        {
            if(_stats != null)
                return _stats.Health;
            return _maxHealth;
        }
    }
    [SerializeField] protected float _health;
    void Start()
    {
        InitObject();
    }

    void Update()
    {
        _health = MaxHealth;
    }

    protected virtual void InitObject() => CurrentHealth = MaxHealth;

    public virtual void OnGetHit(CalculatedDamage damageVerifier)
    {
        // OnTakeDamage(attackStats.physicalDamage);
        effectModifier.SerilizeEffectSource(damageVerifier);
    }

    public virtual void OnTakeDamage(float damage)
    {
        CurrentHealth -= damage;
        DamageSpawner.Instance.VisualizeDamage(transform.position + Vector3.up * 0.1f + MyUtils.RandomizeVector3() * 1.9f ,damage);
    }

    protected virtual void OnDied()
    {
        // transform.DOKill();
        Destroy(gameObject ,0.11f);
        Debug.Log("Enemy died");
    }

    void OnEnable()
    {
        effectModifier.OnTakePhysicDamage += OnTakePhysicalDmg;
        
        effectModifier.OnTakeFireDamage += OnFire;
        effectModifier.OnGetKnockBack += OnGetKnockBack;
    }

    private void OnFire(float damageFactor)
    {
        if(!effectModifier.isOnFire)
            StartCoroutine(GetBurn(damageFactor));
    }
    IEnumerator GetBurn(float damageFactor)
    {
        float time = 2.1f;
        effectModifier.isOnFire = true;
        while(time > 0)
        {
            time -= 0.2f;
            OnTakeDamage(damageFactor);
            yield return new WaitForSeconds(0.2f);
        }
        effectModifier.isOnFire = false;
    }

    private void OnGetKnockBack(float damageFactor)
    {
        // StartCoroutine(GetKnockBack());\
        Debug.Log("Knock back");
        Transform obj = transform;
        if(obj != null)
            obj.DOMove(obj.transform.position + obj.forward * -1 * damageFactor  ,0.1f).onKill += ()=>{};
    }

    private void OnTakePhysicalDmg(float damageFactor)
    {
        OnTakeDamage(damageFactor);
    }
}
