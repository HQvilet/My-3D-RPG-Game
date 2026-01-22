using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameUIManager : Singleton<GameUIManager>
{
    [Header("Page Components")]
    public GameObject _mainUI;
    public GameObject _inventoryPanel;
    public GameObject _weaponPanel;
    public GameObject _pausePanel;

    [Header("UI Handlers")]
    public WeaponAbilityUIHandler weaponAbilityUIHandler;
    public WeaponSelectorUIHandler weaponSelectorUIHandler;
    public PlayerSkillUIHandler playerSkillUIHandler;
    public DamageVisualization damageVisualization;

    [Header("Action")]
    public InputAction pauseOrExitAction;
    public InputAction showInventoryAction;
    public InputAction showWeaponAbilityPanel;
    public bool isPausing = false;

    public Action OnInventoryUIEnable;
    public Action OnWeaponAbilityUIEnable;
    public Action OnMainGameUIDisable;

    [Header("Gameplay UI")]
    public TextMeshProUGUI timerTextMesh;
    public GameObject _bossHealthBar;
    public GameObject _winPanel;
    public GameObject _losePanel;

    protected override void Awake()
    {
        base.Awake();
        pauseOrExitAction.Enable();
        pauseOrExitAction.performed += PauseAction;

        // showInventoryAction.Enable();
        // showWeaponAbilityPanel.Enable();
        // showInventoryAction.performed += (context) =>
        // {
        //     if(!isPausing)
        //     {
        //         ShowInGameInventory();
        //     }
        // };

        // showWeaponAbilityPanel.performed += (ctx) =>
        // {
        //     if(!isPausing)
        //     {
        //         ShowWeaponAbilityGUI();
        //     }
        // };
    }

    void PauseAction(InputAction.CallbackContext callback)
    {
        
        PauseOrUnpauseGame();
        
    }

    public void ShowInGamePausePanel()
    {
        EnablePlayerGUIAction();
        _pausePanel.SetActive(true);
        
    }

    void EnablePlayerGUIAction()
    {
        isPausing = true;
        GameManager.Instance.Pause();
        UseCursor(true);
    }

    void DisablePlayerGUIAction()
    {
        isPausing = false;
        GameManager.Instance.Resume();
        UseCursor(false);
    }

    public void ShowInGameInventory()
    {
        isPausing = true;
        GameManager.Instance.Pause();
        UseCursor(true);

        _inventoryPanel.SetActive(true);
        OnInventoryUIEnable?.Invoke();
    }

    public void ShowWeaponAbilityGUI()
    {
        isPausing = true;
        GameManager.Instance.Pause();
        UseCursor(true);

        _weaponPanel.SetActive(true);
        OnWeaponAbilityUIEnable?.Invoke();
    }

    public void DisableAllGUI()
    {
        _weaponPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        OnMainGameUIDisable?.Invoke();
        DisablePlayerGUIAction();
    }


    public void UseCursor(bool check)
    {
        
        Cursor.visible = check;
        if(check)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void ClearGameResultUI()
    {
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
        SetTimerVisibility(false);
        UseCursor(false);
    }

    public void ClearGameGUI()
    {
        _weaponPanel.SetActive(false);
        _inventoryPanel.SetActive(false);
        _pausePanel.SetActive(false);
        OnMainGameUIDisable?.Invoke();
        DisablePlayerGUIAction();
    }

    public void PauseOrUnpauseGame()
    {
        if(!isPausing)
        {
            ShowInGamePausePanel();
        }
        else
        {
            ClearGameGUI();
        }
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.SaveGame();
        GameManager.Instance.BackToMainMenu();
    }

    
    public void ShowWinningPanel()
    {
        _winPanel.SetActive(true);
        UseCursor(true);
    }

    public void ShowLosingPanel()
    {
        _losePanel.SetActive(true);
        UseCursor(true);
    }
    
    public void SetTimer(int time) => timerTextMesh.text = $"{time/60:D2} : {time%60:D2}";

    public void SetTimerVisibility(bool v) => timerTextMesh.gameObject.SetActive(v);

    public void SetBossHealthBarVisibility(bool v)
    {
        _bossHealthBar.gameObject.SetActive(v);
    }

    void OnDestroy()
    {
        pauseOrExitAction.performed -= PauseAction;
    }
}
