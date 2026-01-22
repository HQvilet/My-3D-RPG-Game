using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EditorAttributes;
using GameSaveLoadSystem;
using ItemSystem.ItemConfiguration;
using UnityEngine;

[Serializable]
public struct MarkupTime
{
    public float timeStamp;
    public string methodName;
}

public abstract class BaseWeapon : MonoBehaviour
{
    [ReadOnly] [SerializeField] public EntityComponent authenticatedOwner;
    public virtual void SetAuthenticatedOwner(EntityComponent entityComponent) => authenticatedOwner = entityComponent;

    [ReadOnly] public WeaponRef weaponRefData;
    [SerializeField] public List<AbilityAsset> weaponAbilities = new();
    protected int selectedAbilityIndex = -1;
    protected List<RealTimeCooldownAbility> cooldownAbilities = new();

    public virtual void LoadData()
    {

        List<WeaponAbilitySaveData> data = GameDataManager.GetLoadedData().weaponAbilities;
        
        if(data == null)
            data = new();
        
        WeaponAbilitySaveData s_data = data.Where(item => item.weaponID == weaponRefData.Id).FirstOrDefault();
        if(s_data == null)
        {
            s_data = new();
            s_data.abilityIDs = new();
        }
        Debug.Log($"Load {gameObject.name} data with {s_data.abilityIDs.Count} abilities.");
        for(int i = 0; i < Math.Min(weaponAbilities.Count, s_data.abilityIDs.Count); ++i)
        {
            LoadDirectlyIntoSlot(i, ItemIdentifyManager.Instance.GetItemByID(s_data.abilityIDs[i]) as WeaponAbilityItem);
        }
    }

    public WeaponAbilitySaveData GetSaveData()
    {
        WeaponAbilitySaveData data = new WeaponAbilitySaveData();
        data.weaponID = weaponRefData.Id;

        data.abilityIDs = weaponAbilities.Select(e =>
        {
            if(e == null)
                return "";
            if(e.IsEmpty())
                return "";
            return e.ItemData.ID;
        }).ToList();
        return data;
    }

    void LoadDirectlyIntoSlot(int index, WeaponAbilityItem abilityItem)
    {
        if(weaponAbilities[index] == null)
            weaponAbilities[index] = new AbilityAsset();

        if(abilityItem == null)
            return;
        
        if(abilityItem.ability.OnEnableAbility(this))
        {
            weaponAbilities[index].ItemData = abilityItem;
            GameUIManager.Instance.weaponAbilityUIHandler.ShowWeaponAbilities();
            
            if(abilityItem.ability is CoolDownWeaponAbility coolDownWeaponAbility)
            {
                RealTimeCooldownAbility cooldownAbility = new();
                cooldownAbility.timer = new CountdownTimer(coolDownWeaponAbility.coolDownTime);
                cooldownAbility.timer.Start();
                cooldownAbility.abilityItem = abilityItem;
                cooldownAbilities.Add(cooldownAbility);
            }
        }
    }

    public virtual bool AddOrReplaceAtIndex(int index, AbilityAsset abilityAsset, out AbilityAsset replacedAbilityAsset)// add or remove
    {
        RemoveAbilityAtIndex(index, out replacedAbilityAsset);

        if(abilityAsset == null || abilityAsset.IsEmpty())
            return false;
            
        if(abilityAsset.ItemData.ability.OnEnableAbility(this))
        {
            weaponAbilities[index] = abilityAsset;
            GameUIManager.Instance.weaponAbilityUIHandler.ShowWeaponAbilities();
            
            if(abilityAsset.ItemData.ability is CoolDownWeaponAbility coolDownWeaponAbility)
            {
                RealTimeCooldownAbility cooldownAbility = new();
                cooldownAbility.timer = new CountdownTimer(coolDownWeaponAbility.coolDownTime);
                cooldownAbility.timer.Start();
                cooldownAbility.abilityItem = abilityAsset.ItemData;
                cooldownAbilities.Add(cooldownAbility);
            }

            return true;
        }
        return false;
    }

