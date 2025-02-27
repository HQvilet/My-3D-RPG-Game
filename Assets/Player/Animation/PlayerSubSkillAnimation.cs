using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkillAnimation : MonoBehaviour
{
    public Player player;
    [SerializeField] private BaseWeaponUtilities CurrentWeaponUtilities;

    public void SetUpSkillUtils(BaseWeaponUtilities weaponUtilities)
    {
        CurrentWeaponUtilities = weaponUtilities;
    }

    // public void ActionEvent_1() => CurrentWeaponUtilities?.ActionEvent_1();
    // public void ActionEvent_2() => CurrentWeaponUtilities?.ActionEvent_2();
    // public void ActionEvent_3() => CurrentWeaponUtilities?.ActionEvent_3();
    // public void ActionEvent_4() => CurrentWeaponUtilities?.ActionEvent_4();
    // public void ActionEvent_5() => CurrentWeaponUtilities?.ActionEvent_5();
    // public void ActionEvent_6() => CurrentWeaponUtilities?.ActionEvent_6();
    // public void ActionEvent_7() => CurrentWeaponUtilities?.ActionEvent_7();
    // public void ActionEvent_8() => CurrentWeaponUtilities?.ActionEvent_8();

    public void TriggerAnimationEvent(string eventName)
    {
        CurrentWeaponUtilities.AnimationEvent?.Invoke(eventName);
    }
    
}
