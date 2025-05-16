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


    void Start()
    {
        gameObject.SetActive(false);
    }

}
