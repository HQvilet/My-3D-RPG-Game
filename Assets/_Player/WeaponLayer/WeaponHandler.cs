using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponModelConfig modelHandler;
    [SerializeField] private PlayerSubSkillAnimation skillAnimationEvent;
    [SerializeField] private WeaponServiceLocator weaponService;

    Dictionary<int ,BaseWeapon> WeaponSlot = new Dictionary<int, BaseWeapon>()
    {{1 ,null} ,
     {2 ,null} ,
     {3 ,null}};


    private BaseWeapon CurrentWeapon;
    private int CurrentIndexSlot;

    void Start()
    {
        InputDataHandler.Instance.PlayerUIInteraction.WeaponIndexSlot.performed += ChangeWeapon;
        LoadWeapon();
        
    }

    void LoadWeapon()
    {
        InitializeWeapon(1 ,1);
        InitializeWeapon(2 ,2);
        InitializeWeapon(3 ,3);
    }


    void InitializeWeapon(int slot ,int ID)
    {
        // Init Weapon
        WeaponSlot[slot]?.GetDestroyed();
        WeaponSlot[slot] = null;
        WeaponRef weaponRef = WeaponIdManager.Instance.GetWeaponFromId(ID);
        if(weaponRef == null) return;

        var weapon = Instantiate(weaponRef.WeaponPref).GetComponent<BaseWeapon>();
        // Set up model
        weapon.WeaponRiggingSetup(modelHandler);
        weapon.WeaponServiceSetup(weaponService);
        // Set active false
        weapon.gameObject.SetActive(false);

        WeaponSlot[slot] = weapon;

        if(CurrentIndexSlot == slot)
            weapon.OnSelected();
    }

    //listen to Player input
    void ChangeWeapon(InputAction.CallbackContext context)
    {
        
        if(int.TryParse(context.control.name ,out int res))
            SelectWeaponOnIndex(res);
        else
            Debug.Log("Invalid slot input " + context.control.name);
    }

    public void SelectWeaponOnIndex(int res)
    {
        
            
        if(CurrentIndexSlot == res) return;

        CurrentWeapon?.OnDeselected();
        
        if(WeaponSlot.ContainsKey(res))
        {
            CurrentIndexSlot = res;
            CurrentWeapon = WeaponSlot[CurrentIndexSlot];
            if(CurrentWeapon)
                skillAnimationEvent.SetUpSkillUtils(CurrentWeapon.GetComponent<BaseWeaponUtilities>());
            CurrentWeapon?.OnSelected();
        }
        else
            Debug.Log("Inventory does not contain this slot");
    }

} 
