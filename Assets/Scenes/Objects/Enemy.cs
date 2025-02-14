using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private float currentHealth;
    public float CurrentHealth{
        get{
            return currentHealth;
        }
        set{
            currentHealth = value;
            if (currentHealth <= 0f){
                Die();
            }
        }
    }

    [SerializeField] private float MaxHealth;  

    private void Start(){
        CurrentHealth = MaxHealth;
    }  

    void Update()
    {
        
    }

    public void DealDamage(float damage){
        CurrentHealth -= damage;
        Debug.Log("Hit " + damage + " damage");
    }

    private void Die(){
        Debug.Log("Enemy Die");
    }
}
