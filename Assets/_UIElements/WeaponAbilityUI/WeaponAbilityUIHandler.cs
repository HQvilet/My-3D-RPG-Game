using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaveLoadSystem;
using ItemSystem.ItemConfiguration;
using MEC;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponAbilityUIHandler : MonoBehaviour
{ 
    [SerializeField] Transform abilitySlotAttachmentContainer;
    List<WeaponAbilitySlotEquipment> abilitySlots;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI weaponNameTextMesh;
    [SerializeField] Image weaponSprite;

    [SerializeField] Image abilitySprite;
    [SerializeField] TextMeshProUGUI abilityDescription;

    [Header("Data")]
    public WeaponAbilityItem SelectedAbility;
    public BaseWeapon currentWeapon;
    public Dictionary<WeaponType, BaseWeapon> weapons = new();

    

    void Awake()
    {
        abilitySlots = abilitySlotAttachmentContainer.GetComponentsInChildren<WeaponAbilitySlotEquipment>(true).ToList();
    }

    void Start()
    {
        ShowWeaponData(0);
    }

    public void SetSelectedAbility(WeaponAbilityItem weaponAbilityItem)
    {
        SelectedAbility = weaponAbilityItem;
        abilitySprite.sprite = weaponAbilityItem?.Sprite ?? null;
        abilityDescription.text = weaponAbilityItem?.Description ?? "";
    }

    public void SetWeaponAbilitiesData(BaseWeapon weapon)
    {
        if(weapons == null)
            weapons = new();

        if(!weapons.ContainsKey(weapon.weaponRefData.Type))
            weapons.Add(weapon.weaponRefData.Type, weapon);
        else
            weapons[weapon.weaponRefData.Type] = weapon;
    }

    public void ShowWeaponData(int type)
    {
        if(!weapons.TryGetValue((WeaponType)type, out BaseWeapon weapon))
            return;

        if(weapon == null)
            return;
            
        currentWeapon = weapon;
        weaponSprite.sprite = currentWeapon.weaponRefData.weaponSprite;
        ShowWeaponAbilities();
    }

    public void ShowWeaponAbilities()
    {
        foreach(WeaponAbilitySlotEquipment weaponAbilitySlot in abilitySlots)
        {
            weaponAbilitySlot.gameObject.SetActive(false);
            weaponAbilitySlot.abilityAsset = null;
        }
            
        if(currentWeapon == null)
            return;
        if(currentWeapon.weaponAbilities == null)
            return;

        for(int i = 0; i < currentWeapon.weaponRefData.abilityCount; ++i)
        {
            abilitySlots[i].gameObject.SetActive(true);
            abilitySlots[i].index = i;
            abilitySlots[i].abilityAsset = currentWeapon.weaponAbilities[i];
        }
    }

    public bool AddOrReplaceAbilityToWeapon(int index, AbilityAsset abilityAsset, out AbilityAsset replacedAbilityAsset)
    {
        return currentWeapon.AddOrReplaceAtIndex(index, abilityAsset, out replacedAbilityAsset);
    }

    public bool RemoveAbilityFromWeapon(int index, AbilityAsset emptyHolder, out AbilityAsset removedAbilityAsset)
    {
        if(currentWeapon.RemoveAbilityAtIndex(index, out removedAbilityAsset))
        {
            currentWeapon.weaponAbilities[index] = emptyHolder;
            return true;
        }
        return false;
    }

    public void SaveWeaponAbilityData(ref List<WeaponAbilitySaveData> data)
    {
        if(data == null)
            data = new();
        data.Clear();
        
        foreach(var weapon in weapons.Values)
        {
            data.Add(weapon.GetSaveData());
        }
    }

    void OnEnable()
    {
        SelectedAbility = null;
    }
}
