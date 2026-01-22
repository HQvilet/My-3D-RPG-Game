using UnityEngine;
using MEC;
using System.Collections.Generic;

public class SlashHitBox : DamageUnitWithEvent
{

    public void DoFlashHitbox() => Timing.RunCoroutine(TriggerDamageCollider());

    IEnumerator<float> TriggerDamageCollider()
    {
        gameObject.SetActive(true);
        yield return Timing.WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

}