using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;

public class ArmourEquipmentMenu : MonoBehaviour//Singleton<ArmourEquipmentMenu>
{
    [SerializeField] CharacterEquipment equipment;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DoEquip(ArmourReference aRef) => equipment.DoEquip(aRef);
    public void DoUnequip(ArmourReference aRef) => equipment.DoUnequip(aRef);


}
