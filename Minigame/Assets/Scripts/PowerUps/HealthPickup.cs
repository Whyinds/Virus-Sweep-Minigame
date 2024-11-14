using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PowerUp
{
    public int RestoreHealthAmount = 2;

    override public void GivePowerUp()
    {
        PlayerHealth.Instance.GainHealth(RestoreHealthAmount);
    }
}
