using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestUI : MonoBehaviour, IPointerClickHandler
{
    public BaseQuest quest;
    [SerializeField] TextMeshProUGUI titleText;

    public void OnPointerClick(PointerEventData eventData)
    {
        quest.OnStartQuest();
    }

    public void SetQuest(BaseQuest quest)
    {
        this.quest = quest;
        titleText.text = quest.Title;

    }
    
}
