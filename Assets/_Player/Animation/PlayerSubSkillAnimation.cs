using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkillAnimation : MonoBehaviour, ICommonlyUsedAnimationEvent
{

    [SerializeField] private CharacterStateHandler stateHandler;

    public void TriggerAnimationEvent(string eventName)
    {
        stateHandler.OnAnimationEvent?.Invoke(eventName);
    }

    public void LockMovement() => stateHandler.CanMove = false;

    public void UnlockMovement() => stateHandler.CanMove = true;

    public void AllowInterupt() => stateHandler.AllowToInterupt = true;
    public void DenyInterupt() => stateHandler.AllowToInterupt = false;

}

public interface ICommonlyUsedAnimationEvent
{
    public void LockMovement();
    public void UnlockMovement();
}
