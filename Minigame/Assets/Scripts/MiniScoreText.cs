using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MiniScoreText : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public int scoreAmount = 100;
    TextMeshProUGUI scoreDisplay;

    private void Start()
    {
        scoreDisplay = GetComponentInChildren<TextMeshProUGUI>();
        scoreDisplay.text = scoreAmount.ToString();
    }

    private void Update()
    {
        MoveUp();
    }
    void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
}
