using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Entity meta data
public class EntityComponent : MonoBehaviour
{
    // World interaction data
    public CharacterStats characterStats;
    public CharacterStateHandler stateHandler;
    public BaseDamageableObject damageableObject;
    public BaseEffectModifier effectModifier;

    // World physic interaction data
    [SerializeField] private InputDataHandler input;
    public PlayerInputAction.PlayerActions TryGetEntityInput() => input.PlayerInput;
    
    [SerializeField] private PlayerAnimationSystem _entityAnimatorModifier;
    public PlayerAnimationSystem GetModifiableAnimator() => _entityAnimatorModifier;

}
