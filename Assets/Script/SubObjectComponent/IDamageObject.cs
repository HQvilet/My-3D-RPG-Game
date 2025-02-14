using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageObject
{
    float CurrentHealth{get; set;}

    float MaxHealth{get; set;}

    void DealDamage(float damage);

    void AddHealth(float healthPoint);
}
