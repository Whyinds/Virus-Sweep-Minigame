using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 10;
    [HideInInspector] public float health;

    public Canvas gameOverUI;
    public Image healthbarFill;

    public static event Action OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        gameOverUI.enabled = false;
        healthbarFill.fillAmount = 1;
    }

    public void LoseHealth(int damage)
    {
        health -= damage;
        SetHealthBar();
        if (health <= 0)
        {
            gameOverUI.enabled = true;
            OnGameOver.Invoke();
            gameObject.SetActive(false);
        }
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
