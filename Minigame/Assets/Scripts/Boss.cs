using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float xRange = 7;

    private void Awake()
    {
        health += GameManager.Instance.bossesDefeated;
    }

    private void OnDestroy()
    {
        if (isDead)
        {
            GameManager.Instance.BossWin();
        }    }

    public override void DoActionOnHit()
    {
        var newPosX = Random.Range(xRange * -1, xRange);

        transform.position = new Vector2(newPosX, transform.position.y);
    }
}
