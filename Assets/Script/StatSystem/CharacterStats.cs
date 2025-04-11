using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public StatMediator mediator;

    public float Health
    {
        get => mediator.Query(StatType.Flat_Health) + mediator.Query(StatType.Perc_Health); // calculate flat / percentage stat
    }

    public float Mana
    {
        get => mediator.Query(StatType.Mana);
    }

    public float Resistance
    {
        get => mediator.Query(StatType.Resistance);
    }

    public float Atk
    {
        get => mediator.Query(StatType.Flat_Atk);
    }

    [SerializeField] float _attack;

    void OnStatsDebug()
    {
        _attack = Atk;
    }

    void Update()
    {
        OnStatsDebug();
    }

}
