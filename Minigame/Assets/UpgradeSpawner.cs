using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public float XSpawnRange = 6f;
    public GameObject upgradePrefab;
    public float SpawnRateMin = 40f;
    public float SpawnRateMax = 60f;

    bool spawnUpgrade = true;
    bool waitingToSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnGameOver += StopSpawning;

        if (XSpawnRange < 0) { XSpawnRange *= -1; }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnUpgrade && !waitingToSpawn)
        {
            StartCoroutine(SpawnUpgrade());
        }
    }

    IEnumerator SpawnUpgrade()
    {
        waitingToSpawn = true;
        yield return new WaitForSeconds(Random.Range(SpawnRateMin, SpawnRateMax));
        waitingToSpawn = false;

        // If game over while waiting
        if (spawnUpgrade)
        {
            Vector3 spawnPos = new Vector3(Random.Range(XSpawnRange * -1, XSpawnRange), transform.position.y);
            var upgrade = Instantiate(upgradePrefab, spawnPos, Quaternion.identity);
        }

    }

    void StopSpawning()
    {
        spawnUpgrade = false;
    }

}
