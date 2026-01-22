using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySlot : MonoBehaviour
{
    [SerializeField] Image abilitySprite;
    [SerializeField] Image coolDownSprite;

    public void SetAbilityData(WeaponAbilityItem weaponAbilityItem)
    {
        abilitySprite.sprite = weaponAbilityItem.Sprite;
    }

    public void SetCooldownProgress(float t)
    {
        coolDownSprite.fillAmount = Mathf.Clamp01(t);
    }


}
