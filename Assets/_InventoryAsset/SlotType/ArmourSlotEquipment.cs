using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmourSlotEquipment : SlotUnit ,IDragHandler
{
    // [SerializeField] MyEquipment equipment;

    public ArmourAsset armourAsset;
    [SerializeField] private ArmourType requiredType;

    public bool TryEquipArmour(ArmourAsset assetData)
    {
        if(assetData == null)
            return false;
        
        if(assetData.IsEmpty())
            return false;
        
        if(IsFull())
            return false;
        
        if(assetData.ItemData.armourType == requiredType)
        {
            Bus<EquipArmourEvent>.Raise(new EquipArmourEvent(assetData.armourRef));
            return true;
        }
        
        return false;
    }

    public bool TryRemoveArmour(out ArmourAsset removedAsset)
    {
        removedAsset = null;

        if(armourAsset == null)
            return false;

        if(armourAsset.IsEmpty())
            return false;

        // if(armourAsset != armourAsset)
        //     return false;

        removedAsset = armourAsset;
        Bus<UnequipArmourEvent>.Raise(new UnequipArmourEvent(armourAsset.armourRef));
        // armourAsset.armourRef = null;

        return true;
    }

    public void ClearSlot()
    {
        armourAsset = null;
    }

    public void UpdateSlot()
    {
        SetName(armourAsset);
        SetSprite(armourAsset);
    }

    void Update()
    {
        UpdateSlot();
    }

    public bool IsFull() => armourAsset.IsFull();
    public bool IsEmpty() => armourAsset.IsEmpty();

    public void OnDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.inventoryUI.DragSlot = this;
    }
}
