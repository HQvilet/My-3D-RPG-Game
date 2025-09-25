using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;


public abstract class ArmourUtils : IArmourUtilsAction 
{
    public virtual void OnEquipped(EntityComponent entity){}

    public virtual void OnEquippedStay(EntityComponent entity){}

    public virtual void OnTriggerAbility(EntityComponent entity){}

    public virtual void OnUnequipped(EntityComponent entity){}
}

