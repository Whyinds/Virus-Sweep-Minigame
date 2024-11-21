using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public float maxHealth = 10;
    [HideInInspector] public float health;

    public Canvas gameOverUI;
    public Image healthbarFill;

    public AudioSource hitAudio;
    public GameObject gameOverAudioObject;

    public static event Action OnGameOver;

    public TextMeshProUGUI fileNameText;

    public GameObject ParticlesOnDefeat;

    private void Awake()
    {
        Instance = this;
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

        Camera.main.GetComponent<CameraShake>().SetNewShake(0.1f);

        if (health <= 0)
        {
            PlayerPrefs.SetInt("losses", PlayerPrefs.GetInt("losses")+1);
            Camera.main.GetComponent<CameraShake>().SetNewShake(0.5f, 1f, true);
            gameOverUI.enabled = true;
            OnGameOver.Invoke();
            var particles = Instantiate(ParticlesOnDefeat, transform.position, Quaternion.identity);
            Destroy(particles, particles.GetComponent<ParticleSystem>().main.duration + 1f);
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
