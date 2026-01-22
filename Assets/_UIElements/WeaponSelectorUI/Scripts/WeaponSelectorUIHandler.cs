using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectorUIHandler : MonoBehaviour
{
    public Transform weaponSelectorContainer;
    public WeaponSelectorSlot selectorSlotPref;

    public List<WeaponSelectorSlot> weaponSelectors;
    public int currentIndex = -1;

    public void AddToSelector(WeaponRef w_ref)
    {
        
        var a = Instantiate(selectorSlotPref, weaponSelectorContainer);
        a.SetWeaponSlotData(w_ref);
        weaponSelectors.Add(a);
    }

    public void ClearSelector()
    {
        weaponSelectors.Clear();
        foreach(var go in weaponSelectorContainer.GetComponentsInChildren<GameObject>())
            Destroy(go);
    }

    public void SetSelectedIndex(int i)
    {
        i -= 1;
        if(i < 0 || i >= weaponSelectors.Count)
            return;

        if(currentIndex == i)
            return;
        
        if(currentIndex >=0 && currentIndex < weaponSelectors.Count)
            weaponSelectors[currentIndex]?.OnDeselected();

        currentIndex = i;
        weaponSelectors[i].OnSelected();
    }
}
