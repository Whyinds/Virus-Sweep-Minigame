using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float xRange = 7;

    AudioSource telportSound;

    private void Awake()
    {
        health += GameManager.Instance.bossesDefeated;
        telportSound = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (isDead)
        {
            Camera.main.GetComponent<CameraShake>().SetNewShake(0.5f, 0.7f);
            GameManager.Instance.BossWin();
        }    
    }

    public override void DoActionOnHit()
    {
        var newPosX = Random.Range(xRange * -1, xRange);

        transform.position = new Vector2(newPosX, transform.position.y);
        telportSound.Play();
    }
}
