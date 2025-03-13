using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour, OnWeaponInitializedCallbacks, OnWeaponSelectedCallbacks
{
    public virtual void GetDestroyed(){}

    public virtual void GetInitialized(){}

    public virtual void OnDeselected(){}

    public virtual void OnSelected(){}

    public virtual void OnSelecting(){}

    public virtual void WeaponSideRiggingSetup(WeaponModelConfig config){}

    public virtual void WeaponServiceSetup(WeaponServiceLocator weaponService){}


}