    public virtual bool RemoveAbilityAtIndex(int index, out AbilityAsset removedAbilityAsset)
    {
        removedAbilityAsset = weaponAbilities[index];
        if(index >= weaponAbilities.Count || index < 0)
            return false;

        if(weaponAbilities[index] == null || weaponAbilities[index].IsEmpty())
            return false;

        if(weaponAbilities[index].ItemData.ability.OnDisableAbility(this))
        {
            if(weaponAbilities[index].ItemData.ability is CoolDownWeaponAbility coolDownWeaponAbility)
            {
                cooldownAbilities.RemoveAll(s => s.abilityItem.ability == coolDownWeaponAbility);
                // GameUIManager.Instance.playerSkillUIHandler.SetCoolDownAbilityData(cooldownAbilities);
            }
            
            removedAbilityAsset = weaponAbilities[index];

            weaponAbilities[index] = null;
            GameUIManager.Instance.weaponAbilityUIHandler.ShowWeaponAbilities();

            return true;
        }
        return false;
    }

    protected RealTimeCooldownAbility GetRealTimeCooldownAbilityAtIndex(int index)
    {
        if(index >= cooldownAbilities.Count || index < 0)
            return null;

        return cooldownAbilities[index];
    }
    
    protected virtual void OnAbilitySelected(int index)
    {
        
    }

    protected bool TryExecuteAbilityAtIndex(int index)
    {
        RealTimeCooldownAbility rtAbility = GetRealTimeCooldownAbilityAtIndex(index);
        if(rtAbility == null)
            return false;
            
        if(!rtAbility.timer.IsRunning)
        {
            rtAbility.abilityItem.ability.Execute(this);
            rtAbility.timer.Reset();
            rtAbility.timer.Start();  
            return true;          
        }
        return false;
    }

    protected bool AllowProcess()
    {
        return authenticatedOwner != null && gameObject.activeSelf;
    }

    public virtual void RegistryForInput(InputDataHandler inputHandler)
    {
        inputHandler.playerInputAction.AbilitySwap.performed += (ctx) =>
        {
            if(!AllowProcess())
                return;

            if(cooldownAbilities.Count <= 0)
                return;

            selectedAbilityIndex = (selectedAbilityIndex + 1) % cooldownAbilities.Count;
            OnAbilitySelected(selectedAbilityIndex);
            GameUIManager.Instance.playerSkillUIHandler.SetSelectedAbilityAtIndex(selectedAbilityIndex);
            
        };
    }

    public void InstantiateWithDamageChecking(GameObject g_object, Transform position, Quaternion rotation, Transform parent)
    {
        // Instantiate(g_object);
    }

    public virtual void GetDestroyed()
    {
        authenticatedOwner = null;
    }

    public virtual void GetInitialized()
    {
        for(int i = 0; i < (weaponRefData?.abilityCount ?? 4); ++i)
        {
            weaponAbilities.Add(new AbilityAsset());
        }

    }

    public virtual void OnDeselected()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSelected()
    {
        gameObject.SetActive(true);
    }

    protected virtual void UpdateAbilityDataToUI()
    {
        GameUIManager.Instance.playerSkillUIHandler.SetCoolDownAbilityData(cooldownAbilities);
        GameUIManager.Instance.playerSkillUIHandler.SetSelectedAbilityAtIndex(selectedAbilityIndex);
    }

    public virtual void UnscaledUpdate(float tick)
    {
        if(cooldownAbilities == null)
            return;
        cooldownAbilities.ForEach(cooldownAbility =>
        {
            cooldownAbility.timer.Tick(tick);
        });
    }


    public virtual void WeaponRiggingSetup(WeaponModelConfig modelConfig) { }

    public virtual void WeaponServiceSetup(WeaponServiceLocator weaponService) { }

    protected virtual void AnimationEventBinding(string eventName){}

}
