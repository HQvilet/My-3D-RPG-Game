using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected EntityComponent authenticatedOwner;
    public virtual void SetAuthenticatedOwner(EntityComponent entityComponent) => authenticatedOwner = entityComponent;

    protected bool AllowProcess()
    {
        return authenticatedOwner != null;
    }

    protected bool AllowInputProcess()
    {
        // return !authenticatedOwner.TryGetEntityInput().Equals(default(PlayerInputAction.PlayerActions));
        return true;
    }

    public virtual void RegistryForInput(InputDataHandler inputHandler){}

    public virtual void GetDestroyed()
    {
        authenticatedOwner = null;
    }

    public virtual void GetInitialized() { }

    public virtual void OnDeselected()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnSelected()
    {
        gameObject.SetActive(true);
    }


    public virtual void WeaponRiggingSetup(WeaponModelConfig modelConfig) { }

    public virtual void WeaponServiceSetup(WeaponServiceLocator weaponService) { }

    protected virtual void AnimationEventBinding(string eventName){}

}
