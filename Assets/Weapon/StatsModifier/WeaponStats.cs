using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapon/WeaponStatsModifier")]
public class WeaponStats : ScriptableObject
{
    public float physicalDamage;

    public float elementalDamage;
    public float fireDamage;

    public float knockBack;
}

[CreateAssetMenu(menuName = "Weapon/DamageStats")]
public class DamageStats : ScriptableObject
{
    public float physicalDamage;

    public float elementalDamage;
    public float fireDamage;

    public float knockBack;
}

