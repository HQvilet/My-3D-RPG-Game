using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelConfig : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private Transform rightHandWeapon;
    [SerializeField] private Transform leftHandWeapon;
    [SerializeField] private Transform shieldWeapon;

    [Header("Skills")]
    public Transform rootVFX;
    [SerializeField] private Transform colliderPool;

    public void SetLeftHandedWeapon(Transform weaponModel ,Vector3 offset = default(Vector3))
    {
        weaponModel.SetParent(leftHandWeapon);
        // weaponModel.localPosition = offset;
        weaponModel.localPosition = Vector3.zero;
        weaponModel.localEulerAngles = Vector3.zero;
    }

    public void SetRightHandedWeapon(Transform weaponModel ,Vector3 offset = default(Vector3))
    {
        weaponModel.SetParent(rightHandWeapon);
        // weaponModel.localPosition = offset;
        weaponModel.localPosition = Vector3.zero;
        weaponModel.localEulerAngles = Vector3.zero;
    }

    public void SetShield(Transform weaponModel)
    {
        weaponModel.SetParent(shieldWeapon);
        // weaponModel.localPosition = offset;
        weaponModel.localPosition = Vector3.zero;
        weaponModel.localEulerAngles = Vector3.zero;
    }

    public void AddHitBoxCollider(Transform collider) // upper collider pool
    {
        collider.transform.SetParent(colliderPool);
        collider.localPosition = Vector3.zero;
        collider.localEulerAngles = Vector3.zero;
    }

}
