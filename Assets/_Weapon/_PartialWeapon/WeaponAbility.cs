
using ItemSystem.ItemConfiguration;
using UnityEngine;

public abstract class WeaponAbility : ScriptableObject 
{
    public virtual bool OnEnableAbility(object weapon) => true;
    public virtual void Execute(object weapon){}
    public virtual bool OnDisableAbility(object weapon) => true;
}

public abstract class CoolDownWeaponAbility : WeaponAbility
{
    public float coolDownTime;
}


public class RealTimeCooldownAbility
{
    public CountdownTimer timer;
    public WeaponAbilityItem abilityItem;
}