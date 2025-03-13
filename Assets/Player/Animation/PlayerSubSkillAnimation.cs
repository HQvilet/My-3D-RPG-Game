using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkillAnimation : MonoBehaviour
{
    [SerializeField] private BaseWeaponUtilities CurrentWeaponUtilities;

    public void SetUpSkillUtils(BaseWeaponUtilities weaponUtilities)
    {
        CurrentWeaponUtilities = weaponUtilities;
    }

    public void TriggerAnimationEvent(string eventName)
    {
        CurrentWeaponUtilities?.RelyActionOnEvent(eventName);
    }

}

public interface ICommonlyUseEvent
{
    void Slash();
}
