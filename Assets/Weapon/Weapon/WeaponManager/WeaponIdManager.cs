using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeaponId
{
    public int Id;
}

public class WeaponIdManager : Singleton<WeaponIdManager>
{
    [SerializeField] private List<WeaponRef> weaponRefs = new List<WeaponRef>();
    private Dictionary<int ,WeaponRef> WeaponHolder = new Dictionary<int, WeaponRef>();

    protected override void Awake()
    {
        base.Awake();
        foreach(var weapon_info in weaponRefs)
        {
            if(!WeaponHolder.ContainsKey(weapon_info.Id))
                WeaponHolder.Add(weapon_info.Id ,weapon_info);
        }
    }

    void LoadAll()
    {
        WeaponRef[] WeaponData = Resources.LoadAll<WeaponRef>("sad");
        foreach(WeaponRef weaponRef in WeaponData)
        {
            if(!WeaponHolder.ContainsKey(weaponRef.Id))
                WeaponHolder.Add(weaponRef.Id ,weaponRef);
            else
            {
                Debug.Log("Exist 2 weapon with the same ID :" + weaponRef.Id + " - " + weaponRef.Name);
            }
        }
    }

    public WeaponRef GetWeaponFromId(int _id)
    {
        if(WeaponHolder.ContainsKey(_id))
            return WeaponHolder[_id];

        Debug.Log("Non existing weapon ID reference");
        return null;
    }

    public bool TryGetWeaponFromId(int _id ,out WeaponRef weaponRef)
    {
        return WeaponHolder.TryGetValue(_id ,out weaponRef);
    }
}
