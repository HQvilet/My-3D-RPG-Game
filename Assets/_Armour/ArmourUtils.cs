using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryAction
{
    void OnEquipped();
    void OnTriggerAbility();
    void OnEquippedStay();
    void OnUnequipped();
}

public class ArmourUtils : IInventoryAction
{
    public virtual void OnEquipped(){}

    public virtual void OnEquippedStay(){}

    public virtual void OnTriggerAbility(){}

    public virtual void OnUnequipped(){}
}

public class TestArmourUtils : ArmourUtils
{
    public override void OnEquipped()
    {
        base.OnEquipped();
        Debug.Log("Equipped");
    }
    public override void OnEquippedStay()
    {
        base.OnEquippedStay();
        Debug.Log("Is Equipping");
    }
}