using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestSystemUI : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject questPanel;

    [SerializeField] QuestUI questLinePref;

    void Start()
    {
        
        button.onClick.AddListener(() =>
        {
            questPanel.transform.parent.gameObject.SetActive(!questPanel.transform.parent.gameObject.activeSelf);
        });

        foreach (var a in QuestSystemManager.Instance.quests)
        {
            Instantiate(questLinePref, questPanel.transform)
                .SetQuest(a);
        }

        questPanel.SetActive(false);
    }

    public void AddQuest()
    {

    }
}
