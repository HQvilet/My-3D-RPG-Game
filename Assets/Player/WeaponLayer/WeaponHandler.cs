using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponModelController modelHandler;
    [SerializeField] private InputDataHandler Input;
    [SerializeField] private PlayerSubSkillAnimation skillAnimationEvent;

    Dictionary<int ,BaseWeapon> WeaponSlot = new Dictionary<int, BaseWeapon>()
    {{1 ,null} ,
     {2 ,null} ,
     {3 ,null}};


    private BaseWeapon CurrentWeapon;
    private int CurrentIndexSlot;

    void Start()
    {
        Input.PlayerUIInteraction.WeaponIndexSlot.performed += ChangeWeapon;

        InitializeWeapon(1 ,1);
    }

    void InitializeWeapon(int slot ,int ID)
    {
        
        // Init Weapon
        WeaponSlot[slot]?.GetDestroyed();
        WeaponSlot[slot] = null;
        WeaponRef weaponRef = WeaponIdManager.Instance.GetWeaponFromId(ID);
        if(weaponRef == null) return;

        var weapon = Instantiate(weaponRef.WeaponPref);
        // Set up model
        modelHandler.SetRightHandedWeapon(weapon.transform);
        // Set active false
        weapon.SetActive(false);
        // Set weapon utilities
        skillAnimationEvent.SetUpSkillUtils(weapon.GetComponent<BaseWeaponUtilities>());
        WeaponSlot[slot] = weapon.GetComponent<BaseWeapon>();
    }

    //listen to Player input
    void ChangeWeapon(InputAction.CallbackContext context)
    {
        CurrentWeapon?.OnDeselected();
        if(int.TryParse(context.control.name ,out int res))
        {
            if(CurrentIndexSlot == res) return;

            if(WeaponSlot.ContainsKey(res))
            {
                CurrentIndexSlot = res;
                CurrentWeapon = WeaponSlot[CurrentIndexSlot];
                CurrentWeapon?.OnSelected();
            }
            else
                Debug.Log("Inventory does not contain this slot");
        }
        else
            Debug.Log("Invalid slot input " + context.control.name);
    }
} 
