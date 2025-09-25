using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public interface IAbilities
{
    void OnEquipped(EntityComponent entity);
    void OnEquippedStay(EntityComponent entity);
    void OnUnequipped(EntityComponent entity);
}

public class CharacterAbilitiesHandler : MonoBehaviour
{
    HashSet<IAbilities> utils = new();
    EntityComponent owner;

    public void AddUtils(IAbilities utilities)
    {
        utils.Add(utilities);
        utilities.OnEquipped(owner);
    }

    public void RemoveUtils(IAbilities utilities)
    {
        utils.Remove(utilities);
        utilities.OnUnequipped(owner);
    }

    public void ClearUtils()
    {
        foreach (IAbilities util in utils)
            util.OnUnequipped(owner);
        utils.Clear();
    }

    void Update()
    {
        foreach (IAbilities util in utils)
            util.OnEquippedStay(owner);
    }
}
