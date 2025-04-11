using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class StatModifier
// {
//     public float Value;
//     public StatType Type;

//     StatModifier(StatType type ,float value)
//     {
//         Type = type;
//         Value = value;
//     }
// }

[CreateAssetMenu(menuName = "Stats/Basic Stats")]
public class BasicStats : ScriptableObject
{
    public float flat_health;
    public float perc_health;
    public float mana;
    public float resistance;

    public float flat_atk;
    public float perc_atk;

}
