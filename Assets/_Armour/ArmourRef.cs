using System.Collections;
using System.Collections.Generic;
using ItemSystem.ItemConfiguration;
using UnityEngine;


public class ArmourItemSet : ScriptableObject
{
    List<ArmourItem> items;
}

public class ArmourConfigAsset
{

}

public class ArmourRef
{
    ArmourItemSet setOfItems;
    ArmourUtils utils;
    ArmourConfigAsset config;

}
