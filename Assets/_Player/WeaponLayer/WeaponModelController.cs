using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModelConfig : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private Transform rightHandWeapon;
    [SerializeField] private Transform leftHandWeapon;
    [SerializeField] private Transform shieldWeapon;
    [SerializeField] private Transform weaponPool;

    [Header("Skills")]
    public Transform rootVFX;
    [SerializeField] private Transform colliderPool;

    public void AddToPool(Transform weapon)
    {
        weapon.SetParent(weaponPool);
        weapon.localPosition = Vector3.zero;
        weapon.localEulerAngles = Vector3.zero;
    }

    public void SetLeftHandedWeapon(Transform weaponModel, bool useLocalTransform = true)
    {
        weaponModel.GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation);
        weaponModel.SetParent(leftHandWeapon);
        if(useLocalTransform)
        {
            weaponModel.localPosition = localPosition;
            weaponModel.localRotation = localRotation;            
        }
        else
        {
            
        }
        
    }

    public void SetRightHandedWeapon(Transform weaponModel, bool useLocalTransform = true)
    {
        weaponModel.GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation);
        weaponModel.SetParent(rightHandWeapon);
        if(useLocalTransform)
        {
            weaponModel.localPosition = localPosition;
            weaponModel.localRotation = localRotation;            
        }
        else
        {
            weaponModel.localPosition = Vector3.zero;
            weaponModel.localRotation = Quaternion.identity;            
        }
    }

    public void SetShield(Transform weaponModel)
    {
        weaponModel.GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation);
        weaponModel.SetParent(shieldWeapon);
        weaponModel.localPosition = localPosition;
        weaponModel.localRotation = localRotation;
    }

    public void AddHitBoxCollider(Transform collider) // upper collider pool
    {
        collider.transform.SetParent(colliderPool);
        collider.localPosition = Vector3.zero;
        collider.localEulerAngles = Vector3.zero;
    }

}
