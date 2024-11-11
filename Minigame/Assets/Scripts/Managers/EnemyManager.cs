using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public float EnemyMoveSpeedMult = 1f;
    public float MultIncreaseRate = 0.25f;

    public List<GameObject> EnemyGroups;

    private void Awake()
    {
        instance = this;
    }

    public void IncreaseEnemyMoveRate()
    {
        EnemyMoveSpeedMult += MultIncreaseRate;
    }

    public List<GameObject> GetEnemyGroups()
    {
        return EnemyGroups;
    }
}
