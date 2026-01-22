using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// Entity meta data
public class EntityComponent : MonoBehaviour
{
    public int groupType;
    // World interaction data
    public CharacterStats characterStats;
    public CharacterStateHandler stateHandler;
    public BaseDamageableObject damageableObject;
    public BaseEffectModifier effectModifier;

}
