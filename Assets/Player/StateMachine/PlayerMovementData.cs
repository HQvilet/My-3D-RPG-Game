using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Config/Player Movement Data")]
public class PlayerMovementData : ScriptableObject
{
    public float gravity;
    public float gravityMultiplier = 1;

    #region Speed Modifier

    public float walkSpeed;
    public float runSpeed;

    #endregion

    #region Jump Force Impulse

    public float JumpImpulse;
    
    #endregion

}
