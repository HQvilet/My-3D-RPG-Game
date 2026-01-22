using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MEC;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : BaseWeapon
{
    [Header("Input")]
    public InputAction holdAimAction;
    InputAction attackAction;

    Animator animator;
    [SerializeField] DamageUnit normalProjectile;
    [SerializeField] float reloadTime = 0.75f;

    public GameObject specialSkillThreshHoldVFX;
    
    public override void GetInitialized()
    {
        base.GetInitialized();
        LoadData();
    }

    public override void OnSelected()
    {
        base.OnSelected();
        UpdateAbilityDataToUI();
        authenticatedOwner.stateHandler.CanRoll = false;
        Timing.RunCoroutine(SetAnimatorWeight(1, 0.1f));
    }

    public void ShotProjectile(DamageUnit projectile)
    {
        Ray aimRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        Quaternion rotateDirection = Quaternion.LookRotation(aimRay.direction);;
        if(Physics.SphereCast(aimRay, 0.2f, out RaycastHit hitInfo, 99f, EnvironmentHelper.Instance.onlyExcludePlayer))
        {
            if(hitInfo.distance > 1f)
                rotateDirection = Quaternion.LookRotation(hitInfo.point - transform.position);
        }

        Instantiate(projectile, transform.position, rotateDirection)
            .SetDamageData(authenticatedOwner);
    }

    bool isAiming = false;
    void DrawArrow()
    {
        if(!AllowProcess())
            return;

        isAiming = true;
        animator.SetBool("IsAiming", true);
        CameraCaching.Instance.SwitchToAimCamera();
    }

    void DrawRelease()
    {
        if(!AllowProcess())
            return;

        animator.SetBool("IsAiming", false);
        isAiming = false;
        CameraCaching.Instance.SwitchToNormalCamera();
    }

    float lastRelease = 0f;
    float holdTime;
    float specialSkillThreshHold = 0.8f;
    float normalAttackThreshHold = 0.25f;
    void Update()
    {
        lastRelease -= Time.deltaTime;
        if(attackAction.IsPressed() && isAiming)
        {
            holdTime += Time.deltaTime;
        }

        if(holdTime > specialSkillThreshHold)
        {
            if(cooldownAbilities.Count > 0 && GetRealTimeCooldownAbilityAtIndex(selectedAbilityIndex) != null)
                if(GetRealTimeCooldownAbilityAtIndex(selectedAbilityIndex).timer.IsFinished)
                    specialSkillThreshHoldVFX.SetActive(true);
        }
        else
        {
            specialSkillThreshHoldVFX.SetActive(false);
        }
        
        if(attackAction.WasReleasedThisFrame())
        {
            if(holdTime > specialSkillThreshHold)
            {
                ShotArrow(true);
            }
            else if(holdTime > normalAttackThreshHold)
            {
                ShotArrow(false);
            }
            holdTime = 0;
            animator.SetBool("IsAiming", false);
            isAiming = false;
            CameraCaching.Instance.SwitchToNormalCamera();
        }
    }

    void ShotArrow(bool tryPerformSpecialSkill)
    {
        if(!isAiming)
            return;
        
        if(lastRelease <= 0)
        {
            lastRelease = reloadTime;
            animator.CrossFade("Standing Aim Recoil", 0.03f);
            if(tryPerformSpecialSkill)
            {
                if(!TryExecuteAbilityAtIndex(selectedAbilityIndex))
                {
                    ShotProjectile(normalProjectile);
                }
            }
            else
                ShotProjectile(normalProjectile);
        }

    }

    public override void OnDeselected()
    {
        base.OnDeselected();
        authenticatedOwner.stateHandler.CanRoll = true;
        isAiming = false;
        holdTime = 0f;
        Timing.RunCoroutine(SetAnimatorWeight(0, 0.1f));
        CameraCaching.Instance.SwitchToNormalCamera();
    }


    IEnumerator<float> SetAnimatorWeight(float value, float time)
    {
        float t = time;
        while(time > 0)
        {
            time -= Time.deltaTime;
            float lerp = Mathf.Lerp(value, 1- value, time/t);
            animator.SetLayerWeight(1, lerp);
            yield return 0;
        }
    }

    public override void RegistryForInput(InputDataHandler inputHandler)
    {
        base.RegistryForInput(inputHandler);
        holdAimAction.Enable();
        // holdAimAction.performed += (ctx) => DrawArrow();
        // holdAimAction.canceled += (ctx) => DrawRelease();
        inputHandler.playerInputAction.Attack.performed += (ctx) => DrawArrow();
        // inputHandler.PlayerInput.Attack.canceled += (ctx) => DrawRelease();
        attackAction = inputHandler.playerInputAction.Attack;
        
    }

    public override void WeaponRiggingSetup(WeaponModelConfig config)
    {
        config.SetLeftHandedWeapon(this.transform);
    }

    public override void WeaponServiceSetup(WeaponServiceLocator weaponService)
    {
        animator = authenticatedOwner.GetComponentInChildren<Animator>();
        GameUIManager.Instance.weaponAbilityUIHandler.SetWeaponAbilitiesData(this);
    }
}
