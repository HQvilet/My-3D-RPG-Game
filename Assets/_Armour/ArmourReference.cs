using System;
using System.Collections;
using ItemSystem.ItemConfiguration;
using UnityEditor;
using UnityEngine;

public enum ArmourType
{
    HEAD_ARMOUR ,
    ARM_ARMOUR ,
    LEG_ARMOUR ,
    BODY_ARMOUR ,
    ARTIFACT ,
}

public interface IArmourRef
{
    public ArmourItem GetItemData();
    public ArmourUtils GetArmourUtils();
}

public class ArmourReference : ScriptableObject ,IArmourRef
{
    [ReadOnly] [SerializeField] private string _id;
    public string ID => _id;
    public void SetUniqueID(string id) => this._id = id;

    public virtual void Set(ArmourReference reference) { }

    public virtual ArmourUtils GetArmourUtils() => null;

    public virtual ArmourItem GetItemData() => null;
        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(_id))
            {
                _id = Guid.NewGuid().ToString();
                EditorUtility.SetDirty(this);
            }
#endif
        }
}

public interface IArmourUtilsAction
{
    void OnEquipped(EntityComponent entity);
    void OnTriggerAbility(EntityComponent entity);
    void OnEquippedStay(EntityComponent entity);
    void OnUnequipped(EntityComponent entity);
}


public interface ArmourConfigAsset{}

