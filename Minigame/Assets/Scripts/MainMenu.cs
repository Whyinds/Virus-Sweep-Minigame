using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public TextMeshProUGUI highScore;
    public TextMeshProUGUI coins;

    public static MainMenu instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore.SetText("Highscore: " + PlayerPrefs.GetInt("HighScore"));
        ResetCoins();
    }

    public void ResetCoins()
    {
        coins.SetText("Coins: " + PlayerPrefs.GetInt("coins"));
    }

}
