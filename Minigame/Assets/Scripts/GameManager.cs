using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool TriShotEnabled = false;

    private void Awake()
    {
        Instance = this;
    }

    public void TriShotUpgrade(float duration=10f)
    {
        if (TriShotEnabled) { StopCoroutine("CountdownTimerTri"); }

        StartCoroutine(CountdownTimerTri(duration));
    }

    IEnumerator CountdownTimerTri(float amount)
    {
        TriShotEnabled = true;

        yield return new WaitForSeconds(amount);

        TriShotEnabled = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
