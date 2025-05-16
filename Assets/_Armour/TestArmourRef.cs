using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;
using UnityEngine.UI;





[CreateAssetMenu(menuName = "ArmourAsset/Test Asset")]
public class TestArmourRef : ArmourReference //,IArmourRef
{
    public ArmourItem item;
    public TestArmourUtils utils;
    // public BasicStatsConfig basicStats;
    public TestArmourConfig config;

    public TestArmourRef Set(TestArmourRef assetFile)
    {
        item = assetFile.item;

        config = assetFile.config;
        utils = new TestArmourUtils();
        utils.SetConfigFile(config);
        
        // basicStats = assetFile.basicStats;

        return this;
    }

    public override ArmourUtils GetArmourUtils() => utils;
    public override ArmourItem GetItemData() => item;

}


public class TestArmourUtils : ArmourUtils
{
    private TestArmourConfig config;
    public void SetConfigFile(TestArmourConfig config) => this.config = config;

    public override void OnEquipped(EntityComponent entity)
    {
        entity.characterStats.mediator.AddStats(config.stats);
    }
    
    public override void OnEquippedStay(EntityComponent entity)
    {
        Debug.Log("Is Equipping");
    }

    public override void OnUnequipped(EntityComponent entity)
    {
        entity.characterStats.mediator.RemoveStats(config.stats);
    }
}

[System.Serializable]
public class TestArmourConfig : ArmourConfigAsset
{
    public bool skill_1;
    public bool skill_2;
    public int nig_damage;
    public BasicStatsConfig stats;
}