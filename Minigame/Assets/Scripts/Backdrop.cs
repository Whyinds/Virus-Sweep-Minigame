using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backdrop : MonoBehaviour
{

    public List<Sprite> BGs;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("BG"))
        {
            PlayerPrefs.SetInt("BG", 0);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        SetBG();
    }

    public void SetNewBG()
    {

        var coinCount = PlayerPrefs.GetInt("coins");

        if (coinCount >= 15)
        {
            PlayerPrefs.SetInt("coins", coinCount - 15);
            MainMenu.instance.ResetCoins();

            int newBGID = PlayerPrefs.GetInt("BG") + 1;

            if (newBGID >= BGs.Count)
            {
                newBGID = 0;
            }

            PlayerPrefs.SetInt("BG", newBGID);

            SetBG();
        }
    }

    void SetBG()
    {
        spriteRenderer.sprite = BGs[PlayerPrefs.GetInt("BG")];
    }
}
