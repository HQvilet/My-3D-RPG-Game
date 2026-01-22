using System;
using System.Collections.Generic;

public enum DamageTargetGroup
{
    NONE, ALL, EXCLUDE_MY_GROUP, EXCLUDE_SELF
}

public static class DamageHandler
{
    public static float Processor(CharacterStats stats, float dmgToModify)
    {
        return 0;
    }

    public static float Processor(EntityComponent target, List<DamageVerifier> verifier, CharacterStats stats, float multiplier = 1)
    {
        verifier.ForEach(dmg =>
        {
            // target.effectModifier.GetDamage(stats.Atk * dmg.amount * multiplier, dmg.type);
        });
        return 1;
    }

    // public static float Processor(EntityComponent target, DamageVerifier damage)

    public static bool CanDamageThisEntity(EntityComponent source, EntityComponent target, DamageTargetGroup targetGroup = DamageTargetGroup.EXCLUDE_MY_GROUP)
    {
        return source.groupType != target.groupType;
    }
}

[Serializable]
public struct DamageVerifier
{
    public float amount;
    public DmgType type;
}