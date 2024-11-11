using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Basic Settings")]
    public int damage = 1;
    public int numOfBounces = 3;

    Rigidbody2D RGBProjectile;
    PlayerShooter player;
    public AudioSource bounceAudio;
    OnDestroyAudio onDestroyAudio;

    private Vector3 lastVelocity;
    private float curSpeed;
    private Vector3 direction;
    [HideInInspector] public int curBounces = 0;
    [HideInInspector] public int curEnemiesHit = 0;

    bool despawning = false;

    [Header("Bounce Boosts")]
    public float speedMultiplier = 1.25f;
    public float damageMultiplier = 2f;

    private void Start()
    {
        player = FindObjectOfType<PlayerShooter>();
        RGBProjectile = GetComponent<Rigidbody2D>();
        onDestroyAudio = GetComponent<OnDestroyAudio>();

        PlayerHealth.OnGameOver += DeleteProjectile;
    }

    private void LateUpdate()
    {
        lastVelocity = RGBProjectile.velocity;
    }

    void DeleteProjectile()
    {
        OnDelete();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.GetComponent<Enemy>())
        {
            
            if (collision.GetComponent<Enemy>().isDead) { return; }
            curEnemiesHit++;
            collision.GetComponent<Enemy>().LoseHealth(damage, this);
        } else if (collision.gameObject.tag == "Despawner")
        {
            OnDelete();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        curBounces++;
        if (curBounces > numOfBounces) { onDestroyAudio.OnDelete(); OnDelete(); }
        if (bounceAudio != null && !despawning) { bounceAudio.Play(); }

        curSpeed = lastVelocity.magnitude * speedMultiplier;
        //damage = Mathf.CeilToInt(damage*damageMultiplier);
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        RGBProjectile.velocity = direction * Mathf.Max(curSpeed,0);

        
    }


    private void OnDelete()
    {
        despawning = true;
        PlayerHealth.OnGameOver -= DeleteProjectile;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        player.CountProjectile();
    }

}
