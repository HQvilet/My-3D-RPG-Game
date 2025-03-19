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
    Item ,
    ARMOUR ,
    WEAPON
}

public class SlotUnit : MonoBehaviour //,IDragHandler
{
    // public ItemStack itemSlotData;
    // public int index;
    public SlotType slotType;
    [SerializeField] private TextMeshProUGUI nameHolder;
    [SerializeField] private Image imageHolder;
    [SerializeField] private TextMeshProUGUI amountHolder;

    //place holder

    // public void SetSlotData(ItemStack item)
    // {
    //     itemSlotData = item;

    //     UpdateSlot();
    // }

    protected void SetAmount(ItemStack itemStack)
    {
        int amount = itemStack.Amount;

        if(itemStack.ItemData == null)
        {
            amountHolder.text = string.Empty;
            return;
        }

        if(amount > 1)
            amountHolder.text = amount.ToString();
        else
            amountHolder.text = string.Empty;
    }

    protected void SetSprite(ItemStack itemStack)
    {
        imageHolder.color = Color.white;

        if(itemStack.ItemData == null)
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }
            

        if(!itemStack.IsEmpty())
            imageHolder.sprite = itemStack.ItemData.Sprite;
        else
            imageHolder.color = new Color(1 ,1 ,1 ,0);
    }

    protected void SetSprite(ItemData item)
    {
        imageHolder.color = Color.white;

        if(item == null)
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
        }
        else
        {
            imageHolder.sprite = item.Sprite;
        }
            

    }

    protected void SetName(ItemStack itemStack)
    {
        if(itemStack.IsEmpty())
        {
            nameHolder.text = string.Empty;
            return;
        }
        nameHolder.text = itemStack.ItemData.Name;
    }

    protected void SetName(ItemData item)
    {
        if(item == null)
        {
            nameHolder.text = string.Empty;
            return;
        }
        else
        {
            nameHolder.text = item.Name;
        }
        
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
