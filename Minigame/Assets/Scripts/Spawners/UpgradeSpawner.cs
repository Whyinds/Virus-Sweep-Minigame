using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public static UpgradeSpawner Instance;
    
    public float XSpawnRange = 6f;
    public List<GameObject> upgradePrefabs;
    public float SpawnRateMin = 40f;
    public float SpawnRateMax = 60f;

    bool spawnUpgrade = true;
    bool waitingToSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

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
            var randomUpgrade = upgradePrefabs[Random.Range(0, upgradePrefabs.Count)];

            Vector3 spawnPos = new Vector3(Random.Range(XSpawnRange * -1, XSpawnRange), transform.position.y);
            var upgrade = Instantiate(randomUpgrade, spawnPos, Quaternion.identity);
        }

    }

    void StopSpawning()
    {
        spawnUpgrade = false;
    }

    public void IncreaseSpawnTime(float amount=2f)
    {
        SpawnRateMin += amount;
        SpawnRateMax += amount;
    }

}
