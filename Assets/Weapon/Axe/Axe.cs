using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Axe : BaseWeapon
{

    private WeaponCombo weaponCombo;
    public InputDataHandler Input;
    void Start()
    {
        weaponCombo = GetComponent<WeaponCombo>();
        Input.PlayerInput.Attack.performed += Use;
    }

    void Use(InputAction.CallbackContext context)
    {
        weaponCombo.weaponStateMachine.TriggerNextCombo();
    }

    public override void OnSelected()
    {
        gameObject.SetActive(true);
    }

    public override void OnDeselected()
    {
        gameObject.SetActive(false);
    }
}
