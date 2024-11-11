using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 10;
    [HideInInspector] public float health;

    public Canvas gameOverUI;
    public Image healthbarFill;

    public AudioSource hitAudio;
    public GameObject gameOverAudioObject;

    public static event Action OnGameOver;

    public TextMeshProUGUI fileNameText;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("losses"))
        {
            PlayerPrefs.SetInt("losses", 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameOverUI.enabled = false;
        healthbarFill.fillAmount = 1;

        fileNameText.text = "Mom's Credit Card #" + (PlayerPrefs.GetInt("losses") + 1).ToString();
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        
        SetHealthBar();

        FindObjectOfType<CameraShake>().shakeDuration += 0.1f;

        if (health <= 0)
        {
            PlayerPrefs.SetInt("losses", PlayerPrefs.GetInt("losses")+1);
            gameOverUI.enabled = true;
            OnGameOver.Invoke();
            var audioObject = Instantiate(gameOverAudioObject);
            Destroy(audioObject, audioObject.GetComponent<AudioSource>().clip.length + 0.1f);
            gameObject.SetActive(false);
            return;
        }

        if (hitAudio != null) { hitAudio.Play(); }
    }

    public void GainHealth(int restored)
    {
        health += restored;
        if (health > maxHealth) { health = maxHealth; }
        SetHealthBar();
    }

    void SetHealthBar()
    {
        float healthAmount = health / maxHealth;
        healthbarFill.fillAmount = healthAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            LoseHealth(collision.gameObject.GetComponent<Enemy>().attackDMG);
            Destroy(collision.gameObject);
        }
    }
}
