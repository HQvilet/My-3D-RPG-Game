using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeUtilities : BaseWeaponUtilities
{
    public Collider WeaponHitBox;

    public override void ActionEvent_1()
    {
        StartCoroutine(ToggleHitBox());
    }

    IEnumerator ToggleHitBox()
    {
        WeaponHitBox.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        WeaponHitBox.gameObject.SetActive(false);
    }
}
