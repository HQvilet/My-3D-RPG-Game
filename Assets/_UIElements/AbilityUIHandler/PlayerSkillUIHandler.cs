using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class PlayerSkillUIHandler : MonoBehaviour
{
    [SerializeField] Transform abilityContainer;

    public List<AbilitySlot> coolDownAbilitySlots;
    public List<RealTimeCooldownAbility> cooldownAbilities;

    [SerializeField] Transform selector;

    void Awake()
    {
        coolDownAbilitySlots = abilityContainer.GetComponentsInChildren<AbilitySlot>(true).ToList();
        GameUIManager.Instance.OnMainGameUIDisable += () =>
        {
            UpdateAbilitiesUI();
        };
    }

    public void SetCoolDownAbilityData(List<RealTimeCooldownAbility> cooldowns)
    {
        // if(cooldowns == cooldownAbilities)
        //     return;
        cooldownAbilities = cooldowns;
        if(cooldownAbilities != null)
            UpdateAbilitiesUI();
    }

    public void SetSelectedAbilityAtIndex(int index)
    {
        coolDownAbilitySlots.ForEach(s => s.transform.localScale = Vector3.one);
        if(index < 0 || index >= cooldownAbilities.Count)
            return;
        coolDownAbilitySlots[index].transform.localScale = Vector3.one * 1.1f;
    }

    public void ClearAbilitySlots()
    {
        cooldownAbilities = null;
        UpdateAbilitiesUI();
    }

    public void UpdateAbilitiesUI()
    {
        coolDownAbilitySlots.ForEach(s => s.gameObject.SetActive(false));

        if(cooldownAbilities == null)
            return;
            
        for(int i = 0; i < cooldownAbilities.Count; ++i)
        {
            coolDownAbilitySlots[i].gameObject.SetActive(true);
            coolDownAbilitySlots[i].SetAbilityData(cooldownAbilities[i].abilityItem);
        }
    }


    public void UpdateAbilitySlot()
    {
        if(cooldownAbilities == null)
            return;

        for(int i = 0; i < cooldownAbilities.Count; ++i)
        {
            coolDownAbilitySlots[i].SetCooldownProgress(cooldownAbilities[i].timer.Progress);
        }
    }

    void Update()
    {
        
        UpdateAbilitiesUI();
        UpdateAbilitySlot();
    }
}
