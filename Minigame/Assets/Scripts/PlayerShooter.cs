using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    bool gameOver = false;

    public static PlayerShooter instance;

    Animator animator;

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
        animator = GetComponent<Animator>();
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
        if (currentProjectiles >= maxProjectiles)
        {
            transform.rotation = Quaternion.identity;
            return;
        }
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

            CheckAnimation();

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
        CheckAnimation();
    }

    public void AddProjectileAmountUpgrade()
    {
        if (currentProjectileUpgrades >= maxProjectileUpgrade) { return; }
        maxProjectiles++;
        currentProjectileUpgrades++;

        CheckAnimation();

        var enemySpawnObj = Instantiate(FindObjectOfType<EnemySpawner>().gameObject);

        var allSpawners = FindObjectsOfType<EnemySpawner>();

        foreach (EnemySpawner spawns in allSpawners)
        {
            spawns.IncreaseSpawnRate();
        }
    }

    void CheckAnimation()
    {
        if (animator == null) { return; }
        if (currentProjectiles < maxProjectiles)
        {
            animator.SetBool("HasBalls", true);
        }
        else if (currentProjectiles >= maxProjectiles)
        {
            animator.SetBool("HasBalls", false);
        }
    }

}
