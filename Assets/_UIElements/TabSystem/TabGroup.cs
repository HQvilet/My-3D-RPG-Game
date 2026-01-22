using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    List<TabBinder> tabButtons;
    TabBinder isSelectingTab;

    void Awake()
    {
        tabButtons = GetComponentsInChildren<TabBinder>().ToList();
        foreach (TabBinder tab in tabButtons)
        {
            var tabButt = tab.GetComponent<Button>();
            tabButt.onClick.AddListener(() =>
            {
                isSelectingTab?.OnDeselect();
                isSelectingTab = tab;
                isSelectingTab.OnSelect();
            });
        }
        isSelectingTab = tabButtons[0];
    }






}
