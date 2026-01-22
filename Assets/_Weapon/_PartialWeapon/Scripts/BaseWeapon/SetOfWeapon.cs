using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//abstract function for animation event override
public class SetOfWeapon : BaseWeapon
{
    [SerializeField] List<WeaponRef> weaponRefs;
    [SerializeField] List<BaseWeapon> weaponSet;

    public override void GetInitialized()
    {
        // Debug.Log("Init Set");
        weaponSet.AddRange(weaponRefs.Select(weaponRef =>
        {
            var a = Instantiate(weaponRef.WeaponPref).GetComponent<BaseWeapon>();
            a.weaponRefData = weaponRef;
            a.GetInitialized();
            return a;
        }));
    }

    // public override void LoadData(int id, List<string> abilityIDs)
    // {
    //     weaponSet.ForEach(weapon =>
    //     {
            
    //     });
    // }

    public override void RegistryForInput(InputDataHandler inputHandler)
    {
        weaponSet.ForEach(weapon => weapon.RegistryForInput(inputHandler));
    }

    public override void SetAuthenticatedOwner(EntityComponent entityComponent)
    {
        base.SetAuthenticatedOwner(entityComponent);
        weaponSet.ForEach(weapon => weapon.SetAuthenticatedOwner(entityComponent));
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();
        weaponSet.ForEach(weapon => weapon.GetDestroyed());
    }

    public override void OnSelected()
    {
        base.OnSelected();
        weaponSet.ForEach(weapon => weapon.OnSelected());
    }

    public override void UnscaledUpdate(float tick)
    {
        base.UnscaledUpdate(tick);
        weaponSet.ForEach(weapon => weapon.UnscaledUpdate(tick));
    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        weaponSet.ForEach(weapon => weapon.OnDeselected());
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        base.WeaponRiggingSetup(config);
        config.AddToPool(this.transform);
        weaponSet.ForEach(weapon => weapon.WeaponRiggingSetup(config));
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        base.WeaponServiceSetup(weaponService);
        // weaponService.Set
        weaponSet.ForEach(weapon => weapon.WeaponServiceSetup(weaponService));
    }
}
