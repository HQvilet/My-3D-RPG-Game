using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameStateManager : Singleton<GameStateManager>
{
    public Action OnGamePaused;
    public Action OnGameResumed;
    public UnityAction<float> ltest;
    public UnityEvent<GameObject> ltest1;
    public void Pause()
    {
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
        Debug.Log("paused");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        OnGameResumed?.Invoke();
        Debug.Log("resume");
    }
}
