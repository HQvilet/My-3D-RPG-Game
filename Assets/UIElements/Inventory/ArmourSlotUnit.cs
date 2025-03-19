using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmourSlotUnit : SlotUnit ,IDragHandler
{
    public ArmourItem armourItem;
    public ArmourType requiredType;

    public bool TryEquipArmour(ArmourItem item)
    {
        if(item == null)
            return false;

        if(IsFull())
            return false;

        if(item.armourType == requiredType)
        {
            armourItem = item;
            return true;
        }
        return false;
    }

    public void ClearSlot()
    {
        armourItem = null;
    }

    void Update()
    {
        SetSprite(armourItem);
        SetName(armourItem);
    }

    public bool IsFull() => armourItem != null;

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.DragSlot = this;
    }
}
