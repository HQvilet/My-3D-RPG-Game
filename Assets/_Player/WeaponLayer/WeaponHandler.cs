using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSaveLoadSystem;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHandler : MonoBehaviour
{
    public InputAction switchWeaponNumericKeyAction;
    [SerializeField] private WeaponModelConfig modelHandler;
    [SerializeField] private WeaponServiceLocator weaponService;
    public bool allowToChange = true;

    [SerializeField] private EntityComponent weaponHolder;
    [SerializeField] private InputDataHandler inputHandler;

    Dictionary<int, BaseWeapon> weaponSlots = new Dictionary<int, BaseWeapon>()
    {
        {0 ,null},
        {1 ,null},
        {2 ,null},
        {3, null}
    };


    private BaseWeapon _currentWeapon;
    private int _currentIndexSlot = -1;

    void Start()
    {
        switchWeaponNumericKeyAction.Enable();
        switchWeaponNumericKeyAction.performed += (context) =>
        {
            if(GameUIManager.Instance.isPausing)
                return;
            if(int.TryParse(context.control.displayName, out int numericKeyPressed))
            {
                SelectWeaponOnIndex(numericKeyPressed - 1);
            }
        };
        // inputHandler.PlayerUIInteraction.WeaponIndexSlot.performed += ChangeWeapon;
        // inputHandler.playerInputAction.WeaponSwap.performed += (ctx) =>
        // {
        //     if(GameUIManager.Instance.isPausing)
        //         return;

        //     if(!weaponHolder.stateHandler.AllowToInterupt)
        //         return;

        //     SelectWeaponOnIndex((_currentIndexSlot + 1) % 3);
            
        // };
        LoadWeapon();
        SelectWeaponOnIndex(0);
        
    }

    void LoadWeapon()
    {
        int[] weaponsRange = new int[3]{4,3,5};
        for(int i = 0; i < 3; ++i)
        {
            LoadWeaponInterface(i, weaponsRange[i]);
        }
    }

    void LoadWeaponInterface(int slot ,int ID)
    {

        weaponSlots[slot]?.GetDestroyed();
        weaponSlots[slot] = null;
        WeaponRef weaponRef = ItemIdentifyManager.Instance.GetWeaponFromId(ID);
        if(weaponRef == null) return;

        BaseWeapon weapon = Instantiate(weaponRef.WeaponPref).GetComponent<BaseWeapon>();
        
        weapon.weaponRefData = weaponRef;
        weapon.GetInitialized();
        weapon.SetAuthenticatedOwner(weaponHolder);
        weapon.WeaponRiggingSetup(modelHandler);
        weapon.WeaponServiceSetup(weaponService);
        weapon.RegistryForInput(inputHandler);

        GameUIManager.Instance.weaponSelectorUIHandler.AddToSelector(weaponRef);

        // Set active false
        weapon.gameObject.SetActive(false);

        weaponSlots[slot] = weapon;

    }

    void Update()
    {
        if(weaponSlots != null)
        foreach(BaseWeapon weapon in weaponSlots.Values)
            weapon?.UnscaledUpdate(Time.deltaTime);
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
        
        if(weaponSlots.ContainsKey(res))
        {
            _currentIndexSlot = res;
            _currentWeapon = weaponSlots[_currentIndexSlot];
            _currentWeapon?.OnSelected();

            GameUIManager.Instance.weaponSelectorUIHandler.SetSelectedIndex(res + 1);
        }
        else
            Debug.Log("Inventory does not contain this slot");
    }

    void OnDestroy()
    {
        switchWeaponNumericKeyAction.Disable();
    }
} 
