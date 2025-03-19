using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject SlotPref;
    [SerializeField] private UnityEngine.UI.Button openCloseButton;

    public List<ItemSlotUnit> Slots;// = new List<SlotUnit>(InventoryData.INVENTORY_CAPACITY);
    List<RaycastResult> raycastResults = new List<RaycastResult>();

    public bool IsHoverUI;

    public SlotUnit DragSlot;
    public SlotUnit DropSlot;

    public void Start()
    {
        openCloseButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(!gameObject.activeSelf);
            // Time.timeScale = gameObject.activeSelf ? 0 : 1;
        });
        int i = 0;
        // foreach(ItemStack item in InventoryManager.Instance.inventoryData.Items)
        // {
        //     var slot = Instantiate(SlotPref ,transform).GetComponent<SlotUnit>();
        //     slot.SetSlotData(item);
            
        //     slot.index = i++;
        //     Slots.Add(slot);
        // }
        // gameObject.SetActive(false);

        Slots = transform.GetComponentsInChildren<ItemSlotUnit>().ToList();
        foreach(ItemSlotUnit slot in Slots)
        {
            ItemStack item = new ItemStack();
            InventoryManager.Instance.inventoryData.Items.Add(item);
            slot.SetSlotData(item);
            slot.index = i++;
        }
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
            if(castInfo.gameObject.TryGetComponent(out ItemSlotUnit slot))
            {
                ItemSlotUnit itemSlotUnit = DragSlot as ItemSlotUnit;
                if(itemSlotUnit.itemSlotData.IsEmpty())
                    return;

                if(DragSlot.slotType == SlotType.Item)
                    InventoryManager.Instance.ExchangeItem(itemSlotUnit,slot);

                else if(DragSlot.slotType == SlotType.ARMOUR)
                {
                    ArmourSlotUnit armourSlotUnit = DragSlot as ArmourSlotUnit;
                    //Add item directly into slot
                    if(InventoryManager.Instance.TryAddItem(slot ,armourSlotUnit.armourItem ,1))
                        armourSlotUnit.ClearSlot();
                }
            }
            if(castInfo.gameObject.TryGetComponent(out ArmourSlotUnit armourSlot))
            {
                if(DragSlot.slotType == SlotType.Item)
                {
                    ItemSlotUnit itemSlotUnit = DragSlot as ItemSlotUnit;
                    if(armourSlot.TryEquipArmour(itemSlotUnit.itemSlotData.ItemData as ArmourItem))
                        itemSlotUnit.itemSlotData.Remove(1);
                    
                }

                
            }
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
