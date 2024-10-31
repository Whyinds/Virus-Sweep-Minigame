using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool gavePowerUp = false;

    public float moveSpeed = 1f;

    bool gameOver = false;

    void Update()
    {
        if (!gameOver)
        {
            MoveDown();
        }
    }

    void Start()
    {
        PlayerHealth.OnGameOver += StopCoin;
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Despawner") { Destroy(gameObject); }
        if (collision.TryGetComponent<Projectile>(out var projectile) && !gavePowerUp)
        {
            gavePowerUp = true;
            GiveCoin();
            GetComponent<OnDestroyAudio>().OnDelete();

            Destroy(gameObject);
        }
    }

    public void GiveCoin()
    {
        GameManager.Instance.coinsCollected++;
        ScoreManager.instance.AddCoin();
    }

    public void StopCoin()
    {
        gameOver = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlayerHealth.OnGameOver -= StopCoin;
    }
}
