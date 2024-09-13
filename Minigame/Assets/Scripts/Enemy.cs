using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1f;
    public int health = 1;
    public int attackDMG = 1;
    public int scoreAmout = 100;

    public float aboveYProtection = 5.5f;

    bool gameOver = false;
    public bool isDead = false;

    OnDestroyAudio onDestroyAudio;
    Projectile projectile;
    public GameObject miniScoreDisplayPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnGameOver += StopEnemy;
        onDestroyAudio = GetComponent<OnDestroyAudio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            MoveDown();
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    public void LoseHealth(int damage = 1, Projectile bullet = null)
    {
        if (isDead) { return; }
        isDead = true;
        if (transform.position.y > aboveYProtection) { return; }
        health -= damage;
        if (health <= 0) {
            DisplayScoreAdded(bullet);
            Destroy(gameObject); }
    }

    void StopEnemy()
    {
        gameOver = true;
    }

    void DisplayScoreAdded(Projectile bullet)
    {
        var score = FindObjectOfType<ScoreManager>();

        int addedScore = scoreAmout * bullet.curEnemiesHit * (bullet.curBounces + 1);

        score.AddScore(addedScore);

        var miniScoreDisplay = Instantiate(miniScoreDisplayPrefab, transform.position, Quaternion.identity);
        miniScoreDisplay.GetComponent<MiniScoreText>().scoreAmount = addedScore;
        Destroy(miniScoreDisplay, 1f);
    }

    private void OnDestroy()
    {
        
        PlayerHealth.OnGameOver -= StopEnemy;
    }

}
