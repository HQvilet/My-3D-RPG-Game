using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder;
using UnityEngine.UI;


//flexible inventory v
//category inventory o

public class InventoryUI : MonoBehaviour
{
    public InventoryItemDescription ItemDisplayPanel;

    [Header("Containers")]
    public Transform InventoryContainer;
    public Transform PlayerHotbarContainer;
    public Transform WeaponAbilityContainer;

    public Action<ItemSlotUnit> OnSlotSelected;
    public Action<int> OnPlayerHotbarSelected;

    [Header("Interactions")]
    public bool IsHoverUI;
    [HideInInspector] public SlotUnit DragSlot;

    [Header("Components")]
    // public RectTransform selectorMark;
    public RectTransform hotbarSelectorMark;
    public Image dragableSprite;

    public void Start()
    {
        // OnSlotSelected += (slot) =>
        // {
        //     RectTransform rectTransform = slot.transform as RectTransform;
        //     if(!selectorMark.gameObject.activeSelf)
        //         selectorMark.gameObject.SetActive(true);
        //     selectorMark.position = rectTransform.position;
        //     selectorMark.sizeDelta = rectTransform.sizeDelta + Vector2.one * 4;
        //     ItemDisplayPanel.SetItemDisplayData(slot.itemSlotData.ItemData);
        // };

        OnPlayerHotbarSelected += (idx) =>
        {
            if(idx < 0 || idx >= InventoryManager.Instance.inventoryData._playerHotbarItemSlots.Count)
            {
                hotbarSelectorMark.gameObject.SetActive(false);
                return;
            }
            hotbarSelectorMark.gameObject.SetActive(true);
            RectTransform rectTransform = InventoryManager.Instance.inventoryData._playerHotbarItemSlots[idx].transform as RectTransform;
            hotbarSelectorMark.position = rectTransform.position;
            hotbarSelectorMark.sizeDelta = rectTransform.sizeDelta + Vector2.one * 4;
        };

        GameUIManager.Instance.OnInventoryUIEnable += () =>
        {
            // selectorMark.gameObject.SetActive(false);
            // hotbarSelectorMark.gameObject.SetActive(false);
            ItemDisplayPanel.SetItemDisplayData(null);
        };

        GameUIManager.Instance.OnMainGameUIDisable += () =>
        {
            
        };
    }

    public void Update()
    {
        if(dragableSprite.gameObject.activeInHierarchy)
            dragableSprite.transform.position = Mouse.current.position.ReadValue();
        GetHoveringUIElements();
        if(Input.GetMouseButtonUp(0))
            DragAndDropAction();
    }

    public void SetDragingItem(SlotUnit slot)
    {
        DragSlot = slot;
        if(slot == null)
        {
            dragableSprite.gameObject.SetActive(false);
            return;
        }
        dragableSprite.gameObject.SetActive(true);
        dragableSprite.sprite = slot.imageHolder.sprite;
        
    }

    private void TrySetSelectedItem()
    {
        foreach(RaycastResult castInfo in raycastResults)
        {
            if (castInfo.gameObject.TryGetComponent(out ItemSlotUnit selectedSlot)) 
            {
                ItemDisplayPanel.SetItemDisplayData(selectedSlot.itemSlotData.ItemData);
                return;
            }
        }
    }



    List<RaycastResult> raycastResults = new List<RaycastResult>();
    void DragAndDropAction()
    {
        if(DragSlot == null)
            return;

        foreach(RaycastResult castInfo in raycastResults)
        {
            if (castInfo.gameObject.TryGetComponent(out ItemSlotUnit dropSlot))
            {
                if(DragSlot is ItemSlotUnit dragSlotUnit)
                {
                    if (dragSlotUnit.itemSlotData.IsEmpty())
                        break;
                    
                    InventoryManager.Instance.ExchangeItem(dragSlotUnit, dropSlot);
                }
            }
            // else if (castInfo.gameObject.TryGetComponent(out ArmourSlotEquipment armourEquipSlot))
            // {
            //     if (DragSlot.slotType == SlotType.ARMOUR_ITEM_SLOT)
            //     {
            //         InventoryManager.Instance.EquipArmourItem(DragSlot as ArmourSlotUnit, armourEquipSlot);
            //     }
            // }
            // else if (castInfo.gameObject.TryGetComponent(out ArmourSlotUnit armourSlot))
            // {
            //     if (DragSlot.slotType == SlotType.ARMOUR_ITEM_SLOT)
            //     {
            //         InventoryManager.Instance.ExchangeItem(DragSlot as ArmourSlotUnit, armourSlot);
            //     }
            //     else if (DragSlot.slotType == SlotType.ARMOUR_EQUIP_SLOT)
            //     {
            //         InventoryManager.Instance.RemoveArmour(armourSlot, DragSlot as ArmourSlotEquipment);
            //     }
            // }
            else if(castInfo.gameObject.TryGetComponent(out WeaponAbilitySlotEquipment equipment))
            {
                if(DragSlot is WeaponAbilitySlot weaponAbilitySlot)
                {
                    InventoryManager.Instance.AttachWeaponAbility(weaponAbilitySlot, equipment);
                }
            }
            else if(castInfo.gameObject.TryGetComponent(out WeaponAbilitySlot abilitySlot))
            {
                if(DragSlot is WeaponAbilitySlotEquipment weaponAbilityAttachment)
                {
                    InventoryManager.Instance.DettachWeaponAbility(abilitySlot, weaponAbilityAttachment);
                }
                else if(DragSlot is WeaponAbilitySlot abilitySlot1)
                {
                    if(!abilitySlot1.IsEmpty())
                        InventoryManager.Instance.ExchangeWeaponAbility(abilitySlot, abilitySlot1);
                }
            }
        }
        DragSlot = null;
        dragableSprite.gameObject.SetActive(false);
    }
    
    public void GetHoveringUIElements()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerEventData ,raycastResults);
    }

}
