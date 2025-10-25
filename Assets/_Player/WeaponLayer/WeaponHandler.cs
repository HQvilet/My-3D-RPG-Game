using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponModelConfig modelHandler;
    [SerializeField] private WeaponServiceLocator weaponService;
    public bool allowToChange = true;

    [SerializeField] private EntityComponent weaponHolder;
    [SerializeField] private InputDataHandler inputHandler;

    Dictionary<int, BaseWeapon> weaponSlot = new Dictionary<int, BaseWeapon>()
    {
        {1 ,null},
        {2 ,null},
        {3 ,null},
        {4, null}
    };


    private BaseWeapon _currentWeapon;
    private int _currentIndexSlot = 1;

    void Start()
    {
        inputHandler.PlayerUIInteraction.WeaponIndexSlot.performed += ChangeWeapon;
        LoadWeapon();
        
    }

    void LoadWeapon()
    {
        InitializeWeapon(1, 4);
        InitializeWeapon(2, 3);
    }


    void InitializeWeapon(int slot ,int ID)
    {
        // Init Weapon
        weaponSlot[slot]?.GetDestroyed();
        weaponSlot[slot] = null;
        WeaponRef weaponRef = WeaponIdManager.Instance.GetWeaponFromId(ID);
        if(weaponRef == null) return;

        BaseWeapon weapon = Instantiate(weaponRef.WeaponPref).GetComponent<BaseWeapon>();
        weapon.GetInitialized();

        weapon.SetAuthenticatedOwner(weaponHolder);
        weapon.WeaponRiggingSetup(modelHandler);
        weapon.WeaponServiceSetup(weaponService);
        weapon.RegistryForInput(inputHandler);

        // Set active false
        weapon.gameObject.SetActive(false);

        weaponSlot[slot] = weapon;

        if (_currentIndexSlot == slot)
        {
            _currentWeapon = weapon;
            weapon.OnSelected();
        }
            
    }

    //listen to Player input
    void ChangeWeapon(InputAction.CallbackContext context)
    {
        if (!allowToChange)
            return;
        if(int.TryParse(context.control.name ,out int res))
            SelectWeaponOnIndex(res);
        else
            Debug.Log("Invalid slot input " + context.control.name);
    }

    public void SelectWeaponOnIndex(int res)
    {
        if(_currentIndexSlot == res) return;

        _currentWeapon?.OnDeselected();
        
        if(weaponSlot.ContainsKey(res))
        {
            _currentIndexSlot = res;
            _currentWeapon = weaponSlot[_currentIndexSlot];
            _currentWeapon?.OnSelected();
        }
        else
            Debug.Log("Inventory does not contain this slot");
    }

} 
