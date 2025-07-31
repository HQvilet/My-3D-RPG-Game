using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(menuName = "Weapon/DamageStats")]
// public class DamageStats : ScriptableObject
// {
//     public float physicalDamage;

//     public float elementalDamage;
//     public float fireDamage;

//     public float knockBack;
// }

[System.Serializable]
public struct DamageModifier
{
    public float physicalDamage;

    public float elementalDamage;
    public float fireDamage;

    public float knockBack;
}
