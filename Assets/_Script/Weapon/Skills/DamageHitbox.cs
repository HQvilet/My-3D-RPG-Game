using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;


[RequireComponent(typeof(Collider))]
public class DamageHitbox : MonoBehaviour
{
    // private DamageStats attackStats;
    // public void SetAttackDamage(DamageStats attackStats) => this.attackStats = attackStats;

    protected DamageModifier calculatedDamage;
    public void SetAttackDamage(DamageModifier damage) => this.calculatedDamage = damage;

    [SerializeField] protected EntityComponent sourceEntity;
    public void SetSourceDamage(EntityComponent entity) => this.sourceEntity = entity;

}
