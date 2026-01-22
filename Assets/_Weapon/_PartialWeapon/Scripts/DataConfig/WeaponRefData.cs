using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using UnityEngine;

public enum WeaponType
{
    MELEE_WEAPON,
    BOW,
    SHIELD,
    WAND,
    NONE
}

[CreateAssetMenu(menuName = "Weapon/WeaponConfiguration")]
public class WeaponRef : ScriptableObject
{
    public int Id;
    public string Name;
    [AssetPreview(100,100)] public Sprite weaponSprite;
    public GameObject WeaponPref;
    public WeaponType Type;
    public int abilityCount;
    //Later can replace with item data
}
