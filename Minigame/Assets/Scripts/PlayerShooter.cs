using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    bool gameOver = false;

    [Header("***Projectile***")]
    public GameObject projectile;
    public AudioSource shootSound;
    public Transform launchPoint;
    public float launchSpeed = 10f;
    public float secondsToDespawn = 5f;
    public float maxProjectiles = 3f;
    [HideInInspector] public float currentProjectiles = 0f;

    [Header("***Trajectory Display***")]
    public LineRenderer lineRenderer;
    public int linePoints = 175;
    public float timeIntervalPoints = 0.01f;

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
            DrawTrajectory();
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
            
            Destroy(_projectile, secondsToDespawn);
        }
    }

    void DrawTrajectory()
    {
        if (lineRenderer == null) { return; }
        Vector2 origin = launchPoint.position;
        Vector2 startVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = linePoints;
        float time = 0;

        for (int i = 0; i < linePoints; i++)
        {
            var x = (startVelocity.x * time);
            var y = (startVelocity.y * time);
            Vector2 point = new Vector2(x, y);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalPoints;
        }
    }

    void StopPlayer()
    {
        gameOver = true;
    }


}
