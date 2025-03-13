using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    MELEE_WEAPON,
    BOW,
    SHIELD,
    WAND
}

[CreateAssetMenu(menuName = "Weapon/WeaponConfiguration")]
public class WeaponRef : ScriptableObject
{
    public int Id;
    public string Name;
    public GameObject WeaponPref;
    public WeaponType Type;
}
