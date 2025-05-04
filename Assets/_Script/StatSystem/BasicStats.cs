using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Flat_Health , Perc_Health, 
    Mana ,
    Resistance ,
    Flat_Atk ,Perc_Atk
}

// [CreateAssetMenu(menuName = "Stats/Basic Stats")]
// public class BasicStats : ScriptableObject
// {
//     // public List<StatComponent> stats;
//     public float flat_health;
//     public float perc_health;
//     public float mana;
//     public float resistance;

//     public float flat_atk;
//     public float perc_atk;
// }

// [System.Serializable]
// public struct StatComponent
// {
//     public StatType type;
//     public int value;
// }

[System.Serializable]
public class BasicStatsConfig
{
    // public List<StatComponent> stats;
    public float flat_health;
    public float perc_health;
    public float mana;
    public float resistance;

    public float flat_atk;
    public float perc_atk;
}

