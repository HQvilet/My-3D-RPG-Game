using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using ItemSystem.ItemConfiguration;
using UnityEngine;


// //passing data from .asset file to armour references class
// [CreateAssetMenu(menuName = "ArmourAsset/Armour Reference Data")]
// public class ArmourAssetFile : ScriptableObject //where Utils : ArmourUtils where ConfigType : ArmourConfigAsset
// {
//     public ArmourItem item;
//     public ArmourUtils utils;
//     public BasicStatsConfig basicStats;
//     // public ArmourAssetFile(ArmourAssetFile other)
//     // {
//     //     item = other.item;
//     //     utils = other.utils;
//     //     basicStats = other.basicStats;
//     // }
// }

public enum ArmourAssetType
{
    //specific name config
    TEST_ARMOUR,
    TEST_ARMOUR_2,
}

public class ArmourReference : ScriptableObject ,IArmourRef
{
    public ArmourAssetType assetType;

    public virtual ArmourUtils GetArmourUtils() => null;
    public virtual ArmourItem GetItemData() => null;
    
}

public interface IArmourRef
{
    public ArmourItem GetItemData();
    public ArmourUtils GetArmourUtils();
}

public interface IArmourUtilsAction
{
    void OnEquipped(EntityComponent entity);
    void OnTriggerAbility(EntityComponent entity);
    void OnEquippedStay(EntityComponent entity);
    void OnUnequipped(EntityComponent entity);
}


public interface ArmourConfigAsset{}

