using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemSlotUnit : SlotUnit ,IDragHandler// ,IPointerEnterHandler ,IPointerExitHandler
{
    public ItemStack itemSlotData;
    // public int index;

    public void SetSlotData(ItemStack item)
    {
        itemSlotData = item;
        UpdateSlot();
    }

    public void OnAmountChange()
    {
        SetAmount(itemSlotData);
    }

    public void UpdateSlot()
    {
        SetName(itemSlotData);
        SetSprite(itemSlotData);
        SetAmount(itemSlotData);
    }

    void Update()
    {
        UpdateSlot();
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.DragSlot = this;
    }

}
