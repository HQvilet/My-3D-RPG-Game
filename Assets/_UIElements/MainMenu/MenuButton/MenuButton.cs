using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetScale(float size)
    {
        transform.localScale = Vector3.one * size;
    }

    public void SetTextColor(Color color)
    {
        if(textMesh)
            textMesh.color = color;
    }

    public void PlayAudio(AudioSource audioSource)
    {
        audioSource.Play();
    }

    public void SetHoverColor()
    {
        if(textMesh) textMesh.color = hoverColor;
    }

    public void SetNormalColor()
    {
        if(textMesh) textMesh.color = normalColor;
    }

    
}
