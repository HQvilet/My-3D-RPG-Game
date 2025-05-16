using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmourSlotUnit : SlotUnit, IDragHandler
{
    public ArmourAsset armourAsset;

    public void SetSlotData(ArmourAsset item)
    {
        armourAsset = item;
        UpdateSlot();
    }

    public void UpdateSlot()
    {
        SetName(armourAsset);
        SetSprite(armourAsset);
    }

    void Update()
    {
       UpdateSlot();
    }

    public void ClearSlot() => armourAsset.Clear();

    public bool IsFull() => armourAsset.IsFull();
    public bool IsEmpty() => armourAsset.IsEmpty();

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.DragSlot = this;
    }

}
