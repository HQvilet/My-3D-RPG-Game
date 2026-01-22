using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponAbilitySlot : SlotUnit, IDragHandler, IPointerClickHandler
{
    public AbilityAsset abilityAsset = new();

    public void UpdateSlot()
    {
        SetSprite(abilityAsset);
    }

    void Update()
    {
       UpdateSlot();
    }

    public void ClearSlot() => abilityAsset.Clear();
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
