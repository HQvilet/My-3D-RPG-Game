using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SlashVFX
{
    public Transform slash_vfx;
    public Quaternion slashQuaternion;    
}

[CreateAssetMenu(menuName = "HitAndSlash/Data")]
public class HitAndSlashData : ScriptableObject
{
    public List<SlashVFX> SlashVFX;
    public Collider collider;
    
}
