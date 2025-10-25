
using ItemSystem.ItemConfiguration;
using UnityEngine;

public enum ConsumEffect
{
    NONE,
    HEAL_EFFECT_1, HEAL_EFFECT_5, HEAL_EFFECT_10,

}

public class ItemConsumptionUnit : MonoBehaviour
{
    [SerializeField] EntityComponent consumer;

    public bool ConsumItem(ConsumableItem item)
    {
        switch (item.util)
        {
            case ConsumEffect.HEAL_EFFECT_1:
                return true;
            default:
                return false;
        }
    }

}