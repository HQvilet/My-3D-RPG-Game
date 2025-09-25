using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatMediator : MonoBehaviour
{
    private bool baked = false;

    public Action OnStatChange;

    public List<BasicStatsConfig> modifiers = new();

    readonly Dictionary<StatType ,float> _stats = new()
    {
        {StatType.Flat_Health ,0f},
        {StatType.Perc_Health ,0f},

        {StatType.Mana ,0f},
        {StatType.Resistance ,0f},    
        
        {StatType.Flat_Atk ,0f},
        {StatType.Perc_Atk ,0f},
    };
    

    public void AddStats(BasicStatsConfig stats)
    {
        OnStatChange?.Invoke();
        modifiers.Add(stats);
        baked = false;
    }

    public void RemoveStats(BasicStatsConfig stats)
    {
        OnStatChange?.Invoke();
        modifiers.Remove(stats);
        baked = false;
    }

    public float Query(StatType type)
    {
        if(!baked)
            CalculateStats();
        return _stats[type];
    }

    public void CalculateStats()
    {
        ResetStats();
        Debug.Log("Do stats calculate ...");
        foreach(BasicStatsConfig stat in modifiers)
        {
            _stats[StatType.Flat_Health] += stat.flat_health;
            _stats[StatType.Perc_Health] += stat.perc_health;

            _stats[StatType.Mana] += stat.mana;
            _stats[StatType.Resistance] += stat.resistance;

            _stats[StatType.Flat_Atk] += stat.flat_atk;
            _stats[StatType.Perc_Atk] += stat.perc_atk;
        }
        baked = true;
    }

    public void ResetStats()
    {
        _stats[StatType.Flat_Health] = 0;
        _stats[StatType.Perc_Health] = 0;

        _stats[StatType.Mana] = 0;
        _stats[StatType.Resistance] = 0;

        _stats[StatType.Flat_Atk] = 0;
        _stats[StatType.Perc_Atk] = 0;
    }

}
