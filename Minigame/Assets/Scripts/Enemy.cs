using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1f;
    public int health = 1;
    public int attackDMG = 1;

    public float aboveYProtection = 5.5f;

    bool gameOver = false;

    OnDestroyAudio onDestroyAudio;

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

    public void LoseHealth(int damage = 1)
    {
        if (transform.position.y > aboveYProtection) { return; }
        health -= damage;
        if (health <= 0) { onDestroyAudio.OnDelete(); Destroy(gameObject); }
    }

    void StopEnemy()
    {
        gameOver = true;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnGameOver -= StopEnemy;
    }

}
