using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverHighScoreText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerHealth.OnGameOver += SetGameOverScore;
        scoreText.GetComponentInParent<Canvas>().enabled = true;
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.SetText("Score: " + score);
    }

    void SetGameOverScore()
    {
        scoreText.GetComponentInParent<Canvas>().enabled = false;
        gameOverScoreText.SetText("Score: " + score);
        SetHighScore();
    }

    void SetHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            gameOverHighScoreText.SetText("NEW Highscore: " + score);
        } else
        {
            gameOverHighScoreText.SetText("Highscore: " + PlayerPrefs.GetInt("HighScore", 0));
        }
    }

}
