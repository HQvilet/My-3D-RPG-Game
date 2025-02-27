using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelController : MonoBehaviour
{
    [SerializeField] private Transform RightHandWeapon;
    [SerializeField] private Transform LeftHandWeapon;

    // [SerializeField] private 

    public void SetLeftHandedWeapon(Transform weaponModel ,Vector3 offset = default(Vector3))
    {
        weaponModel.SetParent(LeftHandWeapon);
        weaponModel.localPosition = offset;
        weaponModel.localPosition = Vector3.zero;
        weaponModel.localEulerAngles = Vector3.zero;
    }

    public void SetRightHandedWeapon(Transform weaponModel ,Vector3 offset = default(Vector3))
    {
        weaponModel.SetParent(RightHandWeapon);
        weaponModel.localPosition = offset;
        weaponModel.localPosition = Vector3.zero;
        weaponModel.localEulerAngles = Vector3.zero;
    }

    public void SetWeaponHandle()
    {
        //Set left or right arm weapon handle transform position.
    }

}
