using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    
    public static CoinSpawner Instance;

    public float XSpawnRange = 6f;
    public GameObject coinPrefab;
    public float SpawnRateMin = 5f;
    public float SpawnRateMax = 10f;

    bool spawnCoin = true;
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
        if (spawnCoin && !waitingToSpawn)
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
        if (spawnCoin)
        {
            Vector3 spawnPos = new Vector3(Random.Range(XSpawnRange * -1, XSpawnRange), transform.position.y);
            var coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }

    }

    void StopSpawning()
    {
        spawnCoin = false;
    }
}
