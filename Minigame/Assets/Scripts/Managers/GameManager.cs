using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool TriShotEnabled = false;

    public int coinsCollected = 0;

    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
        }
    }

    public void TriShotUpgrade(float duration=10f)
    {
        if (TriShotEnabled) { StopCoroutine("CountdownTimerTri"); }

        StartCoroutine(CountdownTimerTri(duration));
    }

    IEnumerator CountdownTimerTri(float amount)
    {
        TriShotEnabled = true;

        yield return new WaitForSeconds(amount);

        TriShotEnabled = false;
    }

    void OnEnable()
    {
        if (FindAnyObjectByType<PlayerHealth>())
        {
            PlayerHealth.OnGameOver += AddCoins;
        }
    }

    public void ResettingScene()
    {
        TriShotEnabled = false;
        coinsCollected = 0;

        if (FindAnyObjectByType<PlayerHealth>())
        {
            PlayerHealth.OnGameOver -= AddCoins;
        }
    }

    void AddCoins()
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + coinsCollected);
    }

    private void OnDestroy()
    {
        PlayerHealth.OnGameOver -= AddCoins;
        StopAllCoroutines();
    }
}
