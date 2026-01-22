using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectorSlot : SlotUnit
{
    public WeaponRef weaponRef;
    public void SetWeaponSlotData(WeaponRef w_ref)
    {
        if(w_ref == null)
            return;
        weaponRef = w_ref;
        SetSprite(w_ref.weaponSprite);
    }
    
    public void OnSelected()
    {
        RectTransform r_transform = transform as RectTransform;
        r_transform.sizeDelta = new Vector2(75, 75);
    }

    public void OnDeselected()
    {
        RectTransform r_transform = transform as RectTransform; 
        r_transform.sizeDelta = new Vector2(50, 50);
        
    }
}
