using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmourSlotEquipment : SlotUnit ,IDragHandler
{
    // [SerializeField] MyEquipment equipment;

    public ArmourAsset armourAsset;
    [SerializeField] private ArmourType requiredType;

    public bool TryEquipArmour(ArmourAsset assetData)
    {
        if(assetData == null)
            return false;

        if(assetData.IsEmpty())
            return false;

        if(IsFull())
            return false;

        if(assetData.ItemData.armourType == requiredType)
        {
            // armourAsset.ItemData = assetData.ItemData;
            Bus<EquipArmourEvent>.Raise(new EquipArmourEvent(assetData));
            armourAsset.Copy(assetData);
            // ArmourEquipmentMenu.OnEquip?.Invoke(item);
            return true;
        }
        return false;
    }

    public bool TryRemoveArmour()
    {
        if(armourAsset == null)
            return false;

        if(!IsFull())
            return false;

        // if(item.armourType == requiredType)
        // {
        //     armourItem = null;
        //     return true;
        // }
        // ArmourEquipmentMenu.OnUnequip?.Invoke(armourItem);
        armourAsset = null;
        return true;
    }

    public void ClearSlot()
    {
        armourAsset = null;
    }

    void Update()
    {
        SetSprite(armourAsset);
        SetName(armourAsset);
    }

    public bool IsFull() => armourAsset.IsFull();

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.DragSlot = this;
    }
}
