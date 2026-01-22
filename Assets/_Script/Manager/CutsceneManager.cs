using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneManager : MonoBehaviour
{
    public UnityEvent OnEnterCutscene;
    public UnityEvent OnExitCutscene;
    public void EnterCutscene()
    {
        GameUIManager.Instance._mainUI.gameObject.SetActive(false);
        OnEnterCutscene?.Invoke();
    }

    public void ExitCutscene()
    {
        GameUIManager.Instance._mainUI.gameObject.SetActive(true);
        OnExitCutscene?.Invoke();
    }
}
