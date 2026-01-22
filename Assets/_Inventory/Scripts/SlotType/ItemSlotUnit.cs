using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemSlotUnit : SlotUnit, IPointerClickHandler ,IDragHandler// ,IPointerEnterHandler ,IPointerExitHandler
{
    public ItemStack itemSlotData;

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
        SetSprite(itemSlotData);
        SetAmount(itemSlotData);
    }

    void Update()
    {
        UpdateSlot();
    }

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.SetDragingItem(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.OnSlotSelected?.Invoke(this);
    }
}
