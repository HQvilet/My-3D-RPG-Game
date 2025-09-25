using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterRootMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    CharacterController com;
    void OnAnimatorMove()
    {
        
    }
}