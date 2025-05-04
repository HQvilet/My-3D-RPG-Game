using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, OnWeaponSelectedCallbacks
{
    public virtual void GetDestroyed(){}

    public virtual void GetInitialized(){}

    public virtual void OnDeselected()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSelected()
    {
        gameObject.SetActive(true);
    }


    public virtual void WeaponRiggingSetup(WeaponModelConfig modelConfig){}

    public virtual void WeaponServiceSetup(WeaponServiceLocator weaponService){}


}
