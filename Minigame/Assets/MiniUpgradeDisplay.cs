using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniUpgradeDisplay : MonoBehaviour
{
    public float moveSpeed = 1.2f;

    private void Update()
    {
        MoveUp();
    }
    void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
}
