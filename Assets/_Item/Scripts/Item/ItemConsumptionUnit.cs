using ItemSystem.ItemConfiguration;
using MEC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public enum ConsumEffect
{
    NONE,
    HEAL_EFFECT_1, HEAL_EFFECT_5, HEAL_EFFECT_10,
    BUFF_ATK_1, BUFF_ATK_5, BUFF_ATK_10, 

}

public class ItemConsumptionUnit : MonoBehaviour
{
    
    public InputAction consumeItemAction;
    public UnityEvent consumItemEvent;

    [SerializeField] EntityComponent consumer;

    [SerializeField] GameObject healAuraVFX;
    [SerializeField] GameObject atkAuraVFX;

    BasicStatsConfig buffStats;

    void Start()
    {
        consumer = GetComponent<EntityComponent>();
        consumeItemAction.Enable();
        consumeItemAction.performed += (ctx) =>
        {
            consumItemEvent.Invoke();
        };

        buffStats = new();
        consumer.characterStats.mediator.AddStats(buffStats);
    }

    bool HealEffect(float amount)
    {
        consumer.damageableObject.OnGetHeal(amount);
        Instantiate(healAuraVFX, transform.position, Quaternion.identity, transform);
        return true;
    }

    string buff_atk_string = "attack_buff";
    bool BuffAttackEffect(float time, float amount)
    {
        if(consumer.effectModifier.effects.Contains(buff_atk_string))
            return false;
        buffStats.flat_atk += amount;
        consumer.characterStats.mediator.CalculateStats();
        consumer.effectModifier.AddEffect(buff_atk_string);
        var _vfx = Instantiate(atkAuraVFX, transform.position, Quaternion.identity, transform);
        Timing.RunCoroutine(MyUtils.WaitToAction(time, () =>
        {
            buffStats.flat_atk -= amount;
            consumer.characterStats.mediator.CalculateStats();
            consumer.effectModifier.RemoveEffect(buff_atk_string);
            Destroy(_vfx);
        }));

        return true;
    }

    public bool TryConsumItem(ConsumableItem item)
    {
        switch (item.util)
        {
            case ConsumEffect.HEAL_EFFECT_1:
                return HealEffect(20);;
            case ConsumEffect.HEAL_EFFECT_5:
                return HealEffect(40f);
            case ConsumEffect.HEAL_EFFECT_10:
                return HealEffect(60f);
            case ConsumEffect.BUFF_ATK_1:
                return BuffAttackEffect(30, 20f);
            case ConsumEffect.BUFF_ATK_5:
                return BuffAttackEffect(30, 40f);
            case ConsumEffect.BUFF_ATK_10:
                return BuffAttackEffect(30, 60f);
            default:
                return false;
        }
    }
}