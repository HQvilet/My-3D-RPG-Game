using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ItemSystem.ItemConfiguration;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public enum SlotType
{
    ITEM_SLOT,
    ARMOUR_EQUIP_SLOT,
    ARMOUR_ITEM_SLOT,
    WEAPON
}

public class SlotUnit : MonoBehaviour //,IDragHandler
{

    public SlotType slotType;
    [SerializeField] private TextMeshProUGUI nameHolder;
    [SerializeField] private Image imageHolder;
    [SerializeField] private TextMeshProUGUI amountHolder;

    protected void SetAmount(IInventorySlot slotInfo)
    {
        
        if(slotInfo == null)
        {
            amountHolder.text = string.Empty;
            return;
        }

        if(slotInfo.GetItemData() == null)
        {
            amountHolder.text = string.Empty;
            return;
        }

        if(slotInfo.Amount > 1)
            amountHolder.text = slotInfo.Amount.ToString();
        else
            amountHolder.text = string.Empty;
    }

    protected void SetSprite(IInventorySlot slotInfo)
    {
        imageHolder.color = Color.white;
        if(slotInfo == null)
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }

        if(slotInfo.IsEmpty())
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }
            

        if(!slotInfo.IsEmpty())
            imageHolder.sprite = slotInfo.GetItemData().Sprite;
        else
            imageHolder.color = new Color(1 ,1 ,1 ,0);
    }

    protected void SetName(IInventorySlot slotInfo)
    {
        if(slotInfo == null)
        {
            nameHolder.text = string.Empty;
            return;
        }

        if(slotInfo.IsEmpty())
        {
            nameHolder.text = string.Empty;
            return;
        }
        nameHolder.text = slotInfo.GetItemData().Name;
    }

    // public void OnAmountChange()
    // {
    //     SetAmount(itemSlotData);
    // }

    // public void UpdateSlot()
    // {
    //     SetName(itemSlotData.ItemData);
    //     SetSprite(itemSlotData.ItemData);
    //     SetAmount(itemSlotData);
    // }

    // void Update()
    // {
    //     UpdateSlot();
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     InventoryManager.Instance.inventoryUI.DragSlot = this;
    // }
}
