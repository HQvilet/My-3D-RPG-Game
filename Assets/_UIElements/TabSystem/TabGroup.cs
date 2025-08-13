using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    List<MTabButton> tabButtons;
    [SerializeField] MTabButton isSelectingTab;

    void Awake()
    {
        tabButtons = GetComponentsInChildren<MTabButton>().ToList();
        foreach (MTabButton tab in tabButtons)
        {
            var tabButt = tab.GetComponent<Button>();
            tabButt.onClick.AddListener(() =>
            {
                isSelectingTab?.OnDeselect();
                isSelectingTab = tab;
                isSelectingTab.OnSelect();
            });
        }
    }






}
