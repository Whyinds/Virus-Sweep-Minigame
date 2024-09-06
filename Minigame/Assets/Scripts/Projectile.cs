using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public int damage = 1;
    public int numOfBounces = 3;

    Rigidbody2D RGBProjectile;
    PlayerShooter player;

    private Vector3 lastVelocity;
    private float curSpeed;
    private Vector3 direction;
    int curBounces = 0;

    private void Start()
    {
        player = FindObjectOfType<PlayerShooter>();
        RGBProjectile = GetComponent<Rigidbody2D>();

        PlayerHealth.OnGameOver += DeleteProjectile;
    }

    private void LateUpdate()
    {
        lastVelocity = RGBProjectile.velocity;
    }

    void DeleteProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            collision.GetComponent<Enemy>().LoseHealth(damage);
            Destroy(gameObject);
        } else if (collision.gameObject.tag == "Despawner")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (curBounces > numOfBounces) { Destroy(gameObject); }

        curSpeed = lastVelocity.magnitude;
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        RGBProjectile.velocity = direction * Mathf.Max(curSpeed,0);

        curBounces++;
    }


    private void OnDestroy()
    {
        player.currentProjectiles--;
        PlayerHealth.OnGameOver -= DeleteProjectile;
    }

}
