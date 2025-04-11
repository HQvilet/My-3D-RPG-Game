using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class ArmourEquipmentMenu : MonoBehaviour//Singleton<ArmourEquipmentMenu>
{
    [SerializeField] CharacterEquipment equipment;

    public static Action<ArmourItem> OnEquip = delegate{};
    public static Action<ArmourItem> OnUnequip = delegate{};

    void Awake()
    {
        OnEquip += Equip;
        OnUnequip += Unequip;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Unequip(ArmourItem item)
    {
        equipment.Unequip(item);
    }

    private void Equip(ArmourItem item)
    {
        equipment.Equip(item);
    }
}
