using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(int sceneId)
    {
        GameManager.Instance.ResettingScene();
        SceneManager.LoadScene(sceneId);
    }

    public void ReloadScene()
    {
        GameManager.Instance.ResettingScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
