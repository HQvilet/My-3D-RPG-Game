using System;
using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class PlayerHotbar : MonoBehaviour
{
    public ItemConsumptionUnit consumptionUnit;
    public InputAction numericKeyAction;
    public InputAction mouseScrollAction;

    int hotbarCount = 8;

    int currentIndexSlot = -1;

    void Start()
    {
        consumptionUnit = EntityComponentSystem.Instance.GetPlayerComponent().GetComponent<ItemConsumptionUnit>();
        // numericKeyAction.Enable();
        mouseScrollAction.Enable();
        // numericKeyAction.performed += (context) =>
        // {
        //     if(GameUIManager.Instance.isPausing)
        //         return;
        //     if(int.TryParse(context.control.displayName, out int numericKeyPressed))
        //     {
        //         SetSelectedItemInHotBar(numericKeyPressed - 1);
        //     }
        // };
        mouseScrollAction.performed += (context) =>
        {
            if(GameUIManager.Instance.isPausing)
                return;
            int delta = Convert.ToInt32(Mathf.Clamp(context.ReadValue<Vector2>().y, -1, 1));
            currentIndexSlot += delta;
            if(currentIndexSlot < 0)
                currentIndexSlot += hotbarCount;
            else if(currentIndexSlot >= hotbarCount)
                currentIndexSlot -= hotbarCount;
            SetSelectedItemInHotBar(currentIndexSlot);
        };
        // SetSelectedItemInHotBar(currentIndexSlot);
    }

    void SetSelectedItemInHotBar(int i)
    {
        currentIndexSlot = i;
        InventoryManager.Instance.inventoryUI.OnPlayerHotbarSelected?.Invoke(currentIndexSlot);
    }

    public void UseSelectedItem()
    {
        if(consumptionUnit == null)
            return;

        if(currentIndexSlot < 0 || currentIndexSlot >= hotbarCount)
            return;

        ItemSlotUnit itemSlot = InventoryManager.Instance.inventoryData._playerHotbarItemSlots[currentIndexSlot];
        ConsumableItem consumableItem = itemSlot.itemSlotData.ItemData as ConsumableItem;

        if(consumableItem == null)
            return;

        // Debug.Log(consumableItem.Name);
        if(consumptionUnit.TryConsumItem(consumableItem))
            InventoryManager.Instance.TryRemoveItem(itemSlot, 1);
        return;
        
    }

}
