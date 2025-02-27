using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ItemSystem.ItemConfiguration;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SlotUnit : MonoBehaviour
{
    public ItemStack itemSlotData;
    public int index;

    [SerializeField] private TextMeshProUGUI nameHolder;
    [SerializeField] private Image imageHolder;
    [SerializeField] private TextMeshProUGUI amountHolder;

    public void SetSlotData(ItemStack item)
    {
        itemSlotData = item;

        UpdateSlot();
    }

    private void SetAmount()
    {
        int amount = itemSlotData.Amount;

        if(itemSlotData.ItemData == null)
        {
            amountHolder.text = string.Empty;
            return;
        }

        if(amount > 1)
            amountHolder.text = amount.ToString();
        else
            amountHolder.text = string.Empty;
    }

    private void SetSprite()
    {
        imageHolder.color = Color.white;

        if(itemSlotData.ItemData == null)
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }
            

        if(itemSlotData.Amount > 0)
            imageHolder.sprite = itemSlotData.ItemData.Sprite;
        else
            imageHolder.color = new Color(1 ,1 ,1 ,0);
    }

    private void SetName()
    {
        if(itemSlotData.ItemData == null)
        {
            nameHolder.text = string.Empty;
            return;
        }
            

        nameHolder.text = itemSlotData.ItemData.Name;
    }

    public void OnAmountChange()
    {
        SetAmount();
    }

    public void UpdateSlot()
    {
        SetName();
        SetSprite();
        SetAmount();
    }

    void Update()
    {
        UpdateSlot();
    }

    public void ColorTest()
    {
        imageHolder.color = new Color(255 ,0 ,0 ,255);
    } 



}
