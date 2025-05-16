using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;


//flexible inventory v
//category inventory o

public class InventoryUI : MonoBehaviour
{
    // [SerializeField] private GameObject SlotPref;

    // public List<ItemSlotUnit> Slots;// = new List<SlotUnit>(InventoryData.INVENTORY_CAPACITY);
    List<RaycastResult> raycastResults = new List<RaycastResult>();

    public bool IsHoverUI;
    
    public SlotUnit DragSlot;
    public SlotUnit DropSlot;

    public void Start()
    {
        gameObject.SetActive(false);
        
    }


    public void Update()
    {
        GetHoveringUIElements();
        if(Input.GetMouseButtonUp(0))
            DragAndDropAction();
    }

    void DragAndDropAction()
    {
        if(DragSlot == null)
            return;

        foreach(RaycastResult castInfo in raycastResults)
        {
            if(castInfo.gameObject.TryGetComponent(out ItemSlotUnit dropSlot))
            {
                ItemSlotUnit dragSlotUnit = DragSlot as ItemSlotUnit;

                if(dragSlotUnit.itemSlotData.IsEmpty())
                    return;

                if(DragSlot.slotType == SlotType.ITEM_SLOT)
                {
                    InventoryManager.Instance.ExchangeItem(dragSlotUnit,dropSlot);
                }

            }
            else if(castInfo.gameObject.TryGetComponent(out ArmourSlotEquipment armourEquipSlot))
            {
                if(DragSlot.slotType == SlotType.ARMOUR_ITEM_SLOT)
                {
                    InventoryManager.Instance.EquipItem(DragSlot as ArmourSlotUnit ,armourEquipSlot);
                }
            }
            else if(castInfo.gameObject.TryGetComponent(out ArmourSlotUnit armourSlot))
            {

                if(DragSlot.slotType == SlotType.ARMOUR_ITEM_SLOT)
                {
                    InventoryManager.Instance.ExchangeItem(DragSlot as ArmourSlotUnit, armourSlot);
                }
                else if(DragSlot.slotType == SlotType.ARMOUR_EQUIP_SLOT)
                {
                    InventoryManager.Instance.RemoveArmour(armourSlot, DragSlot as ArmourSlotEquipment);
                }
            }
        }
        
    }
    
    public void GetHoveringUIElements()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerEventData ,raycastResults);

    }

}
