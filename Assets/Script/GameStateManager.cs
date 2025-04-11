using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public void Pause()
    {
        Time.timeScale = 0f;
        Debug.Log("paused");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        Debug.Log("resume");
    }
}
