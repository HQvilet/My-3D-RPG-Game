using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponAbilitySlotEquipment : SlotUnit, IDragHandler, IPointerClickHandler
{
    [ReadOnly] public int index;
    public AbilityAsset abilityAsset;


    public void UpdateSlot()
    {
        SetSprite(abilityAsset);
    }

    void Update()
    {
       UpdateSlot();
    }

    public void ClearSlot() => abilityAsset?.Clear();
    public bool IsFull() => abilityAsset.IsFull();
    public bool IsEmpty() => abilityAsset.IsEmpty();

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.SetDragingItem(this);
    }

    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameUIManager.Instance.weaponAbilityUIHandler.SetSelectedAbility(abilityAsset.GetItemData() as WeaponAbilityItem);
    }
}
