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

                // if(DragSlot.slotType == SlotType.ARMOUR)
                // {

                //     ArmourSlotEquipment armourSlotUnit = DragSlot as ArmourSlotEquipment;
                //     //Add item directly into slot
                //     if(InventoryManager.Instance.TryAddItem(dropSlot ,armourSlotUnit.armourItem ,1))
                //     {
                //         armourSlotUnit.TryRemoveArmour();
                //         return;
                //     }
                // }

                if(dragSlotUnit.itemSlotData.IsEmpty())
                    return;

                if(DragSlot.slotType == SlotType.ITEM)
                {
                    InventoryManager.Instance.ExchangeItem(dragSlotUnit,dropSlot);
                }

            }
            if(castInfo.gameObject.TryGetComponent(out ArmourSlotEquipment armourEquipSlot))
            {
                ArmourSlotUnit dragSlotUnit = DragSlot as ArmourSlotUnit;
                
                // if(DragSlot.slotType == SlotType.ITEM)
                // {
                //     ItemSlotUnit itemSlotUnit = DragSlot as ItemSlotUnit;
                //     if(armourSlot.TryEquipArmour(itemSlotUnit.itemSlotData.GetItemData() as ArmourItem))
                        // itemSlotUnit.itemSlotData.Remove(1);
                // }
                if(dragSlotUnit != null)
                {
                    InventoryManager.Instance.EquipItem(dragSlotUnit ,armourEquipSlot);
                }
            }
            // else if(castInfo.gameObject.TryGetComponent(out ArmourSlotUnit armourSlot))
            // {
            //     ArmourSlotUnit dragArmour = DragSlot as ArmourSlotUnit;
                
            //     // if(DragSlot.slotType == SlotType.ITEM)
            //     // {
            //     //     ItemSlotUnit itemSlotUnit = DragSlot as ItemSlotUnit;
            //     //     if(armourSlot.TryEquipArmour(itemSlotUnit.itemSlotData.GetItemData() as ArmourItem))
            //             // itemSlotUnit.itemSlotData.Remove(1);
            //     // }

            //     if(dragArmour != null)
            //     {

            //     }
            // }
        }
        
        // if(DragSlot != null && DropSlot != null)
        // {
        //     if(DragSlot.slotType == SlotType.Item && DropSlot.slotType == SlotType.Item)
        //     {
        //         InventoryManager.Instance.ExchangeItem(DragSlot as ItemSlotUnit,DropSlot as ItemSlotUnit);
        //     }
        //     if(DragSlot.slotType == SlotType.ARMOUR && DropSlot.slotType == SlotType.Item)
        //     {
        //         // if()
        //         InventoryManager.Instance.ExchangeItem(DragSlot as ItemSlotUnit,DropSlot as ItemSlotUnit);
        //     }
        // }
        // 
    }
    
    
    public void GetHoveringUIElements()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointerEventData ,raycastResults);

    }



}
