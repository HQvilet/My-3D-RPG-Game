using System;
using System.Collections;
using System.Collections.Generic;
using EditorAttributes;
using GameSaveLoadSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{

    public Action OnGamePaused;
    public Action OnGameResumed;

    protected override void Awake()
    {
        base.Awake();
        // LoadGame();
        DontDestroyOnLoad(gameObject);
    }

    public void EnterMainGamePlay()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;
    }


    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
    }

    [Button("Load")]
    public void LoadGame()
    {
        GameDataManager.Load();
    }

    [Button("Save")]
    public void SaveGame()
    {
        GameDataManager.Save();
    }

    [Button("Quit")]
    void QuitGame()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }
}
