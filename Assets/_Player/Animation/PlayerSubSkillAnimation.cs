using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkillAnimation : MonoBehaviour
{
    [SerializeField] private BaseWeaponUtilities CurrentWeaponUtilities;
    [SerializeField] private PlayerStateHandler stateHandler;

    public void SetUpSkillUtils(BaseWeaponUtilities weaponUtilities)
    {
        CurrentWeaponUtilities = weaponUtilities;
    }

    public void TriggerAnimationEvent(string eventName)
    {
        CurrentWeaponUtilities?.RelyActionOnEvent(eventName);
        stateHandler.OnAnimationEvent?.Invoke(eventName);
    }

}

public interface ICommonlyUseEvent
{
    void Slash();
}
