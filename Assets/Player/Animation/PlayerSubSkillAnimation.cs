using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubSkillAnimation : MonoBehaviour
{
    public Player player;
    [SerializeField] private GameObject WeaponHitBox;

    public void TriggerHitBox(int test)
    {
        StartCoroutine(ToggleHitBox());
    }

    IEnumerator ToggleHitBox()
    {
        WeaponHitBox.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        WeaponHitBox.SetActive(false);
    }

}
