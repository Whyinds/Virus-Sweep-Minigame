using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public float XSpawnRange = 6f;
    public float SpawnRateMin = 0.5f;
    public float SpawnRateMax = 3f;

    bool spawnEnemy = true;
    bool waitingToSpawn = false;

    public int currentWave = 0;
    public int wavesToBoss = 15;

    List<GameObject> EnemyGroupList;

    public GameObject BossPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        PlayerHealth.OnGameOver += StopSpawning;
        EnemyGroupList = EnemyManager.instance.GetEnemyGroups();
        if (XSpawnRange < 0) { XSpawnRange *= -1; }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnEnemy && !waitingToSpawn)
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    IEnumerator SpawnEnemy()
    {
        waitingToSpawn = true;
        yield return new WaitForSeconds(Random.Range(SpawnRateMin, SpawnRateMax));
        waitingToSpawn = false;

        // If game over while waiting
        if (spawnEnemy)
        {
            if (currentWave % wavesToBoss != 0 || currentWave < wavesToBoss)
            {
                var groupChoice = EnemyGroupList[Random.Range(0, EnemyGroupList.Count)];
                Instantiate(groupChoice, transform.position, Quaternion.identity);
                
            } else
            {
                Instantiate(BossPrefab, transform.position, Quaternion.identity);
            }
            currentWave++;
        }
        
    }

    public void StopSpawning()
    {
        spawnEnemy = false;
    }

    public void IncreaseSpawnRate(float amount=0.5f)
    {
        SpawnRateMin -= amount;
        SpawnRateMax -= amount*3;

        

        if (SpawnRateMin <= 0)
        {
            SpawnRateMin = 0.05f;
        }
        if (SpawnRateMax <= 0)
        {
            SpawnRateMax = SpawnRateMin + 0.5f;
        }
    }
}
