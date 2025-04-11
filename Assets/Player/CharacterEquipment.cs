using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField] StatMediator stats;

    public void Equip(ArmourItem item)
    {
        stats.AddStats(item.stats);
    }

    public void Unequip(ArmourItem item)
    {
        stats.RemoveStats(item.stats);
    }
}
