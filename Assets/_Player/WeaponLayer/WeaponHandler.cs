using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponModelConfig modelHandler;
    [SerializeField] private WeaponServiceLocator weaponService;
    
    [SerializeField] private EntityComponent ownerEntity;

    Dictionary<int, BaseWeapon> WeaponSlot = new Dictionary<int, BaseWeapon>()
    {
        {1 ,null},
        {2 ,null},
        {3 ,null},
        {4, null}
    };


    private BaseWeapon _currentWeapon;
    private int _currentIndexSlot = 0;

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
        weapon.SetAuthenticatedOwner(ownerEntity);
        weapon.WeaponRiggingSetup(modelHandler);
        weapon.WeaponServiceSetup(weaponService);

        // Set active false
        weapon.gameObject.SetActive(false);

        WeaponSlot[slot] = weapon;

        if(_currentIndexSlot == slot)
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
        if(_currentIndexSlot == res) return;

        _currentWeapon?.OnDeselected();
        
        if(WeaponSlot.ContainsKey(res))
        {
            _currentIndexSlot = res;
            _currentWeapon = WeaponSlot[_currentIndexSlot];
            if (_currentWeapon)
            {
                // _currentWeapon.SetAuthenticatedOwner(ownerEntity);
                // skillAnimationEvent.SetUpSkillUtils(_currentWeapon.GetComponent<BaseWeaponUtilities>());
            }
                
            _currentWeapon?.OnSelected();
        }
        else
            Debug.Log("Inventory does not contain this slot");
    }

} 
