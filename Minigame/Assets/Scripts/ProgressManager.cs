using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance;
    public TextMeshProUGUI progressText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        progressText.SetText("Sweeping... (0/"+ GameManager.Instance.bossesToWin + ")");
    }

    public void UpdateProgress()
    {
        if (GameManager.Instance.bossesToWin < GameManager.Instance.bossesDefeated)
        {
            progressText.SetText("Sweeping... (" + GameManager.Instance.bossesDefeated + "/" + GameManager.Instance.bossesToWin + ")");
        } else
        {
            progressText.SetText("Sweep Complete!");
        }
    }

    public void FailProgress()
    {
        progressText.SetText("Sweep Failed.");
    }
}
