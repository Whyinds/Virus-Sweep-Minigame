using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI gameOverScoreText;

    public void AddScore(int amount)
    {
        score += amount;
        gameOverScoreText.text = score.ToString();
    }
}
