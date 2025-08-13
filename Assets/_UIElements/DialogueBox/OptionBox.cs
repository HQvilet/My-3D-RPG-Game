using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMesh;
    private int index;
    public int GetIndex() => index;


    public void SetInfo(int index, string text, UnityAction action)
    {
        this.index = index;
        textMesh.text = text;
        GetComponent<Button>().onClick.AddListener(action);
        
    }
    
}
