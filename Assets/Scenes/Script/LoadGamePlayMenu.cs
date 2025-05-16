using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGamePlayMenu : MonoBehaviour
{

    public void LoadMainGam() => SceneManager.LoadSceneAsync(0);
}
