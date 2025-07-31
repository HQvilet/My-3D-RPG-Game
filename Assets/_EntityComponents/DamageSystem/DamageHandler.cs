using System;
using System.Collections.Generic;
// using 

public static class DamageHandler
{
    public static DamageModifier Processor(CharacterStats stats ,DamageModifier damageStats)
    {
        DamageModifier dmg = new DamageModifier();

        dmg.physicalDamage = stats.Atk * damageStats.physicalDamage;
        dmg.fireDamage = stats.Atk * damageStats.fireDamage;
        dmg.elementalDamage = stats.Atk * damageStats.elementalDamage;
        dmg.knockBack = damageStats.knockBack;

        return dmg;
    }
}