using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInterface : MonoBehaviour
{
    public GameObject _tutorialPanel;
    public void LoadMainGameWithData()
    {
        GameManager.Instance.LoadGame();
        GameManager.Instance.EnterMainGamePlay();
    }

    public void LoadNewGamePlay()
    {
        GameManager.Instance.EnterMainGamePlay();
    }

    public void ShowTutorialPanel(bool check)
    {
        _tutorialPanel.gameObject.SetActive(check);
    }

    public void ExitGame() => Application.Quit();
}
