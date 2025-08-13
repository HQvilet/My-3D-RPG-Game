using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using ItemSystem.ItemConfiguration;
using UnityEngine;


public interface IArmourRef
{
    public ArmourItem GetItemData();
    public ArmourUtils GetArmourUtils();
}

public enum ArmourAssetType
{
    //specific name config
    TEST_ARMOUR,
    TEST_ARMOUR_2,
}

public class ArmourReference : ScriptableObject ,IArmourRef
{
    public ArmourAssetType assetType;
    public string assetName;

    public virtual void Set(ArmourReference reference) { }

    public virtual ArmourUtils GetArmourUtils() => null;
    public virtual ArmourItem GetItemData() => null;
    
}

public interface IArmourUtilsAction
{
    void OnEquipped(EntityComponent entity);
    void OnTriggerAbility(EntityComponent entity);
    void OnEquippedStay(EntityComponent entity);
    void OnUnequipped(EntityComponent entity);
}


public interface ArmourConfigAsset{}

