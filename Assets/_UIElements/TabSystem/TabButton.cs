using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTabButton : MonoBehaviour
{
    [SerializeField] GameObject panel;

    public void OnSelect()
    {
        panel.SetActive(true);
    }

    public void OnDeselect()
    {
        panel.SetActive(false);
    }
}
