using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    bool gameOver = false;

    public static PlayerShooter instance;

    [Header("***Projectile***")]
    public GameObject projectile;
    public AudioSource shootSound;
    public Transform launchPoint;
    public float launchSpeed = 10f;
    public float maxProjectiles = 3f;
    [HideInInspector] public float currentProjectiles = 0f;

    [Header("***Power Up Potential***")]
    public float maxProjectileUpgrade = 3f;
    [HideInInspector] public float currentProjectileUpgrades = 0f;
    public float trajectoryLineDuration = 10f;
    public GameObject specialProjectile;
    bool canShootSpecial = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnGameOver += StopPlayer;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver) {
            FollowMouse();
            ShootProjectile();
        }
    }

    void FollowMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 directionToFace = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = directionToFace;
    }

    void ShootProjectile()
    {
        if (Input.GetMouseButtonDown(0) && currentProjectiles < maxProjectiles)
        {
            if (projectile == null) { return; }

            currentProjectiles++;

            shootSound.Play();

            var _projectile = Instantiate(projectile, launchPoint.position, launchPoint.rotation);
            _projectile.GetComponent<Rigidbody2D>().velocity = launchSpeed * launchPoint.up;
        }
    }

    

    void StopPlayer()
    {
        gameOver = true;
    }

    public void CountProjectile()
    {
        currentProjectiles = FindObjectsOfType<Projectile>().Length;
    }

    public void AddProjectileAmountUpgrade()
    {
        if (currentProjectileUpgrades >= maxProjectileUpgrade) { return; }
        maxProjectiles++;
        currentProjectileUpgrades++;

        var enemySpawnObj = Instantiate(FindObjectOfType<EnemySpawner>().gameObject);

        var allSpawners = FindObjectsOfType<EnemySpawner>();

        foreach (EnemySpawner spawns in allSpawners)
        {
            spawns.IncreaseSpawnRate();
        }
    }

}
