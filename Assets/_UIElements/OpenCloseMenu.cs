using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button button;

    [SerializeField] GameObject _inventoryTab;
    [SerializeField] GameObject _armourEquipmentTab;

    private bool isOpen = false;

    void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if(!isOpen)
            OpenMenu();
        else
            CloseMenu();
    }

    void OpenMenu()
    {
        isOpen = true;
        _inventoryTab.SetActive(true);
        _armourEquipmentTab.SetActive(true);
        GameStateManager.Instance.Pause();
    }

    void CloseMenu()
    {
        isOpen = false;
        _inventoryTab.SetActive(false);
        _armourEquipmentTab.SetActive(false);
        GameStateManager.Instance.Resume();
    }


}
