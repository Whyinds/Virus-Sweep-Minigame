using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleBallsPowerUp : PowerUp
{
    private void Start()
    {
        if (PlayerShooter.instance.currentProjectileUpgrades >= PlayerShooter.instance.maxProjectileUpgrade)
        {
            Destroy(gameObject);
        }
    }

    override public void GivePowerUp()
    {
        PlayerShooter.instance.AddProjectileAmountUpgrade();
    }
}
