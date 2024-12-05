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
    public Color TriShotColor = Color.green;

    [Header("***Power Up Potential***")]
    public float maxProjectileUpgrade = 3f;
    [HideInInspector] public float currentProjectileUpgrades = 0f;
    public float trajectoryLineDuration = 10f;
    public GameObject specialProjectile;

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

            if (GameManager.Instance.TriShotEnabled)
            {
                var _projectileTwo = Instantiate(projectile, launchPoint.position, launchPoint.rotation *= Quaternion.Euler(0, 0, 15));
                _projectileTwo.GetComponent<Rigidbody2D>().velocity = launchSpeed * launchPoint.up;
                _projectileTwo.GetComponent<Projectile>().original = false;
                _projectileTwo.GetComponent<SpriteRenderer>().color = TriShotColor;
                _projectileTwo.GetComponentInChildren<TrailRenderer>().startColor = TriShotColor;
                _projectileTwo.GetComponentInChildren<TrailRenderer>().endColor = TriShotColor;

                var _projectileThree = Instantiate(projectile, launchPoint.position, launchPoint.rotation *= Quaternion.Euler(0, 0, -30));
                _projectileThree.GetComponent<Rigidbody2D>().velocity = launchSpeed * launchPoint.up;
                _projectileThree.GetComponent<Projectile>().original = false;
                _projectileThree.GetComponent<SpriteRenderer>().color = TriShotColor;
                _projectileThree.GetComponentInChildren<TrailRenderer>().startColor = TriShotColor;
                _projectileThree.GetComponentInChildren<TrailRenderer>().endColor = TriShotColor;

                launchPoint.rotation *= Quaternion.Euler(0, 0, 15);
            }
            
        }
    }

    

    public void StopPlayer()
    {
        gameOver = true;
    }

    public void CountProjectile()
    {
        currentProjectiles--;
        CheckAnimation();
    }

    public void AddProjectileAmountUpgrade()
    {
        if (currentProjectileUpgrades >= maxProjectileUpgrade) { return; }
        maxProjectiles++;
        currentProjectileUpgrades++;

        CheckAnimation();

        FindObjectOfType<EnemySpawner>().IncreaseSpawnRate();
        EnemyManager.instance.IncreaseEnemyMoveRate();
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
