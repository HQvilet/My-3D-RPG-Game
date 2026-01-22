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
    PLAYER_HOTBAR_ITEM_SLOT,
    ARMOUR_EQUIP_SLOT,
    ARMOUR_ITEM_SLOT,
    WEAPON,
    ABILITY_SLOT,
    ABILITY_EQUIP_SLOT
}

public class SlotUnit : MonoBehaviour //,IDragHandler
{

    public SlotType slotType;
    [SerializeField] public Image imageHolder;
    [SerializeField] protected TextMeshProUGUI amountHolder;

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
        if(slotInfo == null || slotInfo.IsEmpty())
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }   

        if(!slotInfo.IsEmpty())
            imageHolder.sprite = slotInfo.GetItemData().Sprite;
        else
            imageHolder.color = new Color(1 ,1 ,1 ,0);
    }

    protected void SetSprite(Sprite sprite)
    {
        imageHolder.color = Color.white;
        if(sprite == null)
        {
            imageHolder.color = new Color(1 ,1 ,1 ,0);
            return;
        }   

        imageHolder.sprite = sprite;
        imageHolder.color = new Color(1 ,1 ,1 ,1);
    }


}
