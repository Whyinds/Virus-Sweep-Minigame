using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    bool gavePowerUp = false;

    public float moveSpeed = 1f;
    public GameObject MiniTextUpgradeDisplay;

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
        PlayerHealth.OnGameOver += StopUpgrade;
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Despawner") { Destroy(gameObject); }
        if (collision.TryGetComponent<Projectile>(out  var projectile) && !gavePowerUp)
        {
            gavePowerUp = true;
            GivePowerUp();
            GetComponent<OnDestroyAudio>().OnDelete();

            var miniDisplay = Instantiate(MiniTextUpgradeDisplay, transform.position, Quaternion.identity);
            Destroy(miniDisplay, 1f);

            Destroy(gameObject);
        }
    }

    virtual public void GivePowerUp()
    {
        throw new System.Exception();
    }

    public void StopUpgrade()
    {
        gameOver = true; 
        Destroy(gameObject);
    }
}
