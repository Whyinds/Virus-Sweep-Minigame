using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float moveSpeed = 1f;
    public int health = 1;
    public int attackDMG = 1;

    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnGameOver += StopEnemy;
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
        health -= damage;
        if (health <= 0) { Destroy(gameObject); }
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
