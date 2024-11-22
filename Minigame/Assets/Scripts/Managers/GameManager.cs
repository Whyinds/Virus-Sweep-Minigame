using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool TriShotEnabled = false;

    public int coinsCollected = 0;

    public int bossesDefeated = 0;
    public int bossesToWin = 3;

    public bool wonGame = false;

    public GameObject WinCanvas;

    public TextMeshProUGUI winScoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI winHighscoreText;

    Toggle camShakeToggle;

    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 0);
        }

        if (WinCanvas != null)
        {
            WinCanvas.SetActive(false);
        }

        if (!PlayerPrefs.HasKey("Shake"))
        {
            PlayerPrefs.SetInt("Shake", 1);
        }

        
    }

    private void Start()
    {
        camShakeToggle = FindObjectOfType<Toggle>();
        if (camShakeToggle != null) {
            if (PlayerPrefs.GetInt("Shake") == 1)
            {
                camShakeToggle.isOn = true;
            }
            else
            {
                camShakeToggle.isOn = false;
            }
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

    public void BossWin()
    {
        bossesDefeated++;
        
        if (bossesDefeated >= bossesToWin)
        {
            wonGame = true;
            Camera.main.gameObject.GetComponent<CameraShake>().GameDone = true;
            foreach (var enemy in FindObjectsOfType<Enemy>())
            {
                enemy.gameObject.SetActive(false);
            }

            foreach (var projectile in FindObjectsOfType<Projectile>())
            {
                projectile.gameObject.SetActive(false);
            }

            foreach (var coin in FindObjectsOfType<Coin>())
            {
                coin.gameObject.SetActive(false);
            }

            foreach (var powerup in FindObjectsOfType<PowerUp>())
            {
                powerup.gameObject.SetActive(false);
            }

            UpgradeSpawner.Instance.StopSpawning();
            EnemySpawner.Instance.StopSpawning();
            CoinSpawner.Instance.StopSpawning();

            PlayerShooter.instance.StopPlayer();

            StartCoroutine(ShowWinScreen());
        }
    }

    void SetWinScore()
    {
        ProgressManager.Instance.UpdateProgress();
        WinCanvas.SetActive(true);
        winScoreText.SetText("Score: " + ScoreManager.instance.score);
        coinText.SetText("Total Credits: " + PlayerPrefs.GetInt("coins"));
        SetHighScore();
    }

    void SetHighScore()
    {
        if (ScoreManager.instance.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreManager.instance.score);
            winHighscoreText.SetText("Highscore: " + ScoreManager.instance.score);
        }
        else
        {
            winHighscoreText.SetText("Highscore: " + PlayerPrefs.GetInt("HighScore", 0));
        }
    }

    IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(1f);
        SetWinScore();
    }

    public void EnablingCameraShake()
    {
        int TF_int = camShakeToggle.isOn ? 1 : 0;
        PlayerPrefs.SetInt("Shake", TF_int);
    }

}
