using System;
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

public class StatMediator : MonoBehaviour
{
    private bool baked = false;

    public Action OnStatChange;

    public List<BasicStats> modifiers = new();

    readonly Dictionary<StatType ,float> stats = new()
    {
        {StatType.Flat_Health ,0f},
        {StatType.Perc_Health ,0f},

        {StatType.Mana ,0f},
        {StatType.Resistance ,0f},    
        
        {StatType.Flat_Atk ,0f},
        {StatType.Perc_Atk ,0f},
    };

    public void AddStats(BasicStats stats)
    {
        OnStatChange?.Invoke();
        modifiers.Add(stats);
        baked = false;
    }

    public void RemoveStats(BasicStats stats)
    {
        OnStatChange?.Invoke();
        modifiers.Remove(stats);
        baked = false;
    }

    public float Query(StatType type)
    {
        if(!baked)
            CalculateStats();
        return stats[type];
    }

    public void CalculateStats()
    {
        ResetStats();
        Debug.Log("Do stats calculate ...");
        foreach(BasicStats stat in modifiers)
        {
            stats[StatType.Flat_Health] += stat.flat_health;
            stats[StatType.Perc_Health] += stat.perc_health;

            stats[StatType.Mana] += stat.mana;
            stats[StatType.Resistance] += stat.resistance;

            stats[StatType.Flat_Atk] += stat.flat_atk;
            stats[StatType.Perc_Atk] += stat.perc_atk;
        }
        baked = true;
    }

    public void ResetStats()
    {
            stats[StatType.Flat_Health] = 0;
            stats[StatType.Perc_Health] = 0;

            stats[StatType.Mana] = 0;
            stats[StatType.Resistance] = 0;

            stats[StatType.Flat_Atk] = 0;
            stats[StatType.Perc_Atk] = 0;
    }
}
